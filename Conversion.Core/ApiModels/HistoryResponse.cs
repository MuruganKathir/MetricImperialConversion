using System;

namespace Conversion.Core.ApiModels
{
  
    public class HistoryResponse
    {
        public long HistoryId { get; set; }
        public string ConversionType { get; set; }
        public string ConversionFrom { get; set; }
        public string ConversionTo { get; set; }
        public decimal ValueToConvert { get; set; }
        public decimal ConvertedResult { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
