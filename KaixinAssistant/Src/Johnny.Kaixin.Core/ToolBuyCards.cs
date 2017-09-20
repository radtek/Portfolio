using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.Core
{
    public class ToolBuyCards : GamePark
    {
        public Collection<AccountInfo> _accounts;
        public CardInfo _card;
        public int _count;

        public ToolBuyCards()
        {
            base.Caption = Constants.TOOL_BUYCARDS;
            base.Key = Constants.TOOL_BUYCARDS;
            base.Proxy = ConfigCtrl.GetProxy();
            base.Delay = ConfigCtrl.GetDelay();
            base.Initial();
        }

        public void BuyCardsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(BuyCards));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void BuyCards()
        {
            base.BuyCards(_accounts, _card, _count);
        }
    }
}
