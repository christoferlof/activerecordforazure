using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Net;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core.Abstractions {

    /// <summary>
    /// Interface extracted from <see cref="TableServiceContext"/>
    /// </summary>
    public interface ITableServiceContext {
        IAsyncResult BeginSaveChangesWithRetries(AsyncCallback callback, object state);
        IAsyncResult BeginSaveChangesWithRetries(SaveChangesOptions options, AsyncCallback callback, object state);
        DataServiceResponse EndSaveChangesWithRetries(IAsyncResult asyncResult);
        DataServiceResponse SaveChangesWithRetries();
        DataServiceResponse SaveChangesWithRetries(SaveChangesOptions options);
        RetryPolicy RetryPolicy { get; set; }
        StorageCredentials StorageCredentials { get; }
        Uri BaseUri { get; }
        ICredentials Credentials { get; set; }
        MergeOption MergeOption { get; set; }
        bool IgnoreMissingProperties { get; set; }
        string DataNamespace { get; set; }
        Func<Type, string> ResolveName { get; set; }
        Func<string, Type> ResolveType { get; set; }
        int Timeout { get; set; }
        Uri TypeScheme { get; set; }
        bool UsePostTunneling { get; set; }
        ReadOnlyCollection<LinkDescriptor> Links { get; }
        ReadOnlyCollection<EntityDescriptor> Entities { get; }
        SaveChangesOptions SaveChangesDefaultOptions { get; set; }
        DataServiceQuery<TElement> CreateQuery<TElement>(string entitySetName);
        Uri GetMetadataUri();
        IAsyncResult BeginLoadProperty(object entity, string propertyName, AsyncCallback callback, object state);
        QueryOperationResponse EndLoadProperty(IAsyncResult asyncResult);
        QueryOperationResponse LoadProperty(object entity, string propertyName);
        IAsyncResult BeginExecuteBatch(AsyncCallback callback, object state, params DataServiceRequest[] queries);
        DataServiceResponse EndExecuteBatch(IAsyncResult asyncResult);
        DataServiceResponse ExecuteBatch(params DataServiceRequest[] queries);
        IAsyncResult BeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state);
        IEnumerable EndExecute<TElement>(IAsyncResult asyncResult);
        IEnumerable Execute<TElement>(Uri requestUri);
        IAsyncResult BeginSaveChanges(AsyncCallback callback, object state);
        IAsyncResult BeginSaveChanges(SaveChangesOptions options, AsyncCallback callback, object state);
        DataServiceResponse EndSaveChanges(IAsyncResult asyncResult);
        DataServiceResponse SaveChanges();
        DataServiceResponse SaveChanges(SaveChangesOptions options);
        void AddLink(object source, string sourceProperty, object target);
        void AttachLink(object source, string sourceProperty, object target);
        bool DetachLink(object source, string sourceProperty, object target);
        void DeleteLink(object source, string sourceProperty, object target);
        void SetLink(object source, string sourceProperty, object target);
        void AddObject(string entitySetName, object entity);
        void AttachTo(string entitySetName, object entity);
        void AttachTo(string entitySetName, object entity, string etag);
        void DeleteObject(object entity);
        bool Detach(object entity);
        void UpdateObject(object entity);
        bool TryGetEntity<TEntity>(Uri identity, out TEntity entity) where TEntity : class;
        bool TryGetUri(object entity, out Uri identity);
        event EventHandler<SendingRequestEventArgs> SendingRequest;
        event EventHandler<ReadingWritingEntityEventArgs> ReadingEntity;
        event EventHandler<ReadingWritingEntityEventArgs> WritingEntity;
    }
}