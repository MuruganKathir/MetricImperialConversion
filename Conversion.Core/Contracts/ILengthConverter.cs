using System.Threading.Tasks;

namespace Conversion.Core.Contracts
{
    public interface ILengthConverter
    {
        Task<decimal> ConvertToKilometers(decimal mileValue);
        Task<decimal> ConvertToMiles(decimal kilometerValue);
    }
}