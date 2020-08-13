using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lolipop
{
    public class LolipopConfiguration
    {
        public static DataEngine Engine { get; set; }
        public static string ConnectionString { get; set; }
    }

    public enum DataEngine
    {
        SqlDataEngine, MySqlDataEngine
    }
}
