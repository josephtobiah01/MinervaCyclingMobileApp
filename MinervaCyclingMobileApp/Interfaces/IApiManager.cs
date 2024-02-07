using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Interfaces
{
    public interface IApiManager
    {
        Task<T> PostForResponseAsync<T>(string uriRequest, StringContent content, CancellationToken cancellationToken = default);
        Task<T> SendForResponseAsync<T>(string uriRequest, HttpMethod method, StringContent content, CancellationToken cancellationToken = default);
        Task<T> GetForResponseAsync<T>(string uriRequest, CancellationToken cancellationToken = default);
    }
}
