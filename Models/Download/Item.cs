using System.Text.Json.Serialization;

namespace ETF_API.Models.Download
{
    public class DownloadItem
    {
        [JsonConstructor]
        public DownloadItem(ETF.Item ETF)
        {
            this.ETF = ETF ?? throw new Exception("ETF can't be null");
        }
        public ETF.Item ETF { get; set;  }
        public bool Success { get; set; }
        public bool IsNew { get; set; }
        public string Status { get; set; }
    }
}
