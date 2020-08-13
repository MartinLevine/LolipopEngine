using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Lolipop.Engine
{
    public class MySqlEngine: LolipopEngine
    {
        public override DbConnection Connection { get; set; } = new MySqlConnection(LolipopConfiguration.ConnectionString);
        public override DbCommand Command { get; set; } = new MySqlCommand();
    }
}
