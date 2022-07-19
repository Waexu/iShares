using Amazon.S3;
using Amazon.S3.Model;

namespace ETF_API.Logic
{
    public partial class S3
    {
        public static async Task CreateObject(string ObjectPath, string ContentBody = null)
        {
            using var Client = CreateClient();
            await CreateObject(Client, ObjectPath, ContentBody);
        }

        public static async Task CreateObject(AmazonS3Client Client, string ObjectPath, string ContentBody = null)
        {
            var Request = new PutObjectRequest()
            {
                BucketName = Global.Settings.Storage.S3.BucketName,
                StorageClass = S3StorageClass.Standard,
                ServerSideEncryptionMethod = ServerSideEncryptionMethod.None,
                Key = ObjectPath,
                ContentBody = ContentBody
            };

            await Client.PutObjectAsync(Request);
        }
    }
}
