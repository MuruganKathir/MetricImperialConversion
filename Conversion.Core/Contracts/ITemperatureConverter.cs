using System.Threading.Tasks;

namespace Conversion.Core.Contracts
{
    public interface ITemperatureConverter
    {
        public Task<decimal> ConvertToCelsius(decimal fahrenheitValue);
        public Task<decimal> ConvertToFahrenheit(decimal celsiusValue);
    }
}