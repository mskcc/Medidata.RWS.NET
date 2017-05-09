using System;

namespace Medidata.RWS.Extras.AuditEvent
{
    /// <summary>
    /// Event Subscriber interface
    /// </summary>
    public interface ISubscriber
    {

    }


    /// <summary>
    /// Abstract subscriber for audit events.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ISubscriber" />
    public abstract class AuditEventSubscriber : ISubscriber
    {
        /// <summary>
        /// Called when [context built].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="ContextEventArgs"/> instance containing the event data.</param>
        public abstract void OnContextBuilt(object source, ContextEventArgs e);

        /// <summary>
        /// Called when [parsing complete].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="ODMAdapterEventArgs"/> instance containing the event data.</param>
        public abstract void OnParsingComplete(object source, ODMAdapterEventArgs args);


        /// <summary>
        /// Called when [page processed].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public abstract void OnPageProcessed(object source, EventArgs args);
    }

}