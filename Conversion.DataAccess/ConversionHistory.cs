using System;
using Conversion.DataAccess.Models;

namespace Conversion.DataAccess
{
    public class ConversionHistory
    {
        public long HistoryId { get; set; }
        public string ConversionType { get; set; }
        public string ConversionFrom { get; set; }
        public string ConversionTo { get; set; }
        public decimal ValueToConvert { get; set; }
        public decimal ConvertedResult { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}