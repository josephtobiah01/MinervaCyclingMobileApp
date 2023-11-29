using MinervaCyclingMobileApp.Models.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinervaCyclingMobileApp.Models.ApiRequest;

namespace MinervaCyclingMobileApp.Interfaces
{
    public interface ICreateInsuranceOrderService
    {
        Task<InsurranceOrderResponse> CreateInsurranceOrder(InsurranceOrderRequest request);
    }
}
