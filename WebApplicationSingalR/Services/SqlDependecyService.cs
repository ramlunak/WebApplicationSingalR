using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSingalR.Hubs;

namespace WebApplicationSingalR.Services
{
    public interface IDbChangesNotifService
    {
        void Config();
    }

    public class SqlDependecyService : IDbChangesNotifService
    {

        private readonly IConfiguration configuration;
        private readonly IHubContext<ChatHub> chatHub;

        public void Config()
        {
            throw new NotImplementedException();
        }
    }
}
