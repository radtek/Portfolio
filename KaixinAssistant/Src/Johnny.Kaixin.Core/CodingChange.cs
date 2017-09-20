using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class WordConvert
    {
        public static string ToUnicode(string character)
        {
            string coding = "";
            for (int i = 0; i < character.Length; i++)
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(character.Substring(i, 1)); //ȡ�������Ʊ�������
                string lowCode = System.Convert.ToString(bytes[0], 16); //ȡ�����ֽڱ������ݣ���λ16���ƣ�
                if (lowCode.Length == 1)
                    lowCode = "0" + lowCode;
                string hightCode = System.Convert.ToString(bytes[1], 16);//ȡ�����ֽڱ������ݣ���λ16���ƣ�
                if (hightCode.Length == 1)
                    hightCode = "0" + hightCode;
                coding += "\\u" + hightCode + lowCode;//���뵽�ַ�����,
            }
            return coding;
        }
        public static string ToChinese(string coding)
        {
            int pos = 0;
            string characters = "";
            string word;
            for (int ix = 0; ix < coding.Length; ix++)
            {
                pos = coding.IndexOf("\\u", ix);
                if ((pos - ix) == 0)
                {
                    word = CodeToWord(coding.Substring(pos + 2, 4)); //ÿ��λΪһ������
                    characters += word;
                    ix += 5; //�ƶ����ú��ֺ�һ��λ�á�
                }
                else if ((pos-ix) > 0)
                {
                    characters += coding.Substring(ix, pos - ix);
                    word = CodeToWord(coding.Substring(pos + 2, 4)); //ÿ��λΪһ������
                    characters += word;
                    ix += 5 + pos - ix; //�ƶ����ú��ֺ�һ��λ�á� 
                }
                else
                {
                    characters += coding.Substring(ix, coding.Length - ix);
                    ix += coding.Length - ix;
                }
            }
            return characters;
        }

        private static string CodeToWord(string code)
        {
            if (code == null || code == string.Empty)
                return "";

            byte[] bytes = new byte[2];
            string highCode = code.Substring(2, 2); //ȡ�����ֽ�,����16���ƽ���ת��
            bytes[0] = System.Convert.ToByte(highCode, 16);
            string lowCode = code.Substring(0, 2); //ȡ�����ֽ�,����16���ƽ���ת��
            bytes[1] = System.Convert.ToByte(lowCode, 16);
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        //public static string ToUnicode(string character)
        //{
        //    string coding = "";
        //    for (int i = 0; i < character.Length; i++)
        //    {
        //        byte[] bytes = System.Text.Encoding.Unicode.GetBytes(character.Substring(i, 1)); //ȡ�������Ʊ�������
        //        string lowCode = System.Convert.ToString(bytes[0], 16); //ȡ�����ֽڱ������ݣ���λ16���ƣ�
        //        if (lowCode.Length == 1)
        //            lowCode = "0" + lowCode;
        //        string hightCode = System.Convert.ToString(bytes[1], 16);//ȡ�����ֽڱ������ݣ���λ16���ƣ�
        //        if (hightCode.Length == 1)
        //            hightCode = "0" + hightCode;
        //        coding += "\\u" + hightCode + lowCode;//���뵽�ַ�����,
        //    }
        //    return coding;
        //}

        //public static string ToChinese(string coding)
        //{
        //    coding = coding.Replace("\\u", "");
        //    string characters = "";
        //    if (coding.Length % 4 != 0)//����Ϊ16����,����Ϊ4�ı�����
        //    {
        //        return "";
        //    }
        //    for (int i = 0; i < coding.Length; i += 4) //ÿ��λΪһ������
        //    {
        //        byte[] bytes = new byte[2];
        //        string highCode = coding.Substring(i + 2, 2); //ȡ�����ֽ�,����16���ƽ���ת��
        //        bytes[0] = System.Convert.ToByte(highCode, 16);
        //        string lowCode = coding.Substring(i, 2); //ȡ�����ֽ�,����16���ƽ���ת��
        //        bytes[1] = System.Convert.ToByte(lowCode, 16);
        //        string character = System.Text.Encoding.Unicode.GetString(bytes);
        //        characters += character;
        //    }
        //    return characters;
        //}

    }
}
