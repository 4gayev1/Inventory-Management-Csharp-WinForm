using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InventoryMgmtTuto
{
    public  class AppConnection
    {
        public  string ConnectionString => ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
        
    }
}
