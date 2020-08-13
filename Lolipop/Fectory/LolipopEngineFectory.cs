using Lolipop.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lolipop.Fectory
{
    public class LolipopEngineFectory
    {
        private LolipopEngineFectory()
        {

        }

        public static LolipopEngine CreateNewEngine(DataEngine engine)
        {
            LolipopEngine en = null;
            switch (engine)
            {
                case DataEngine.MySqlDataEngine:
                    en = new MySqlEngine();
                    break;
                case DataEngine.SqlDataEngine:
                    en = new SqlEngine();
                    break;
            }

            return en;
        }
    }
}
