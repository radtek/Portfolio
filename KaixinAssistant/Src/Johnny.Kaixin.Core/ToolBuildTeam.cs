using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.Core
{
    public class ToolBuildTeam : GamePark
    {
        public AccountInfo _account;
        public NewCarInfo _modelcar;
        public int _maxcarcount;
        public ExchangeCar _exchange;
        public Collection<NewCarInfo> _carsInMarket;

        public ToolBuildTeam(AccountInfo account)
        {
            base.Caption = Constants.TOOL_BUILDTEAM;
            base.Key = Constants.TOOL_BUILDTEAM;
            base.CurrentAccount = account;
            base.Proxy = ConfigCtrl.GetProxy();
            base.Delay = ConfigCtrl.GetDelay();
            base.Initial();
        }

        public void BuildTeamByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(BuildTeam));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void BuildTeam()
        {
            base.BuildTeam(_account, _modelcar, _maxcarcount, _exchange, _carsInMarket);
        }
        
    }
}
