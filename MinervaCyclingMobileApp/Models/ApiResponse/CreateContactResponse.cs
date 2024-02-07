using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Models.ApiResponse
{
    public class CreateContactResponse : BaseResponse
    {
        public long? Title { get; set; }

        public string Name { get; set; }
    
        public bool? Active { get; set; }
 
        public string Lang { get; set; }

        public string Street { get; set; }

        public string Street2 { get; set; }
      
        public string Zip { get; set; }
      
        public string City { get; set; }
      
        public long? CountryId { get; set; }

        public double? PartnerLatitude { get; set; }

        public double? PartnerLongitude { get; set; }
      
        public string Email { get; set; }
    }
}
