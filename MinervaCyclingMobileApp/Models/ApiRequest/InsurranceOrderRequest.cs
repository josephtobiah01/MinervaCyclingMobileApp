using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Models.ApiRequest
{
    public class InsurranceOrderRequest
    {
        public string UserID { get; set; }
        public float CertificationID { get; set; }
        public string InsurrancePackID { get; set; }
        public float Price { get; set; }
    }
}
