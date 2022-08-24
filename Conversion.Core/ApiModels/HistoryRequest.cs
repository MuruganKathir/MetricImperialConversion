namespace Conversion.Core.ApiModels
{
    public class HistoryRequest
    {
        public string ConversionType { get; set; }
        public string ConversionFrom { get; set; }
        public string ConversionTo { get; set; }
        public decimal ValueToConvert { get; set; }
        public decimal ConvertedResult { get; set; }
        public string UserId { get; set; }
    }
}