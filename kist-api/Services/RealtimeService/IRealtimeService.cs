using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Services.RealtimeService
{
    public interface IRealtimeService
    {
        Task NotifyAssetsUpdated();
    }
}
