using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// Provides access to the Azure Table Store
    /// </summary>
    public class CloudTableProvider : ITableProvider {

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudTableProvider"/> class.
        /// </summary>
        public CloudTableProvider() {
            HandleConfigurationSettingsChange();
        }

        private void HandleConfigurationSettingsChange() {

            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) => {

                configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));

                RoleEnvironment.Changed += (anotherSender, arg) => {
                    if (arg.Changes.OfType<RoleEnvironmentConfigurationSettingChange>()
                        .Any((change) => (change.ConfigurationSettingName == configName))) {

                        if (!configSetter(RoleEnvironment.GetConfigurationSettingValue(configName))) {
                            RoleEnvironment.RequestRecycle();
                        }
                    }
                };
            });
        }

        /// <summary>
        /// Creates the table if it not exists.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        /// <remarks>Requires a connection string with the name "DataConnectionString"</remarks>
        public bool CreateTableIfNotExists(string tableName) {

            var client = CloudStorageAccount
                .FromConfigurationSetting("DataConnectionString") //TODO: remove hard code
                .CreateCloudTableClient();

            return client.CreateTableIfNotExist(tableName);
        }
    }
}