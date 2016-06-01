using JetRx.Entities;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AzureEncryptionExtensions;
using AzureEncryptionExtensions.Providers;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using System.Collections.Concurrent;
using Microsoft.Azure;
using System.Web;
using JetRx.Common.Data.SqlServer;

namespace JetRx.Common.Services
{
    public class ImageService
    {
        public image AddImage(image Image)
        {
            using (var context = new JetRxContext())
            {
                context.Images.Add(Image);
                context.SaveChanges();

                return Image;
            }

        }

        public image GetImage(int imageId)
        {
            using (var context = new JetRxContext())
            {
                return context.Images.FirstOrDefault(i => i.id == imageId);
            }

        }

        public MemoryStream DownloadImage(int imageId)
        {
            
            var image = GetImage(imageId);

            var stream = new MemoryStream();
            var blobContainer = AzureUtilities.GetImageBlobContainer("JetRxAzureDocumentStorage");
            var blob = blobContainer.GetBlockBlobReference(image.url);

            var provider = ProviderFactory.CreateProviderFromKeyFileString(CryptoService.GetEncryptionKey());

            blob.DownloadToStreamEncrypted(provider, stream);

            return stream;

        }

       
    }


    public static class AzureUtilities
    {
        public const string DefaultImageDataTable = "imagedata";
        public const string ImageDataTableKey = "ImageDataTable";

        public const string ImageBlobContainerName = "images";

        // Cache the configuration data.
        private static ConcurrentDictionary<string, string> _ConfigurationEntries = new ConcurrentDictionary<string, string>();

        public static object BlobContainerPublicAccessType { get; private set; }

        /// <summary>
        /// Pulls configuration entries from either the CloudConfigurationManager (app/web.config or the cscfg if deployed) or the environment variable of the same name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The found value, or null.</returns>
        /// <remarks>Side-effect: Stores the result in the dictionary cache.</remarks>
        public static string FromConfiguration(string name)
        {
            return _ConfigurationEntries.GetOrAdd(name, x => CloudConfigurationManager.GetSetting(x) ?? Environment.GetEnvironmentVariable(name));
        }

        /// <summary>
        /// Get/create the Azure Table Storage table for image data.
        /// </summary>
        /// <param name="connectionStringOrKey"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static CloudTable GetImageDataTableAsync(string connectionStringOrKey = null, string tableName = null)
        {
            tableName = tableName ?? FromConfiguration(ImageDataTableKey) ?? DefaultImageDataTable;
            Trace.TraceInformation("Image data table: {0}", tableName);
            var table = GetTableClient(connectionStringOrKey)
                .GetTableReference(tableName);
            table.CreateIfNotExists();

            return table;
        }

        public static CloudTableClient GetTableClient(string connectionStringOrKey = null)
        {
            return GetStorageAccount(connectionStringOrKey).CreateCloudTableClient();
        }

        public static CloudBlobClient GetBlobClient(string connectionStringOrKey = null)
        {
            return GetStorageAccount(connectionStringOrKey).CreateCloudBlobClient();
        }

        public static CloudStorageAccount GetStorageAccount(string connectionStringOrKey = null)
        {


            return CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings[connectionStringOrKey].ConnectionString);
        }

        public static CloudBlobContainer GetImageBlobContainer(string connectionStringOrKey = null)
        {
            var blobClient = GetBlobClient(connectionStringOrKey);

            var container = blobClient.GetContainerReference(ImageBlobContainerName);

            // Create the container if it doesn't already exist
            container.CreateIfNotExistsAsync();

            // Enable public access to blobs but not the full container
            var permissions = container.GetPermissions();
            if (permissions.PublicAccess != Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Off)
            {
                permissions.PublicAccess = Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Off;
                container.SetPermissions(permissions);
            }

            return container;
        }
    }

}
