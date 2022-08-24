using System;
using System.Threading.Tasks;
using Conversion.Core.Contracts;

namespace Conversion.Core.Converters
{
    public class MassConverter : IMassConverter
    {
        public Task<decimal> ConvertToKilograms(decimal poundValue)
        {
            var result = Math.Round(poundValue / 2.20462m, 2);
            return Task.FromResult(result);
        }

        public Task<decimal> ConvertToPounds(decimal kilogramValue)
        {
            var result = Math.Round(kilogramValue * 2.20462m, 2);
            return Task.FromResult(result);
        }
    }
}
