using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class NewCarInfo
    {
        private int _carId;
        private string _carName;
        private int _carPrice;
        private CarColor _carcolor;

        public int CarId
        {
            get { return this._carId; }
            set { this._carId = value; }
        }

        public string CarName
        {
            get { return this._carName; }
            set { this._carName = value; }
        }

        public int CarPrice
        {
            get { return this._carPrice; }
            set { this._carPrice = value; }
        }

        public CarColor CarColor
        {
            get { return this._carcolor; }
            set { this._carcolor = value; }
        }

        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(this._carName) && (this._carPrice > 0));
            }
        }

        public override string ToString()
        {
            return string.Concat(new object[] { this._carName, "(", this._carId.ToString(), ")", "--¼Û¸ñ£º", this._carPrice.ToString(), "Ôª" });
        }
    }
}
