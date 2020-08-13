using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Lolipop.Engine
{
    public class SqlEngine : LolipopEngine
    {
        public override DbConnection Connection { get; set; } = new SqlConnection(LolipopConfiguration.ConnectionString);
        public override DbCommand Command { get; set; } = new SqlCommand();
    }
}
