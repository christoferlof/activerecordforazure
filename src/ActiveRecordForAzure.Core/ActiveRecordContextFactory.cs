using System;
using ActiveRecordForAzure.Core.Abstractions;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecordContextFactory {

        protected ActiveRecordContextFactory() {}

        private static Func<IActiveRecordContext> _factory;

        public static IActiveRecordContext CreateContext() {
            if (_factory == null)
                RegisterFactory(DefaultFactory());
            return _factory.Invoke();
        }

        public static void RegisterFactory(Func<IActiveRecordContext> factory) {
            _factory = factory;
        }

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