using System;

namespace Johnny.CMS.OM.SystemInfo
{
    /// <summary>
    /// Entity Class Breviarysettings
    /// </summary>
    [Serializable]
    public class BreviarySettings
    {
        #region declaration
        private string _TableName = "cms_breviarysettings";
        private string _PrimaryKey = "Id";
        private bool _IsDesc = false;
        private int _id;
        private int _width;
        private int _height;
        private bool _pluswatermark;
        private bool _watermarktype;
        private string _watermarkimage;
        private int _imagetransparent;
        private string _watermarktext;
        private int _texttransparent;
        private int _watermarkposition;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public BreviarySettings() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        public BreviarySettings(int id, int width, int height, bool pluswatermark, bool watermarktype, string watermarkimage, int imagetransparent, string watermarktext, int texttransparent, int watermarkposition)
        {
            this._id = id;
            this._width = width;
            this._height = height;
            this._pluswatermark = pluswatermark;
            this._watermarktype = watermarktype;
            this._watermarkimage = watermarkimage;
            this._imagetransparent = imagetransparent;
            this._watermarktext = watermarktext;
            this._texttransparent = texttransparent;
            this._watermarkposition = watermarkposition;
        }
        #endregion

        #region property
        /// <summary>
        /// TableName
        /// </summary>
        public string TableName
        {
            get { return _TableName; }
        }
        /// <summary>
        /// PrimaryKey
        /// </summary>
        public string PrimaryKey
        {
            get { return _PrimaryKey; }
        }
        /// <summary>
        /// IsDesc
        /// </summary>
        public bool IsDesc
        {
            get { return _IsDesc; }
        }
        /// <summary>
        /// ��ţ��Զ���1��
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ����ͼ���
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        /// <summary>
        /// ����ͼ�߶�
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        /// <summary>
        /// ���ˮӡ
        /// </summary>
        public bool PlusWatermark
        {
            get { return _pluswatermark; }
            set { _pluswatermark = value; }
        }
        /// <summary>
        /// ˮӡ���ͣ�ͼƬˮӡ,����ˮӡ 
        /// </summary>
        public bool WatermarkType
        {
            get { return _watermarktype; }
            set { _watermarktype = value; }
        }
        /// <summary>
        /// ˮӡͼƬ
        /// </summary>
        public string WatermarkImage
        {
            get { return _watermarkimage; }
            set { _watermarkimage = value; }
        }
        /// <summary>
        /// ͼƬˮӡ͸����
        /// </summary>
        public int ImageTransparent
        {
            get { return _imagetransparent; }
            set { _imagetransparent = value; }
        }
        /// <summary>
        /// ˮӡ����
        /// </summary>
        public string WatermarkText
        {
            get { return _watermarktext; }
            set { _watermarktext = value; }
        }
        /// <summary>
        /// ����ˮӡ͸����
        /// </summary>
        public int TextTransparent
        {
            get { return _texttransparent; }
            set { _texttransparent = value; }
        }
        /// <summary>
        /// ˮӡλ�ã����ϣ����У����£����ϣ����У����£����ϣ����У�����
        /// </summary>
        public int WatermarkPosition
        {
            get { return _watermarkposition; }
            set { _watermarkposition = value; }
        }
        #endregion
    }
}
