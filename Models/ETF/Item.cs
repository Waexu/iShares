using System.Text.Json.Serialization;

namespace ETF_API.Models.ETF
{
    public class Item
    {
        [JsonConstructor]
        public Item(string Name, short Year, byte Month)
        {
            this.Name = !string.IsNullOrEmpty(Name) ? Name : throw new Exception("ETF Name is required");
            var NameParts = Name.Split(Global.Settings.Storage.FileNameSeparator);
            if (NameParts.Length != 4)
            {
                throw new Exception($"Invalid ETF Name: '{Name}'.");
            }

            this.Id = int.Parse(NameParts[2]);

            var NameUrlFormat = string.Join("/", NameParts);

            this.Year = Year;
            this.Month = Month;
            this.AsOfDate = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            var OffsetDays = this.AsOfDate.DayOfWeek == DayOfWeek.Sunday ? -2 :
                             this.AsOfDate.DayOfWeek == DayOfWeek.Saturday ? -1 : 0;

            if (OffsetDays != default)
            {
                this.AsOfDate = this.AsOfDate.AddDays(OffsetDays);
            }

            var AsOfDateUrlFormat = AsOfDate.ToString("yyyyMMdd");
            this.CsvUrl = $"{Global.Settings.iShares.Root}/{NameUrlFormat}/{Global.Settings.iShares.AjaxTimestamp}.ajax?fileType=csv&fileName=holdings&dataType=fund&asOfDate={AsOfDateUrlFormat}";
            this.JsonUrl = $"{Global.Settings.iShares.Root}/{NameUrlFormat}/{Global.Settings.iShares.AjaxTimestamp}.ajax?tab=all&fileType=json&asOfDate={AsOfDateUrlFormat}";
        }
        public string Name { get; }
        public int Id { get; }
        public short Year { get; }
        public byte Month { get; }
        public DateTime AsOfDate { get; }
        public string CsvUrl { get; }
        public string JsonUrl { get; }

    }
}
