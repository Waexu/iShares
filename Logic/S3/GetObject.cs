using Amazon.S3;
using Amazon.S3.Model;

namespace ETF_API.Logic
{
    public partial class S3
    {
        public static async Task<GetObjectResponse> GetObject(string ObjectPath)
        {
            using var Client = CreateClient();
            return await GetObject(Client, ObjectPath);
        }

        public static async Task<GetObjectResponse> GetObject(AmazonS3Client Client, string ObjectPath)
        {
            var Request = new GetObjectRequest()
            {
                BucketName = Global.Settings.Storage.S3.BucketName,
                Key = ObjectPath,
            };

            return await Client.GetObjectAsync(Request);
        }
    }
}
