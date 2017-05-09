using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.DataBuilders
{
    /// <summary>
    /// Basic Builder interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <tocexclude />
    public interface Builds<T>
    {
        /// <summary>
        /// Builds the object context.
        /// </summary>
        /// <returns></returns>
        T Build();
    }

    /// <summary>
    /// Indicates that the implementation can set a TransactionType parameter.
    /// </summary>
    /// <typeparam name="BuilderClass"></typeparam>
    /// <tocexclude />
    public interface SpecifiesTransactionType<BuilderClass>
    {
        /// <summary>
        /// Set the transaction type on the object being built.
        /// </summary>
        /// <param name="tranxType"></param>
        /// <returns></returns>
        BuilderClass WithTransactionType(TransactionType tranxType);

    }

    /// <summary>
    /// An extended version of <see cref="Builds{T}"/> that provides the ability to attach AuditRecord nodes to the object context.
    /// </summary>
    /// <typeparam name="DataClass"></typeparam>
    /// <typeparam name="BuilderClass"></typeparam>
    /// <tocexclude />
    public interface AuditableBuilder<DataClass, BuilderClass> : Builds<DataClass>
    {
        /// <summary>
        /// Add an AuditRecord object to the current object context using the specified parameter values.  Returns the specified Builder class. 
        /// </summary>
        /// <param name="UserOID"></param>
        /// <param name="LocationOID"></param>
        /// <param name="ReasonForChange"></param>
        /// <param name="SourceID"></param>
        /// <param name="DateTimeStamp"></param>
        /// <param name="auditRecordBuilder"></param>
        /// <returns></returns>
        BuilderClass WithAuditRecord(string UserOID, string LocationOID, string ReasonForChange, string SourceID, DateTime DateTimeStamp, Action<AuditRecordBuilder> auditRecordBuilder);
    }

    /// <summary>
    /// Provides a base class for ODM data builders that need to attach an AuditRecord object to their output. This class cannot be instantiated. 
    /// </summary>
    /// <typeparam name="DataClass"></typeparam>
    /// <typeparam name="BuilderClass"></typeparam>
    /// <tocexclude />
    public abstract class AuditableBuilderBase<DataClass, BuilderClass> : AuditableBuilder<DataClass, BuilderClass>
    {

        /// <summary>
        /// The object context to be constructed.
        /// </summary>
        protected DataClass thisNode;

        /// <summary>
        /// Fluent method for attaching an Audit Record to this instance.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>
        /// The BuilderClass.
        /// </returns>
        public BuilderClass WithAuditRecord(ODMcomplexTypeDefinitionAuditRecord record)
        {
            dynamic d = this.thisNode;
            d.AuditRecord = record;
            return ThisAsTSelf();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <tocexclude />
        protected abstract BuilderClass ThisAsTSelf();

        /// <summary>
        /// See <see cref="Builds{T}.Build()"></see> for more information.
        /// </summary>
        /// <returns></returns>
        /// <tocexclude />
        public DataClass Build()
        {
            return thisNode;
        }

        /// <summary>
        /// See <see cref="AuditableBuilder{DataClass, BuilderClass}.WithAuditRecord(string, string, string, string, DateTime, Action{AuditRecordBuilder})"/> for more information. 
        /// </summary>
        /// <param name="UserOID"></param>
        /// <param name="LocationOID"></param>
        /// <param name="ReasonForChange"></param>
        /// <param name="SourceID"></param>
        /// <param name="DateTimeStamp"></param>
        /// <param name="auditRecordBuilder"></param>
        /// <returns></returns>
        /// <tocexclude />
        public BuilderClass WithAuditRecord(string UserOID, string LocationOID, string ReasonForChange, string SourceID, DateTime DateTimeStamp, Action<AuditRecordBuilder> auditRecordBuilder)
        {
            var arb = new AuditRecordBuilder(UserOID, LocationOID, ReasonForChange, SourceID, DateTimeStamp);

            auditRecordBuilder(arb);

            dynamic d = thisNode;

            d.AuditRecord = arb.Build();

            return ThisAsTSelf();
        }
    }


}
