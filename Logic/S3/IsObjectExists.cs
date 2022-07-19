using Amazon.S3;
using Amazon.S3.Model;

namespace ETF_API.Logic
{
    public partial class S3
    {

        public static async Task<bool> IsObjectExists(string Path)
        {
            using var Client = CreateClient();
            return await IsObjectExists(Client, Path);
        }

        public static async Task<bool> IsObjectExists(AmazonS3Client Client, string Path)
        {
            var Request = new ListObjectsV2Request
            {
                BucketName = Global.Settings.Storage.S3.BucketName,
                Prefix = Path,
                MaxKeys = 1
            };

            return (await Client.ListObjectsV2Async(Request)).S3Objects.Any();
        }
    }
}
