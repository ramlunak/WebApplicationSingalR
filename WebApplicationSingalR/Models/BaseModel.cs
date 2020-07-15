using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSingalR.Models
{
    public class BaseModel
    {
        public int id { get; set; }
        public DateTime? create_at { get; set; }
        public DateTime? update_at { get; set; }
        public bool activo { get; set; }

        public BaseModel()
        {
            this.create_at = DateTime.Now;
            this.activo = true;
        }
    }
}
