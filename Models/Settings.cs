using System.Globalization;

namespace ETF_API.Models
{
    public class Settings
    {
        public class iSharesSettings
        {
            public class CsvSettings
            {
                public string Ticker_Header { get; set; }
                public string Name_Header { get; set; }
                public string Type_Header { get; set; }
                public string Sector_Header { get; set; }
                public string AssetClass_Header { get; set; }
                public string MarketValue_Header { get; set; }
                public string Weight_Header { get; set; }
                public string NotionalValue_Header { get; set; }
                public string Shares_Header { get; set; }
                public string Price_Header { get; set; }
                public string Location_Header { get; set; }
                public string Exchange_Header { get; set; }
                public string Currency_Header { get; set; }
                public string FXRate_Header { get; set; }
                public string MarketCurrency_Header { get; set; }
                public string AccrualDate_Header { get; set; }
                public string[] NumericHeaders => new string[]
                                                {
                                                    MarketValue_Header,
                                                    Weight_Header,
                                                    NotionalValue_Header,
                                                    Shares_Header,
                                                    Price_Header,
                                                    FXRate_Header
                                                };
            }

            public string Root { get; set; }
            public byte Threads { get; set; }
            public byte MaxAttempts { get; set; }
            public int MinStocksInCsv { get; set; }
            public short RequestTimeoutSeconds { get; set; }
            public DateTime FirstAvailableHoldingsDate { get; set; }
            public short SleepSecondsBetweenRequestsMin { get; set; }
            public short SleepSecondsBetweenRequestsMax { get; set; }
            public long AjaxTimestamp { get; set; }
           // public JsonSettings Json { get; set; }
            public CsvSettings Csv { get; set; }
        }

        public class StorageSettings
        {
            public enum StorageType
            {
                Local, S3
            };

            public class LocalSettings
            {
                public string Path { get; set; }
            }

            public class S3Settings
            {
                public string Path { get; set; }
                public string BucketName { get; set; }
                public string Region { get; set; }
                public string AccessKeyId { get; set; }
                public string SecretAccessKey { get; set; }
            }

            public string Active { get; set; }
            public string FileNameSeparator { get; set; }
            public LocalSettings Local { get; set; }
            public S3Settings S3 { get; set; }
            public StorageType GetStorageType() => Active == nameof(Local) ? StorageType.Local :
                                                   Active == nameof(S3) ? StorageType.S3 :
                                                   throw new Exception($"Unknown storage type: '{Active}'");
        }


        public class PostgreSQLSettings
        {
            public string Address { get; set; }
            public int Port { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string DatabaseName { get; set; }
        }

        public StorageSettings Storage { get; set; }
        public iSharesSettings iShares { get; set; }
        public PostgreSQLSettings PostgreSQL { get; set; }
    }
}
