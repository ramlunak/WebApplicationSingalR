using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSingalR.Models
{
    public class Cliente
    {
        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string imagen_url { get; set; }
    }
}
