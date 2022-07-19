using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using System.Globalization;
using System.Text.Json;

namespace ETF_API.Logic.ETF.Csv
{
    public partial class Merged
    {
        public static string ToString(List<Models.ETF.Csv.Merged.Line> Lines)
        {
            var CsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            using var CsvStringWriter = new StringWriter();
            using var Writer = new CsvWriter(CsvStringWriter, CsvConfiguration);
            Writer.WriteRecords(Lines);
            return CsvStringWriter.ToString();
        }
    }
}
