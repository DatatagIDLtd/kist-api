using kist_api.Realtime;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Services.RealtimeService
{
    public class RealtimeService : IRealtimeService
    {
        private readonly IHubContext<AssetHub> hubContext;

        public RealtimeService(IHubContext<AssetHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task NotifyAssetsUpdated()
        {
            await this.hubContext.Clients.All.SendAsync("AssetsChanged");
        }
    }
}
