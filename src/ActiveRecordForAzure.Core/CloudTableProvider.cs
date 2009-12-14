using System;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace ActiveRecordForAzure.Core {
    public class CloudTableProvider : ITableProvider {

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

        public bool CreateTableIfNotExists(string tableName) {

            var client = CloudStorageAccount
                .FromConfigurationSetting("DataConnectionString") //TODO: remove hard code
                .CreateCloudTableClient();

            return client.CreateTableIfNotExist(tableName);
        }
    }
}