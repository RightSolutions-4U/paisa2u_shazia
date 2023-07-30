//Added by Mohtashim 03-07-2023
using System;

namespace paisa2u.common.Models
{
    public partial class Transactins
    {
        public int Tranid { get; set; }
        public int RegId { get; set; }
        public float Amount { get; set; }
        public DateTime Endate { get; set; }
        public string Enuser { get; set; }
    }
}
