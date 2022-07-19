namespace ETF_API.Models.Database
{
    public partial class Table
    {
        public static readonly Template T_ETF = new ()
        {
            Name = nameof(T_ETF),
            Columns = new List<(string, string)>
            {
                ("Id", "SERIAL PRIMARY KEY"),
                ("Name", "VARCHAR(100) NOT NULL"),
            },
            Additional = ",UNIQUE (\"Name\")"

        };
    }


}
