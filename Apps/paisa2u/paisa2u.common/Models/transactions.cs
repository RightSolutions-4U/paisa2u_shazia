//Added by Mohtashim 03-07-2023
using System;

namespace paisa2u.common.Models
{
    public partial class transactions
    {
        public int Tranid { get; set; }
        public int RegId { get; set; }
        public double Amount { get; set; }
        public DateTime Endate { get; set; }
        public string Enuser { get; set; }
        public string Drcrid { get; set; }
    }
}
