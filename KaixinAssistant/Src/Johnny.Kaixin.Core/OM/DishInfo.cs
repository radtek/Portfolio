using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    [Serializable]
    public class DishInfo
    {
        private int _dishid;
        private string _title;
        private int _rank;
        private int _price;

        private decimal _maxprice;
        private decimal _minprice;
        private decimal _sellprice;
        private decimal _purchaseprice;
        private decimal _currentprice;

        public DishInfo()
        { }

        public int DishId
        {
            get { return _dishid; }
            set { _dishid = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal MaxPrice
        {
            get { return _maxprice; }
            set { _maxprice = value; }
        }

        public decimal MinPrice
        {
            get { return _minprice; }
            set { _minprice = value; }
        }

        public decimal SellPrice
        {
            get { return _sellprice; }
            set { _sellprice = value; }
        }

        public decimal PurchasePrice
        {
            get { return _purchaseprice; }
            set { _purchaseprice = value; }
        }

        public decimal CurrentPrice
        {
            get { return _currentprice; }
            set { _currentprice = value; }
        }

        public void Clone(DishInfo source)
        {
            this.DishId = source.DishId;
            this.Title = source.Title;
            this.Rank = source.Rank;
            this.Price = source.Price;
            this.CurrentPrice = source.CurrentPrice;
        }

        public override string ToString()
        {
            return _title + "(" + _price.ToString() + ")";
        }
    }
}
