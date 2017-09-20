using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class CardInfo
    {
        private int _cardid;
        private string _cardname;
        private int _price;

        public CardInfo()
        { }

        public CardInfo(int cardid, string cardname, int price)
        {
            this._cardid = cardid;
            this._cardname = cardname;
            this._price = price;
        }

        public int CardId
        {
            get { return _cardid; }
            set { _cardid = value; }
        }

        public string CardName
        {
            get { return _cardname; }
            set { _cardname = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public override string ToString()
        {
            return _cardname + "(гд" + _price.ToString() + ")";
        }
    }
}
