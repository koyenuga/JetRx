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

namespace JetRx.Api.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class MultipartBlobStream : Stream
    {
        public static bool ReplaceFilenamesWithGuids { get; set; }

        public MultipartBlobStream(CloudBlobContainer container, string filename, string extension)
        {
            extension = extension.ToLowerInvariant().TrimStart('.');
            if (filename.EndsWith(extension))
            {
                filename = filename.Substring(0, filename.Length - 1 - extension.Length);
            }
            this.blobContainer = container;
            this.FileName = Guid.NewGuid().ToString("N");
            this.Extension = extension;
        }

        private CloudBlobContainer blobContainer;
        private MemoryStream underlyingStream = new MemoryStream();

        public string FileName { get; set; }
        public string Extension { get; set; }

        public override bool CanRead { get { return this.underlyingStream.CanRead; } }

        public override bool CanSeek { get { return this.underlyingStream.CanSeek; } }

        public override bool CanWrite { get { return this.underlyingStream.CanWrite; } }

        public override long Length { get { return this.underlyingStream.Length; } }

        public override long Position
        {
            get { return this.underlyingStream.Position; }
            set { this.underlyingStream.Position = value; }
        }

        public override void Flush()
        {
            this.underlyingStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.underlyingStream.Read(buffer, offset, count);
        }

        public override int ReadByte()
        {
            return this.underlyingStream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.underlyingStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.underlyingStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.underlyingStream.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            this.underlyingStream.WriteByte(value);
        }

        public string BlobPath()
        {
            return Uri.EscapeUriString(string.Format("{0}.{1}", this.FileName, this.Extension));
        }

        public string UploadStreamToBlob()
        {

            var blobPath = this.BlobPath();

            Trace.TraceInformation("Stream closed, writing {0} bytes to {1} in {2}.", this.underlyingStream.Length, blobPath, this.blobContainer.Name);

            this.underlyingStream.Position = 0;

            var blob = this.blobContainer.GetBlockBlobReference(blobPath);

            var EcryptKey = HttpContext.Current.Cache["EnCode"] as ApplicationConfig;

            var provider = ProviderFactory.CreateProviderFromKeyFileString(EcryptKey.Value);

            blob.UploadFromStreamEncrypted(provider, this.underlyingStream);

            return blobPath;
        }
    }

    /// <summary>
    /// Processes uploaded images into Blob storage. Uses 
    /// </summary>
    public class AzureBlobMultipartProvider : MultipartStreamProvider
    {
        private CloudBlobContainer imageBlobContainer;
        private CloudTable imageDataTable;

        public AzureBlobMultipartProvider(CloudBlobContainer imageBlobContainer)
        {
            this.imageBlobContainer = imageBlobContainer;
            
            this.FormFields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            this.FormFiles = new Dictionary<string, MultipartBlobStream>();
        }

        public IDictionary<string, string> FormFields { get; private set; }

        public IDictionary<string, MultipartBlobStream> FormFiles { get; private set; }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
            if (contentDisposition != null)
            {
                // Found a file! Track it, and ultimately upload to blob store.
                if (!String.IsNullOrWhiteSpace(contentDisposition.FileName))
                {
                    var fileInfo = new FileInfo(contentDisposition.FileName.Trim('"'));
                    var blobStream = new MultipartBlobStream(this.imageBlobContainer, fileInfo.Name, fileInfo.Extension);
                    this.FormFiles[fileInfo.Name] = blobStream;
                    return blobStream;
                }
                else
                {
                    return new MemoryStream();
                }
            }
            else
            {
                throw new InvalidOperationException("No 'Content-Disposition' header");
            }
        }

        /// <summary>
        /// Read the non-file contents as form data.
        /// </summary>
        /// <returns></returns>
        public override async Task ExecutePostProcessingAsync()
        {
            foreach (var formContent in Contents)
            {
                ContentDispositionHeaderValue contentDisposition = formContent.Headers.ContentDisposition;
                // Not a file, treat as a form field.
                if (String.IsNullOrWhiteSpace(contentDisposition.FileName))
                {
                    var fieldName = (contentDisposition.Name ?? "").Trim('"');
                    var fieldValue = await formContent.ReadAsStringAsync();
                    this.FormFields[fieldName] = fieldValue;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<image> UploadFiles(imagetype type, int ownerId)
        {
            List<image> images = new List<image>();
            foreach (var blob in this.FormFiles.Values)
            {
                var blobName = blob.UploadStreamToBlob();
                images.Add(new image {
                    image_type = type,
                    owner_id = ownerId,
                    url = blobName

                });
            }

            return images;
        }

     
        
        public string UploadFile()
        {

            var blob = this.FormFiles.Values.FirstOrDefault();
            string name = string.Empty;
            if (blob != null)
            {
               name = blob.UploadStreamToBlob();
             

            }

            return name;
        }
    }

    

}