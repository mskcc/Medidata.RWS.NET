using Medidata.RWS.Core.Exceptions;

namespace Medidata.RWS.Core.Requests
{
    using Responses;
    using RestSharp;
    using RestSharp.Authenticators;
    using System;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Represents a connection to RAVE Web Services.
    /// </summary>
    public class RwsConnection : IRWSConnection
    {
        private string domain;

        private string base_url;

        private IAuthenticator auth;


        /// <summary>
        /// Gets the result of the last request that was made.
        /// </summary>
        /// <value>
        /// The last result.
        /// </value>
        public IRestResponse last_result { get; private set; }
        private TimeSpan request_time;
        private RestClient client;

        /// <summary>
        /// Create a connection to RWS using the specified domain and virtual directory.
        /// </summary>
        /// <param name="domain">The client portion of the Medidata RWS url, e.g. `mediflex`.</param>
        /// <param name="virtual_dir">The virtual directory.</param>
        public RwsConnection(string domain, string virtual_dir = "RaveWebServices")
        {
            this.domain = domain.ToLower().StartsWith("http") ? domain : string.Format("https://{0}.mdsol.com", domain);
            this.base_url = string.Format("{0}/{1}", this.domain, virtual_dir);
            this.request_time = TimeSpan.MinValue;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RwsConnection"/> class.
        /// </summary>
        /// <param name="domain">The client portion of the Medidata RWS url, e.g. `mediflex`.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="virtual_dir">The virtual directory.</param>
        public RwsConnection(string domain, string username, string password, string virtual_dir = "RaveWebServices") : this(domain, virtual_dir)
        {
            this.auth = new HttpBasicAuthenticator(username, password);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RwsConnection"/> class, using a supplied <see cref="IAuthenticator" />.
        /// </summary>
        /// <param name="domain">The client portion of the Medidata RWS url, e.g. `mediflex`.</param>
        /// <param name="auth">The authenticator.</param>
        /// <param name="virtual_dir">The virtual directory.</param>
        public RwsConnection(string domain, IAuthenticator auth, string virtual_dir = "RaveWebServices") : this(domain, virtual_dir)
        {

            this.auth = auth;

        }


        /// <summary>
        /// Sends a request to RWS.
        /// </summary>
        /// <param name="rws_request">The RWS request.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        /// <exception cref="RWSException">
        /// IIS Error
        /// or
        /// Server Error (500)
        /// or
        /// Unauthorized.
        /// or
        /// Unspecified Error.
        /// </exception>
        public IRWSResponse SendRequest(RWSRequest rws_request, int? timeout = null)
        {

            this.client = new RestClient(base_url);

            if (rws_request.RequiresAuthentication)
            {
                client.Authenticator = auth;
                if (timeout != null) { client.Timeout = (int)timeout; }
            }

            var request = new RestRequest(rws_request.UrlPath(), rws_request.HttpMethod);

            //Add post body if the request is a POST
            if(rws_request.HttpMethod == Method.POST)
            {
                request.AddParameter("text/xml; charset=utf-8", rws_request.RequestBody, ParameterType.RequestBody);
            }

            foreach (var header in rws_request.Headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            var start_time = DateTime.UtcNow;

            var response = client.Execute(request);

            //keep track of last response
            this.last_result = response;

            request_time = DateTime.UtcNow.Subtract(start_time);

            //Based on the response code...
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.NotFound:
                    if (response.Content.StartsWith("<Response"))
                    {
                        var error = new RWSErrorResponse(response.Content);
                        throw new RWSException(error.ErrorDescription, error);
                    }
                    else if (response.Content.Contains("<html"))
                    {
                        throw new RWSException("IIS Error", response.Content);
                    }
                    else
                    {
                        var error = new RwsError(response.Content);
                        throw new RWSException(error.ErrorDescription, error);
                    }
                case HttpStatusCode.InternalServerError:
                    throw new RWSException("Server Error (500)", response.Content);
                case HttpStatusCode.Forbidden:
                    if (response.Content.Contains("<h2>HTTP Error 401.0 - Unauthorized</h2>"))
                    {
                        throw new RWSException("Unauthorized.", response.Content);
                    }

                    dynamic _error;
                    if (response.Headers.Any(x => x.ContentType.StartsWith("text/xml")))
                    {
                        if (response.Content.StartsWith("<Response"))
                        {
                            _error = new RWSErrorResponse(response.Content);
                        }
                        else if (response.Content.Contains("ODM"))
                        {
                            _error = new RwsError(response.Content);
                        }
                        else
                        {
                            throw new RWSException("Unspecified Error.", response.Content);
                        }

                    }
                    else
                    {
                        _error = new RWSErrorResponse(response.Content);
                    }
                    throw new RWSException(_error.ErrorDescription, _error);

            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                dynamic _error;
                if (response.Content.Contains("<"))
                {
                    if (response.Content.Trim().StartsWith("<Response"))
                    {
                        _error = new RWSErrorResponse(response.Content);
                    }
                    else if (response.Content.Contains("ODM"))
                    {
                        _error = new RwsError(response.Content);
                    }
                    else
                    {
                        throw new RWSException(string.Format("Unexpected Status Code ({0})", response.StatusCode.ToString()), response.Content);
                    }
                }
                else
                {
                    throw new RWSException(string.Format("Unexpected Status Code ({0})", response.StatusCode.ToString()), response.Content);
                }
                throw new RWSException(_error.ErrorDescription, _error);
            }

            return rws_request.Result(response);

        }

        /// <summary>
        /// Gets the last result.
        /// </summary>
        /// <returns>
        /// The IRestResponse of the last result.
        /// </returns>
        public IRestResponse GetLastResult()
        {
            return last_result;
        }
    }

    /// <summary>
    /// Rave Web Services interface.
    /// </summary>
    public interface IRWSConnection
    {
        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="rWSRequest">The RWS request.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        /// The IRWSResponse.
        /// </returns>
        IRWSResponse SendRequest(RWSRequest rWSRequest, int? timeout = null);

        /// <summary>
        /// Gets the last result.
        /// </summary>
        /// <returns>
        /// The IRestResponse.
        /// </returns>
        IRestResponse GetLastResult();
    }
}
