namespace ETF_API.Models.Database
{
    public partial class Table
    {
        public class Template
        {
            public string Name;
            public List<(string Name, string DataType)> Columns;
            public string Additional;
            // public string PostCommand;
        }
    }
}
