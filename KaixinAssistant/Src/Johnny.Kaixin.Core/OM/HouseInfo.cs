using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class HouseInfo
    {
        private string _roomid;
        private string _housename;
        private string _fuid;
        private HouseStatus _status;
        //private bool _isonlyanotheronelodger;
        private int _lodgercount;
        private int _roomcount;
        private bool _ismasterinhouse;

        public HouseInfo()
        {
            _roomid = "";
            _fuid = "";
            _status = HouseStatus.UnKnown;
            //_isonlyanotheronelodger = false;
            _lodgercount = 0;
            _roomcount = 0;
        }

        public HouseInfo(string roomid, string housename, string fuid, HouseStatus status)
        {
            _roomid = roomid;
            _housename = housename;
            _fuid = fuid;
            _status = status;
        }

        public string RoomId
        {
            get { return _roomid; }
            set { _roomid = value; }
        }

        public string HouseName
        {
            get { return _housename; }
            set { _housename = value; }
        }
        
        public string Fuid
        {
            get { return _fuid; }
            set { _fuid = value; }
        }

        public HouseStatus Status
        {
            //get { return _status; }            
            //set { _status = value; }
            get
            {
                if (_lodgercount == 0)
                    return HouseStatus.NoLodger;
                else if (_lodgercount < _roomcount)
                    return HouseStatus.CanStay;
                else if (_lodgercount == _roomcount)
                    return HouseStatus.Full;
                else
                    return HouseStatus.UnKnown;
            }
        }

        public bool IsOnlyOtherLodgersStayIn
        {
            //get { return _isonlyanotheronelodger; }
            //set { _isonlyanotheronelodger = value; }
            get 
            {
                if (!_ismasterinhouse && _lodgercount == _roomcount - 1)
                    return true;
                else
                    return false;
            }
        }

        public int LodgerCount
        {
            get { return _lodgercount; }
            set { _lodgercount = value; }
        }

        public int RoomCount
        {
            get { return _roomcount; }
            set { _roomcount = value; }
        }

        public bool IsMasterInHouse
        {
            get { return _ismasterinhouse; }
            set { _ismasterinhouse = value; }
        }
    }
}
