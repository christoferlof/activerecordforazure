using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Net;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Linq;

namespace ActiveRecordForAzure.Core.Abstractions {

    /// <summary>
    /// Wraps the <see cref="TableServiceContext"/> for testability
    /// </summary>
    public class TableServiceContextWrapper : ITableServiceContext {
        
        private readonly TableServiceContext _context;

        public TableServiceContextWrapper(TableServiceContext context) {
            _context = context;
        }

        public IAsyncResult BeginSaveChangesWithRetries(AsyncCallback callback, object state) {
            return _context.BeginSaveChangesWithRetries(callback, state);
        }

        public IAsyncResult BeginSaveChangesWithRetries(SaveChangesOptions options, AsyncCallback callback, object state) {
            return _context.BeginSaveChangesWithRetries(options, callback, state);
        }

        public DataServiceResponse EndSaveChangesWithRetries(IAsyncResult asyncResult) {
            return _context.EndSaveChangesWithRetries(asyncResult);
        }

        public DataServiceResponse SaveChangesWithRetries() {
            return _context.SaveChangesWithRetries();
        }

        public DataServiceResponse SaveChangesWithRetries(SaveChangesOptions options) {
            return _context.SaveChangesWithRetries(options);
        }

        public RetryPolicy RetryPolicy {
            get { return _context.RetryPolicy; }
            set { _context.RetryPolicy = value; }
        }

        public StorageCredentials StorageCredentials {
            get { return _context.StorageCredentials; }
        }

        public Uri BaseUri {
            get { return _context.BaseUri; }
        }

        public ICredentials Credentials {
            get { return _context.Credentials; }
            set { _context.Credentials = value; }
        }

        public MergeOption MergeOption {
            get { return _context.MergeOption; }
            set { _context.MergeOption = value; }
        }

        public bool IgnoreMissingProperties {
            get { return _context.IgnoreMissingProperties; }
            set { _context.IgnoreMissingProperties = value; }
        }

        public string DataNamespace {
            get { return _context.DataNamespace; }
            set { _context.DataNamespace = value; }
        }

        public Func<Type, string> ResolveName {
            get { return _context.ResolveName; }
            set { _context.ResolveName = value; }
        }

        public Func<string, Type> ResolveType {
            get { return _context.ResolveType; }
            set { _context.ResolveType = value; }
        }

        public int Timeout {
            get { return _context.Timeout; }
            set { _context.Timeout = value; }
        }

        public Uri TypeScheme {
            get { return _context.TypeScheme; }
            set { _context.TypeScheme = value; }
        }

        public bool UsePostTunneling {
            get { return _context.UsePostTunneling; }
            set { _context.UsePostTunneling = value; }
        }

        public ReadOnlyCollection<LinkDescriptor> Links {
            get { return _context.Links; }
        }

        public ReadOnlyCollection<EntityDescriptor> Entities {
            get { return _context.Entities; }
        }

        public SaveChangesOptions SaveChangesDefaultOptions {
            get { return _context.SaveChangesDefaultOptions; }
            set { _context.SaveChangesDefaultOptions = value; }
        }

        public DataServiceQuery<TEntity> CreateQuery<TEntity>(string entitySetName) {
            return _context.CreateQuery<TEntity>(entitySetName);
        }

        public Uri GetMetadataUri() {
            return _context.GetMetadataUri();
        }

        public IAsyncResult BeginLoadProperty(object entity, string propertyName, AsyncCallback callback, object state) {
            return _context.BeginLoadProperty(entity, propertyName, callback, state);
        }

        public QueryOperationResponse EndLoadProperty(IAsyncResult asyncResult) {
            return _context.EndLoadProperty(asyncResult);
        }

        public QueryOperationResponse LoadProperty(object entity, string propertyName) {
            return _context.LoadProperty(entity, propertyName);
        }

        public IAsyncResult BeginExecuteBatch(AsyncCallback callback, object state, params DataServiceRequest[] queries) {
            return _context.BeginExecuteBatch(callback, state, queries);
        }

        public DataServiceResponse EndExecuteBatch(IAsyncResult asyncResult) {
            return _context.EndExecuteBatch(asyncResult);
        }

        public DataServiceResponse ExecuteBatch(params DataServiceRequest[] queries) {
            return _context.ExecuteBatch(queries);
        }

        public IAsyncResult BeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state) {
            return _context.BeginExecute<TElement>(requestUri, callback, state);
        }

        public IEnumerable EndExecute<TElement>(IAsyncResult asyncResult) {
            return _context.EndExecute<TElement>(asyncResult);
        }

        public IEnumerable Execute<TElement>(Uri requestUri) {
            return _context.Execute<TElement>(requestUri);
        }

        public IAsyncResult BeginSaveChanges(AsyncCallback callback, object state) {
            return _context.BeginSaveChanges(callback, state);
        }

        public IAsyncResult BeginSaveChanges(SaveChangesOptions options, AsyncCallback callback, object state) {
            return _context.BeginSaveChanges(options, callback, state);
        }

        public DataServiceResponse EndSaveChanges(IAsyncResult asyncResult) {
            return _context.EndSaveChanges(asyncResult);
        }

        public DataServiceResponse SaveChanges() {
            return _context.SaveChanges();
        }

        public DataServiceResponse SaveChanges(SaveChangesOptions options) {
            return _context.SaveChanges(options);
        }

        public void AddLink(object source, string sourceProperty, object target) {
            _context.AddLink(source, sourceProperty, target);
        }

        public void AttachLink(object source, string sourceProperty, object target) {
            _context.AttachLink(source,sourceProperty,target);
        }

        public bool DetachLink(object source, string sourceProperty, object target) {
            return _context.DetachLink(source, sourceProperty, target);
        }

        public void DeleteLink(object source, string sourceProperty, object target) {
            _context.DeleteLink(source,sourceProperty,target);
        }

        public void SetLink(object source, string sourceProperty, object target) {
            _context.SetLink(source,sourceProperty,target);
        }

        public void AddObject(string entitySetName, object entity) {
            _context.AddObject(entitySetName,entity);
        }

        public void AttachTo(string entitySetName, object entity) {
            _context.AttachTo(entitySetName,entity);
        }

        public void AttachTo(string entitySetName, object entity, string etag) {
            _context.AttachTo(entitySetName,entity,etag);
        }

        public void DeleteObject(object entity) {
            _context.DeleteObject(entity);
        }

        public bool Detach(object entity) {
            return _context.Detach(entity);
        }

        public void UpdateObject(object entity) {
            _context.UpdateObject(entity);
        }

        public bool TryGetEntity<TEntity>(Uri identity, out TEntity entity) where TEntity : class {
            return _context.TryGetEntity<TEntity>(identity,out entity);
        }

        public bool TryGetUri(object entity, out Uri identity) {
            return _context.TryGetUri(entity, out identity);
        }

        public event EventHandler<SendingRequestEventArgs> SendingRequest;
        public event EventHandler<ReadingWritingEntityEventArgs> ReadingEntity;
        public event EventHandler<ReadingWritingEntityEventArgs> WritingEntity;
    }
}