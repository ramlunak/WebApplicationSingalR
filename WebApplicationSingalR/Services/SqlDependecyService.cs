using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
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

        private readonly string connectionString ;
        private readonly IHubContext<ChatHub> chatHub;

        public SqlDependecyService(IConfiguration _configuration, IHubContext<ChatHub> _chatHub)
        {
            this.connectionString = _configuration.GetConnectionString("ConnectionString"); 
            this.chatHub = _chatHub;
        }

        public void Config()
        {
            OnClienteChanges();
        }

        private void OnClienteChanges()
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(@"SELECT nombre from [dbo].[Clientes]",conn))
                {
                    cmd.Notification = null;
                    SqlDependency sqlDependency = new SqlDependency(cmd);
                    sqlDependency.OnChange += DboClientes_OnChange;
                    SqlDependency.Start(this.connectionString);
                    cmd.ExecuteReader();
                }

            }
        }

        private void DboClientes_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                string msg = ObterMsg(e);
                chatHub.Clients.All.SendAsync("ReceiveMessage","admin",msg);
            }

            OnClienteChanges();
        }

        private string ObterMsg(SqlNotificationEventArgs e)
        {
            switch (e.Info)
            {                   
                case SqlNotificationInfo.Insert:
                    return "Insert";                  
                case SqlNotificationInfo.Update:
                    return "Update";                   
                case SqlNotificationInfo.Delete:
                    return "Delete";
                default:
                    return "mudança desconhecida";
            }
        }
    }
}
