using System.Threading.Tasks;

namespace Conversion.Core.Contracts
{
    public interface IMassConverter
    {
        public Task<decimal> ConvertToKilograms(decimal poundValue);
        public Task<decimal> ConvertToPounds(decimal kilogramValue);
    }
}