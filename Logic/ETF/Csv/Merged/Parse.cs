using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using System.Globalization;
using System.Text.Json;

namespace ETF_API.Logic.ETF.Csv
{
    public partial class Merged
    {
        public static List<Models.ETF.Csv.Merged.Line> Parse(string Raw, bool HasHeaderRecord)
        {
            var CsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = HasHeaderRecord
            };

            using var CsvStringReader = new StringReader(Raw);
            using var SourceCsv = new CsvReader(CsvStringReader, CsvConfiguration);
            var Records = SourceCsv.GetRecords<Models.ETF.Csv.Merged.Line>();
            return Records.ToList();
        }
    }
}
