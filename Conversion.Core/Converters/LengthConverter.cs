using System;
using System.Threading.Tasks;
using Conversion.Core.Contracts;

namespace Conversion.Core.Converters
{
    public class LengthConverter : ILengthConverter
    {
        public Task<decimal> ConvertToKilometers(decimal mileValue)
        {
            var result = Math.Round(mileValue / 0.62137m, 4);
            return Task.FromResult(result);
        }

        public Task<decimal> ConvertToMiles(decimal kilometerValue)
        {
            var result = Math.Round(kilometerValue * 0.62137m, 4);
            return Task.FromResult(result);
        }
    }
}