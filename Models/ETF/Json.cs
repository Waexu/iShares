﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace ETF_API.Models.ETF
{
    public class Json
    {
        public class Raw
        {
            public List<List<dynamic>> aaData { get; set; }
        }
        
        public class Formatted
        {
            public string Ticker { get; set;  }
            public string ISIN { get; set; }
            public string SEDOL { get; set; }
            public string CUSIP { get; set; }
        }

        public class Schema
        {
            public byte Ticker_Index { get; set; }
            public byte ISIN_Index { get; set; }
            public byte SEDOL_Index { get; set; }
            public byte CUSIP_Index { get; set; }
        }
    }
}
