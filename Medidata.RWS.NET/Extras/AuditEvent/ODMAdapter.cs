using Medidata.RWS.Core.Requests;
using Medidata.RWS.Core.Requests.ODM_Adapter;
using System;
using System.Linq;
using System.Web;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// A data fetcher / parser using a connection to RWS and a `subscriber` class provided by the client (end user)
    /// </summary>
    public class OdmAdapter
    {
        /// <summary>
        /// Gets the RWS connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public IRWSConnection Connection { get; private set; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public string Environment { get; private set; }

        /// <summary>
        /// Gets the study.
        /// </summary>
        /// <value>
        /// The study.
        /// </value>
        public string Study { get; private set; }

        /// <summary>
        /// Gets the start identifier.
        /// </summary>
        /// <value>
        /// The start identifier.
        /// </value>
        public int? StartId { get; private set; }

        /// <summary>
        /// Gets the subscriber.
        /// </summary>
        /// <value>
        /// The subscriber.
        /// </value>
        public AuditEventSubscriber Subscriber { get; private set; }


        /// <summary>
        /// Gets the audit event parser.
        /// </summary>
        /// <value>
        /// The parser.
        /// </value>
        public IAuditEventParser Parser { get; private set; }


        /// <summary>
        /// Occurs when the ODM Adapter has finished running.
        /// </summary>
        public event ParsingCompleteEventHandler ParsingComplete;

        /// <summary>
        /// Occurs when [page processed].
        /// </summary>
        public event PageProcessedEventHandler PageProcessed;

        /// <summary>
        /// Gets the last source identifier.
        /// </summary>
        /// <value>
        /// The last source identifier.
        /// </value>
        public int LastSourceId { get; private set; }

        /// <summary>
        /// Runs the ODM Adapter, using the specified parameters.
        /// </summary>
        /// <param name="startId">The start identifier.</param>
        /// <param name="maxPages">The maximum pages.</param>
        /// <param name="perPage">The per page.</param>
        public void Run(int startId, int maxPages, int perPage)
        {
            LastSourceId = -1;
            var page = 0;
            StartId = startId;

            if (Subscriber != null)
            {
                Parser.ContextBuilt += Subscriber.OnContextBuilt;
                ParsingComplete += Subscriber.OnParsingComplete;
                PageProcessed += Subscriber.OnPageProcessed;
            }

            while (maxPages == -1 || page < maxPages)
            {

                var aRequest = new AuditRecordsRequest(Study, Environment, StartId, perPage);
                var odm = Connection.SendRequest(aRequest);

                Parser.ForOdmResponse(odm);

                StartId = GetNextStartId();

                Parser.Start();

                page += 1;


                //If there are no additional records to parse, exit loop
                if (StartId == null)
                {
                    OnPageProcessed();
                    break;
                }

                OnPageProcessed();

            }

            LastSourceId = Parser.GetLastSourceId();

            //raise event once parsing is complete.
            OnParsingComplete(LastSourceId);


        }

        /// <summary>
        /// Called when [page processed].
        /// </summary>
        protected virtual void OnPageProcessed()
        {
            PageProcessed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when the context object has been built.
        /// </summary>
        /// <param name="lastSourceId">The last source identifier.</param>
        protected virtual void OnParsingComplete(int lastSourceId)

        {

            ParsingComplete?.Invoke(this, new ODMAdapterEventArgs() { LastSourceID = lastSourceId });

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="OdmAdapter" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="study">The study.</param>
        /// <param name="environment">The environment.</param>
        public OdmAdapter(IRWSConnection connection, IAuditEventParser parser, string study, string environment)
        {
            Connection = connection;
            Study = study;
            Environment = environment;
            StartId = 0;
            Parser = parser;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OdmAdapter" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="study">The study.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="subscriber">The subscriber.</param>
        public OdmAdapter(IRWSConnection connection, IAuditEventParser parser, string study, string environment, AuditEventSubscriber subscriber) : this(connection, parser, study, environment)
        {
            Subscriber = subscriber;
        }


        /// <summary>
        /// Get the next "startid", which is based on a "Link" response header.
        /// </summary>
        /// <returns></returns>
        private int? GetNextStartId()
        {

            if (Connection.GetLastResult() == null) return null;
            
            if (Connection.GetLastResult().Headers.Any(t => t.Name == "Link"))
            {
              

                var pageLinks = new PageLinks(Connection.GetLastResult());

                var parsedQueryString = HttpUtility.ParseQueryString(pageLinks.GetNext());

                int sId;

                int.TryParse(parsedQueryString["startid"], out sId);

                return sId;

            }

            return null;


        }
    }
}
