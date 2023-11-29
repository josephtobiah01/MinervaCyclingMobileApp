using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Models.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MinervaCyclingMobileApp.Models.ApiResponse.ShopDetailsResponse;

namespace MinervaCyclingMobileApp.Services
{
    public class GetShopsService : IGetShopsService
    {

        public List<ShopDetails> GetShops()
        {

            var response = GetData();
            

            return response;
        }

        public List<ShopDetails> GetData()
        {
            ShopDetails model = new ShopDetails();

            var shopDetailsList = new List<ShopDetails>
            {
                new ShopDetails{ShopName = "Bruges Shop", ShopId = "1"},
                new ShopDetails{ShopName = "Brussels Shop", ShopId = "2"},
                new ShopDetails{ShopName = "Namur Shop", ShopId = "3"},
                new ShopDetails{ShopName = "Liege Shop", ShopId = "4"},
                new ShopDetails{ShopName = "Charleroi Shop", ShopId = "5"},
                new ShopDetails{ShopName = "Leuven Shop", ShopId = "6"},
                new ShopDetails{ShopName = "Ghent Shop", ShopId = "7"},
                new ShopDetails{ShopName = "Ostend Shop", ShopId = "8"},
                new ShopDetails{ShopName = "Antwerp Shop", ShopId = "9"},
            };

            

            return shopDetailsList;

        }
    }
}
