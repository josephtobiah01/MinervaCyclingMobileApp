using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Models.ApiResponse
{
    public class BaseResponse
    {
        public int Id { get; set; }
        public string UserLang { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
