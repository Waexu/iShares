using System;
using System.Collections.Generic;

namespace ETF_API.Models.Database.EntityFramework
{
    public partial class T_ETF
    {
        public T_ETF()
        {
            T_ETF_DATA = new HashSet<T_ETF_DATA>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<T_ETF_DATA> T_ETF_DATA { get; set; }
    }
}
