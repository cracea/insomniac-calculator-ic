using Insomniac.Calculator.Data.Models;

namespace Insomniac.Calculator.API.Models
{
    public readonly struct PostalCodeDto(PostalCode postalCode)
    {
        public string Code { get; } = postalCode.Code;
        public string Calculator { get; } = postalCode.Calculator.ToString();
    }

}