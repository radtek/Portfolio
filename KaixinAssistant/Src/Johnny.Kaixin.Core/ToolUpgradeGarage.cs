using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.Core
{
    public class ToolUpgradeGarage : GamePark
    {
        public Collection<AccountInfo> _accounts;
        public bool _upgrade;
        public bool _buycars;
        public int _maxcars;
        public int _allmaxcars;
        public Collection<NewCarInfo> _carsInMarket;
        public Collection<int> _blackbuylist;
        public bool _cheapest;

        public ToolUpgradeGarage()
        {
            base.Caption = Constants.TOOL_UPGRADEGARAGE;
            base.Key = Constants.TOOL_UPGRADEGARAGE;
            base.Proxy = ConfigCtrl.GetProxy();
            base.Delay = ConfigCtrl.GetDelay();
            base.Initial();
        }

        public void UpgradeGarageByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(UpgradeGarage));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void UpgradeGarage()
        {
            base.UpgradeGarage(_accounts, _upgrade, _buycars, _maxcars, _allmaxcars, _carsInMarket, _blackbuylist, _cheapest);
        }
    }
}
