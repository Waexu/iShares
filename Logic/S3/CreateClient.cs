using Amazon.S3;

namespace ETF_API.Logic
{
    public partial class S3
    {
        public static AmazonS3Client CreateClient()
        {
            var Settings = Global.Settings.Storage.S3;
            var Region = Amazon.RegionEndpoint.GetBySystemName(Settings.Region);
            return new AmazonS3Client(Settings.AccessKeyId, Settings.SecretAccessKey, Region);
        }
    }
}
