using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Conversion.DataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ConversionHistory> ConversionHistorys { get; set; }
    }
}