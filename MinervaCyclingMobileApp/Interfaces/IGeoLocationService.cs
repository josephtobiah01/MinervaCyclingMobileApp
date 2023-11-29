using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Interfaces
{
    public interface IGeoLocationService
    {
        event Action<Location> LocationChanged;
        Task StartListeningAsync(CancellationToken cancellationToken);

        Task<Location> SnapToRoadAsync(Location location);
    }
}
