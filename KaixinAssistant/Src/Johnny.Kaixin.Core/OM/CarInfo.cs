using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class CarInfo
    {
        // Fields
        public int CarId;
        public string CarName;
        public int CarPrice;
        public bool IsPosted;
        public int ParkingMinutes;
        public int Profit;
        public int ParkUserId;
        public string ParkUserName;
        public CarColor CarColor;

        // Methods
        public CarInfo()
        {
            
        }
       
        public override string ToString()
        {
            return string.Concat(new object[] { CarName, "(", CarId.ToString(), ")", "--¼Û¸ñ£º", CarPrice.ToString(), "Ôª" });
        }

        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(CarName) && (CarPrice > 0));
            }
        }
    }
}
