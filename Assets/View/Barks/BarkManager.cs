using Model.Events.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.View.Barks
{
    public class BarkManager
    {
        private BarkManager() {}

        private static BarkManager _instance;
        public static BarkManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BarkManager();
                return _instance;
            }
        }

        public void ProcessFatalityBark(DisplayHitStatsEvent e)
        {

        }
    }
}
