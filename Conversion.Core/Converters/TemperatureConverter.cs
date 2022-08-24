using System;
using System.Threading.Tasks;
using Conversion.Core.Contracts;

namespace Conversion.Core.Converters
{
    public class TemperatureConverter : ITemperatureConverter
    {
        public Task<decimal> ConvertToCelsius(decimal fahrenheitValue)
        {
            var result = Math.Round((fahrenheitValue - 32) * 5 / 9, 4);
            return Task.FromResult(result);
        }

        public Task<decimal> ConvertToFahrenheit(decimal celsiusValue)
        {
            var result = Math.Round((celsiusValue * 9 / 5) + 32, 4);
            return Task.FromResult(result);
        }
    }
}