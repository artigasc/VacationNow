using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.WindowsAzure.Storage.Blob;


namespace GoTour.Helper {
    public static class UploadAzureHelper {
        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string valStorageConnectionString) {
            CloudStorageAccount vStorageAccount;
            try {
                vStorageAccount = CloudStorageAccount.Parse(valStorageConnectionString);
            } catch (FormatException) {
                throw;
            } catch (ArgumentException) {
                throw;
            }

            return vStorageAccount;
        }
        public static async Task<string> UploadFilesToBlobStorageContainer(string valFileName, byte[] valFileData) {
            string vResult = string.Empty;
            try {
                CloudStorageAccount vStorageAccount = CreateStorageAccountFromConnectionString(Constants.vStorageConnectionString);
                CloudBlobClient vBlobClient = vStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer vContainer = vBlobClient.GetContainerReference(Constants.vContainerDefault);
                string vName = Guid.NewGuid().ToString() + valFileName;
                CloudBlockBlob blockBlob = vContainer.GetBlockBlobReference(vName);
                AccessCondition vAccesCondition = new AccessCondition();
                BlobRequestOptions vBlobRequestOptions = new BlobRequestOptions();
                vBlobRequestOptions.MaximumExecutionTime = TimeSpan.FromHours(24);
                vBlobRequestOptions.ServerTimeout = TimeSpan.FromHours(24);
                OperationContext vOperationContext = new OperationContext();

                await blockBlob.UploadFromByteArrayAsync(valFileData, 0, valFileData.Count<byte>(), vAccesCondition,vBlobRequestOptions, vOperationContext);
                vResult = blockBlob.Uri.AbsoluteUri;
            } catch (Exception vEx) {
                string message = vEx.Message;
            }
            return vResult;
        }
    }
}