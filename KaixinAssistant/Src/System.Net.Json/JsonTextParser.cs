using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace System.Net.Json
{
    public sealed class JsonTextParser
    {
        // Fields
        private static readonly Regex _regexLiteral;
        private static readonly Regex _regexNumber;
        private int c;
        private string s;
        private object SyncObject;

        // Methods
        static JsonTextParser()
        {
            _regexNumber = new Regex("(?<minus>[-])?(?<int>(0)|([1-9])[0-9]*)(?<frac>.[0-9]+)?(?<exp>(e|E)([-]|[+])[0-9]+)?", RegexOptions.Compiled);
            _regexLiteral = new Regex("(?<value>false|true|null)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public JsonTextParser()
        {
            this.s = string.Empty;
            this.SyncObject = new object();
        }

        public JsonObject Parse(string text)
        {
            JsonObject obj2;
            lock (this.SyncObject)
            {
                this.c = 0;
                if (text == null)
                {
                    throw new FormatException();
                }
                this.s = text.Trim();
                if (this.s == string.Empty)
                {
                    throw new FormatException();
                }
                try
                {
                    obj2 = this.ParseSomethingWithoutName();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return obj2;
        }

        private JsonCollection ParseCollection()
        {
            JsonCollection jsons;
            string str;
            this.SkipWhiteSpace();
            bool flag = false;
            if (this.s[this.c] == '{')
            {
                flag = false;
                jsons = new JsonObjectCollection();
            }
            else
            {
                if (this.s[this.c] != '[')
                {
                    throw new FormatException();
                }
                flag = true;
                jsons = new JsonArrayCollection();
            }
            this.c++;
            this.SkipWhiteSpace();
        Label_0060:
            str = string.Empty;
            if (!flag)
            {
                str = this.ParseName();
            }
            JsonObject item = this.ParseSomethingWithoutName();
            if (item == null)
            {
                throw new Exception();
            }
            if (!flag)
            {
                item.Name = str;
            }
            jsons.Add(item);
            this.SkipWhiteSpace();
            if (this.s[this.c] == ',')
            {
                this.c++;
                this.SkipWhiteSpace();
                goto Label_0060;
            }
            this.SkipWhiteSpace();
            if (flag)
            {
                if (this.s[this.c] != ']')
                {
                    throw new FormatException();
                }
            }
            else if (this.s[this.c] != '}')
            {
                throw new FormatException();
            }
            this.c++;
            return jsons;
        }

        private JsonBooleanValue ParseLiteralValue()
        {
            Match match = _regexLiteral.Match(this.s, this.c);
            if (!match.Success)
            {
                throw new FormatException("Cannot parse a literal value.");
            }
            string str = match.Captures[0].Value;
            this.c += str.Length;
            return new JsonBooleanValue(null, str);
        }

        private string ParseName()
        {
            if (this.IsEOS)
            {
                throw new FormatException("Cannot find object item's name.");
            }
            if (this.s[this.c] != '"')
            {
                throw new FormatException();
            }
            this.c++;
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                if (this.IsEOS)
                {
                    throw new FormatException();
                }
                if ((this.s[this.c] == '"') && (this.s[this.c - 1] != '\\'))
                {
                    break;
                }
                builder.Append(this.s[this.c]);
                this.c++;
            }
            this.c++;
            this.SkipWhiteSpace();
            if (this.IsEOS)
            {
                throw new FormatException();
            }
            if (this.s[this.c] != ':')
            {
                throw new FormatException();
            }
            this.c++;
            return builder.ToString();
        }

        private JsonNumericValue ParseNumericValue()
        {
            Match match = _regexNumber.Match(this.s, this.c);
            if (!match.Success)
            {
                throw new FormatException("Cannot parse a number value.");
            }
            string s = match.Captures[0].Value;
            this.c += s.Length;
            return new JsonNumericValue(double.Parse(s, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, JsonUtility.CultureInfo));
        }

        private JsonObject ParseSomethingWithoutName()
        {
            this.SkipWhiteSpace();
            if ((this.s[this.c] == '{') | (this.s[this.c] == '['))
            {
                return this.ParseCollection();
            }
            if (this.s[this.c] == '"')
            {
                return this.ParseStringValue();
            }
            if (char.IsDigit(this.s[this.c]) || (this.s[this.c] == '-'))
            {
                return this.ParseNumericValue();
            }
            if (((this.s[this.c] != 't') && (this.s[this.c] != 'f')) && (this.s[this.c] != 'n'))
            {
                throw new FormatException("Cannot parse a value.");
            }
            return this.ParseLiteralValue();
        }

        private JsonStringValue ParseStringValue()
        {
            if (this.s[this.c] != '"')
            {
                throw new FormatException();
            }
            this.c++;
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                if (this.IsEOS)
                {
                    throw new FormatException();
                }
                if ((this.s[this.c] == '"') && (this.s[this.c - 1] != '\\'))
                {
                    break;
                }
                builder.Append(this.s[this.c]);
                this.c++;
            }
            if (this.s[this.c] != '"')
            {
                throw new FormatException();
            }
            this.c++;
            JsonStringValue value2 = new JsonStringValue();
            value2.Value = JsonUtility.UnEscapeString(builder.ToString());
            return value2;
        }

        private void SkipWhiteSpace()
        {
            while (true)
            {
                if (this.IsEOS || !char.IsWhiteSpace(this.s[this.c]))
                {
                    break;
                }
                this.c++;
            }
            if (this.IsEOS)
            {
                throw new FormatException();
            }
        }

        // Properties
        private char cur
        {
            get
            {
                return this.s[this.c];
            }
        }

        private bool IsEOS
        {
            get
            {
                return (this.c >= this.s.Length);
            }
        }
    }
}
