using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.CodeGenerator.Core
{
    public class ColumnInfo
    {
        private int _sequence;
        private string _columnname;
        private string _datatype;
        private int _columnlength;
        private int _precisionlength;
        private int _scale;
        private string _defaultvalue;
        private string _columndescription;
        private bool _isidentity;
        private bool _isprimarykey;
        private bool _isnullable;

        public ColumnInfo()
        {
        }

        public ColumnInfo(string columnname)
        {
            _columnname = columnname;
        }

        /// <summary>
        /// �ֶ����
        /// </summary>
        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        /// <summary>
        /// �ֶ���
        /// </summary>
        public string ColumnName
        {
            get { return _columnname; }
            set { _columnname = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string DataType
        {
            get { return _datatype; }
            set { _datatype = value; }
        }

        /// <summary>
        /// ռ���ֽ���
        /// </summary>
        public int ColumnLength
        {
            get { return _columnlength; }
            set { _columnlength = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int PrecisionLength
        {
            get { return _precisionlength; }
            set { _precisionlength = value; }
        }

        /// <summary>
        /// С��λ��
        /// </summary>
        public int Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefaultValue
        {
            get { return _defaultvalue; }
            set { _defaultvalue = value; }
        }

        /// <summary>
        /// �ֶ�˵��
        /// </summary>
        public string ColumnDescription
        {
            get { return _columndescription; }
            set { _columndescription = value; }
        }

        /// <summary>
        /// ��ʶ.
        /// </summary>
        public bool IsIdentity
        {
            get { return _isidentity; }
            set { _isidentity = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return _isprimarykey; }
            set { _isprimarykey = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        public bool IsNullable
        {
            get { return _isnullable; }
            set { _isnullable = value; }
        }

        public override string ToString()
        {
            if (ColumnName == string.Empty)
                return base.ToString();
            else
                return ColumnName;
        }
    }
}
