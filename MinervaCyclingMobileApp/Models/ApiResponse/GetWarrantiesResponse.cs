﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Models.ApiResponse
{
    public class GetWarrantiesResponse : BaseResponse
    {
        public string Name { get; set; }
        public long ExternIdServiceBike { get; set; }
    }
}
