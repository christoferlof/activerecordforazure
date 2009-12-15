using System;
using ActiveRecordForAzure.Core.Abstractions;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// The ActiveRecord Context Factory supports the lazy creation of the context
    /// </summary>
    public class ActiveRecordContextFactory {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveRecordContextFactory"/> class.
        /// </summary>
        protected ActiveRecordContextFactory() {}

        private static Func<IActiveRecordContext> _factory;

        /// <summary>
        /// Creates the context.
        /// </summary>
        /// <returns></returns>
        public static IActiveRecordContext CreateContext() {
            if (_factory == null)
                RegisterFactory(DefaultFactory());
            return _factory.Invoke();
        }

        /// <summary>
        /// Registers the factory to be used by CreateContext
        /// </summary>
        /// <param name="factory">The factory.</param>
        public static void RegisterFactory(Func<IActiveRecordContext> factory) {
            _factory = factory;
        }

        /// <summary>
        /// The default factory (Azure Context)
        /// </summary>
        /// <returns></returns>
        protected static Func<IActiveRecordContext> DefaultFactory() {

            return () => {
                        //TODO: remove connection string hard code
                       var config = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
                       var context = new ActiveRecordContext(
                           new TableServiceContextWrapper(
                               new TableServiceContext(config.TableEndpoint.ToString(),config.Credentials)));
                       return context;
                   };
        }
    }
}