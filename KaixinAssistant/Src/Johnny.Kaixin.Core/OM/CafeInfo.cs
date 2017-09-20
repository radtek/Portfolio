using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Johnny.Kaixin.Core
{
    public class CafeInfo
    {
        private int _grade;
        private string _gradelabel;
        private string _name;
        private long _cash;
        private int _goldnum;
        private int _evalue;
        private string _cafename;
        private int _cafeid;
        private bool _chef;
        private int _chefmana;
        
        private Collection<CookingInfo> _cookings;
        private Collection<FriendInfo> _employees;
        private Collection<DinnerTableInfo> _dinnertables;

        public CafeInfo()
        {
            _chef = false;
            _chefmana = 0;
        }

        public int Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        public string GradeLabel
        {
            get { return _gradelabel; }
            set { _gradelabel = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public long Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }

        public int GoldNum
        {
            get { return _goldnum; }
            set { _goldnum = value; }
        }

        public int Evalue
        {
            get { return _evalue; }
            set { _evalue = value; }
        }

        public string CafeName
        {
            get { return _cafename; }
            set { _cafename = value; }
        }

        public int CafeId
        {
            get { return _cafeid; }
            set { _cafeid = value; }
        }

        public bool Chef
        {
            get { return _chef; }
            set { _chef = value; }
        }

        public int ChefMana
        {
            get { return _chefmana; }
            set { _chefmana = value; }
        }

        public Collection<CookingInfo> Cookings
        {
            get { return _cookings; }
            set { _cookings = value; }
        }

        public Collection<FriendInfo> Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }

        public Collection<DinnerTableInfo> DinnerTables
        {
            get { return _dinnertables; }
            set { _dinnertables = value; }
        }
    }
}
