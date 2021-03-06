using System.Linq;
using System.Web;
using ActiveRecordForAzure.Core.Abstractions;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// The ActiveRecord Context
    /// </summary>
    public class ActiveRecordContext : IActiveRecordContext {

        #region statics

        private static IActiveRecordContext _context;
        private const string ContextKey = "ar4a-context";

        protected static IActiveRecordContext Context {
            get {
                if (HttpContextExists())
                    return HttpContext;
                return _context;
 
            }
            set {
                if (HttpContextExists())
                    HttpContext = value;
                else
                    _context = value;
            }
        }

        private static IActiveRecordContext HttpContext {
            get{
                if (HttpContextExists()) {
                    return System.Web.HttpContext.Current.Items[ContextKey] as IActiveRecordContext;
                }
                return null;
            }
            set {
                if (HttpContextExists())
                    System.Web.HttpContext.Current.Items[ContextKey] = value;
            }
        }

        private static bool HttpContextExists() {
            return System.Web.HttpContext.Current != null;
        }


        /// <summary>
        /// Gets the current context
        /// </summary>
        /// <value>The current.</value>
        public static IActiveRecordContext Current {
            get {
                EnsureContext();
                return Context;
            }
        }

        /// <summary>
        /// Initializes the default context defined by the <see cref="ActiveRecordContextFactory"/>.
        /// </summary>
        public static void Initialize() {
            Initialize(ActiveRecordContextFactory.CreateContext());
        }

        /// <summary>
        /// Initializes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Initialize(IActiveRecordContext context) {
            Context = context;
        }

        /// <summary>
        /// Destroys current context
        /// </summary>
        public static void Destroy() {
            Initialize(null);
        }

        /// <summary>
        /// Ensures the context.
        /// </summary>
        protected static void EnsureContext() {
            if (Context == null)
                Initialize();
        }

        #endregion

        private readonly ITableServiceContext _dataContext;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveRecordContext"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ActiveRecordContext(ITableServiceContext dataContext) {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Creates the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : ActiveRecord<TEntity>, new() {
            var query = _dataContext.CreateQuery<TEntity>(typeof (TEntity).GetTableName());
            //TODO: move AsTableServiceQuery call
            //if (query != null)
            //    return query.AsTableServiceQuery(); 
            return query;
        }

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void AddEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            _dataContext.AddObject(typeof(TEntity).GetTableName(), entity);
            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            _dataContext.UpdateObject(entity);
            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void DeleteEntity<TEntity>(TEntity entity) where TEntity : ActiveRecord<TEntity>, new() {
            _dataContext.DeleteObject(entity);
            _dataContext.SaveChanges();
        }
    }
}