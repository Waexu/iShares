using System.Globalization;

namespace ETF_API.Models
{
    public class Settings
    {
        public class iSharesSettings
        {
            public class JsonSettings
            {
                public short PropertiesQuantity { get; set; }
                public byte Ticker_Index { get; set; }
                public byte ISIN_Index { get; set; }
                public byte SEDOL_Index { get; set; }
                public byte CUSIP_Index { get; set; }
            }

            public class CsvSettings
            {
                public byte Ticker_Index { get; set; }
                public byte Name_Index { get; set; }
                public byte Sector_Index { get; set; }
                public byte AssetClass_Index { get; set; }
                public byte MarketValue_Index { get; set; }
                public byte Weight_Index { get; set; }
                public byte NotionalValue_Index { get; set; }
                public byte Shares_Index { get; set; }
                public byte Price_Index { get; set; }
                public byte Location_Index { get; set; }
                public byte Exchange_Index { get; set; }
                public byte Currency_Index { get; set; }
                public byte FXRate_Index { get; set; }
                public byte MarketCurrency_Index { get; set; }
                public byte AccrualDate_Index { get; set; }
                public byte[] NumericIndexes => new byte[] 
                                                { 
                                                    MarketValue_Index,
                                                    Weight_Index,
                                                    NotionalValue_Index,
                                                    Shares_Index,
                                                    Price_Index,
                                                    FXRate_Index
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
            public JsonSettings Json { get; set; }
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
