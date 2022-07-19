using Amazon.S3;
using Amazon.S3.Model;

namespace ETF_API.Logic
{
    public partial class S3
    {

        public static async Task<List<S3Object>> GetObjectsList(string Prefix)
        {
            using var Client = CreateClient();
            return await GetObjectsList(Client, Prefix);
        }

        public static async Task<List<S3Object>> GetObjectsList(AmazonS3Client Client, string Prefix)
        {
            var Request = new ListObjectsV2Request
            {
                BucketName = Global.Settings.Storage.S3.BucketName,
                Prefix = Prefix,
            };

            return (await Client.ListObjectsV2Async(Request)).S3Objects;
        }
    }
}
