using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class BreedAnimalInfo
    {
        //<data>
        //  <ret>succ</ret>
        //  <succtips>���«��ĸ�������Կ˹������ڲ��������ֳɹ�!&lt;br&gt;24Сʱ�ڽ����²񼦵���ÿֻ30Ԫ��</succtips>
        //  <bproduct>0</bproduct>
        //  <leftptime>0</leftptime>
        //  <tips>������&lt;font color='#FF0000'&gt;(������)&lt;/font&gt;&lt;br&gt;�����´β�����11��&lt;br&gt;Ԥ�Ʋ�����10&lt;br&gt;&lt;font color='#666666'&gt;�಻�ܲ�������16Сʱ52��&lt;/font&gt;</tips>
        //  <skey>hen</skey>
        //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic>
        //  <tname>&lt;img src='http://img.kaixin001.com.cn//i2/house/ranch/animals/hen_logo1.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;«��ĸ��</tname>
        //  <animalsid>2441805</animalsid>
        //</data>

        private string _ret;
        private string _succtips;
        private int _bproduct;
        private int _leftptime;
        private string _tips;
        private string _skey;
        private string _pic;
        private string _tname;
        private int _animalsid;

        public BreedAnimalInfo()
        { }

        public string Ret
        {
            get { return _ret; }
            set { _ret = value; }
        }

        public string Succtips
        {
            get { return _succtips; }
            set { _succtips = value; }
        }

        public int BProduct
        {
            get { return _bproduct; }
            set { _bproduct = value; }
        }

        public int LeftPTime
        {
            get { return _leftptime; }
            set { _leftptime = value; }
        }

        public string Tips
        {
            get { return _tips; }
            set { _tips = value; }
        }

        public string SKey
        {
            get { return _skey; }
            set { _skey = value; }
        }

        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        public string TName
        {
            get { return _tname; }
            set { _tname = value; }
        }

        public int AnimalSid
        {
            get { return _animalsid; }
            set { _animalsid = value; }
        }

    }
}
