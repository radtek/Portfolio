using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class AdvancedPurchaseInfo
    {
        private long _cash;
        private long _price;
        private long _count;

        public AdvancedPurchaseInfo()
        { }

        public long Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }

        public long Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public long Count
        {
            get { return _count; }
            set { _count = value; }
        }
    }
}
