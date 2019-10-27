/********************************************************************
	created:	11:7:2017   14:26
	author:		jordenwu
	purpose:	统一的Json 解析类
*********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JW.Common
{
    public static class Json
    {
        private sealed class Parser : IDisposable
        {
            private enum TOKEN
            {
                NONE,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            }
            private const string WORD_BREAK = "{}[],:\"";
            private StringReader json;
            private char PeekChar
            {
                get
                {
                    return Convert.ToChar(this.json.Peek());
                }
            }
            private char NextChar
            {
                get
                {
                    return Convert.ToChar(this.json.Read());
                }
            }
            private string NextWord
            {
                get
                {
                    StringBuilder word = new StringBuilder();
                    while (!Json.Parser.IsWordBreak(this.PeekChar))
                    {
                        word.Append(this.NextChar);
                        if (this.json.Peek() == -1)
                        {
                            break;
                        }
                    }
                    return word.ToString();
                }
            }
            private Json.Parser.TOKEN NextToken                             //定义TOKEY类型属性
            {
                get
                {
                    this.EatWhitespace();
                    Json.Parser.TOKEN result;
                    if (this.json.Peek() == -1)
                    {
                        result = Json.Parser.TOKEN.NONE;
                    }
                    else
                    {
                        char peekChar = this.PeekChar;
                        switch (peekChar)
                        {
                            case '"':
                                result = Json.Parser.TOKEN.STRING;
                                return result;
                            case '#':
                            case '$':
                            case '%':
                            case '&':
                            case '\'':
                            case '(':
                            case ')':
                            case '*':
                            case '+':
                            case '.':
                            case '/':
                                break;
                            case ',':
                                this.json.Read();
                                result = Json.Parser.TOKEN.COMMA;
                                return result;
                            case '-':
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                result = Json.Parser.TOKEN.NUMBER;
                                return result;
                            case ':':
                                result = Json.Parser.TOKEN.COLON;
                                return result;
                            default:
                                switch (peekChar)
                                {
                                    case '[':
                                        result = Json.Parser.TOKEN.SQUARED_OPEN;
                                        return result;
                                    case '\\':
                                        break;
                                    case ']':
                                        this.json.Read();
                                        result = Json.Parser.TOKEN.SQUARED_CLOSE;
                                        return result;
                                    default:
                                        switch (peekChar)
                                        {
                                            case '{':
                                                result = Json.Parser.TOKEN.CURLY_OPEN;
                                                return result;
                                            case '}':
                                                this.json.Read();
                                                result = Json.Parser.TOKEN.CURLY_CLOSE;
                                                return result;
                                        }
                                        break;
                                }
                                break;
                        }
                        string nextWord = this.NextWord;
                        if (nextWord != null)
                        {
                            if (nextWord == "false")
                            {
                                result = Json.Parser.TOKEN.FALSE;
                                return result;
                            }
                            if (nextWord == "true")
                            {
                                result = Json.Parser.TOKEN.TRUE;
                                return result;
                            }
                            if (nextWord == "null")
                            {
                                result = Json.Parser.TOKEN.NULL;
                                return result;
                            }
                        }
                        result = Json.Parser.TOKEN.NONE;
                    }
                    return result;
                }
            }
            public static bool IsWordBreak(char c)
            {
                return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
            }
            private Parser(string jsonString)
            {
                this.json = new StringReader(jsonString);
            }
            public static object Parse(string jsonString)
            {
                object result;
                using (Json.Parser instance = new Json.Parser(jsonString))       //实例化Parser类
                {
                    result = instance.ParseValue();                                 
                }
                return result;
            }
            public void Dispose()
            {
                this.json.Dispose();
                this.json = null;
            }
            private Dictionary<string, object> ParseObject()
            {
                Dictionary<string, object> table = new Dictionary<string, object>();
                this.json.Read();
                while (true)
                {
                    Json.Parser.TOKEN nextToken = this.NextToken;
                    switch (nextToken)
                    {
                        case Json.Parser.TOKEN.NONE:
                            goto IL_35;
                        case Json.Parser.TOKEN.CURLY_OPEN:
                            break;
                        case Json.Parser.TOKEN.CURLY_CLOSE:
                            goto IL_3B;
                        default:
                            if (nextToken == Json.Parser.TOKEN.COMMA)
                            {
                                continue;
                            }
                            break;
                    }
                    string name = this.ParseString();
                    if (name == null)
                    {
                        goto Block_3;
                    }
                    if (this.NextToken != Json.Parser.TOKEN.COLON)
                    {
                        goto Block_4;
                    }
                    this.json.Read();
                    table[name] = this.ParseValue();
                }
            IL_35:
                Dictionary<string, object> result = null;
                return result;
            IL_3B:
                result = table;
                return result;
            Block_3:
                result = null;
                return result;
            Block_4:
                result = null;
                return result;
            }
            private List<object> ParseArray()
            {
                List<object> array = new List<object>();
                this.json.Read();
                bool parsing = true;
                List<object> result;
                while (parsing)
                {
                    Json.Parser.TOKEN nextToken = this.NextToken;
                    Json.Parser.TOKEN tOKEN = nextToken;
                    if (tOKEN == Json.Parser.TOKEN.NONE)
                    {
                        result = null;
                        return result;
                    }
                    switch (tOKEN)
                    {
                        case Json.Parser.TOKEN.SQUARED_CLOSE:
                            parsing = false;
                            break;
                        case Json.Parser.TOKEN.COLON:
                            goto IL_49;
                        case Json.Parser.TOKEN.COMMA:
                            break;
                        default:
                            goto IL_49;
                    }
                    continue;
                IL_49:
                    object value = this.ParseByToken(nextToken);
                    array.Add(value);
                }
                result = array;
                return result;
            }
            private object ParseValue()
            {
                Json.Parser.TOKEN nextToken = this.NextToken;
                return this.ParseByToken(nextToken);
            }
            private object ParseByToken(Json.Parser.TOKEN token)
            {
                object result;
                switch (token)
                {
                    case Json.Parser.TOKEN.CURLY_OPEN:
                        result = this.ParseObject();
                        return result;
                    case Json.Parser.TOKEN.SQUARED_OPEN:
                        result = this.ParseArray();
                        return result;
                    case Json.Parser.TOKEN.STRING:
                        result = this.ParseString();
                        return result;
                    case Json.Parser.TOKEN.NUMBER:
                        result = this.ParseNumber();
                        return result;
                    case Json.Parser.TOKEN.TRUE:
                        result = true;
                        return result;
                    case Json.Parser.TOKEN.FALSE:
                        result = false;
                        return result;
                    case Json.Parser.TOKEN.NULL:
                        result = null;
                        return result;
                }
                result = null;
                return result;
            }
            private string ParseString()
            {
                StringBuilder s = new StringBuilder();
                this.json.Read();
                bool parsing = true;
                while (parsing)
                {
                    if (this.json.Peek() == -1)
                    {
                        break;
                    }
                    char c = this.NextChar;
                    char c2 = c;
                    if (c2 != '"')
                    {
                        if (c2 != '\\')
                        {
                            s.Append(c);
                        }
                        else
                        {
                            if (this.json.Peek() == -1)
                            {
                                parsing = false;
                            }
                            else
                            {
                                c = this.NextChar;
                                c2 = c;
                                if (c2 <= '\\')
                                {
                                    if (c2 == '"' || c2 == '/' || c2 == '\\')
                                    {
                                        s.Append(c);
                                    }
                                }
                                else
                                {
                                    if (c2 <= 'f')
                                    {
                                        if (c2 != 'b')
                                        {
                                            if (c2 == 'f')
                                            {
                                                s.Append('\f');
                                            }
                                        }
                                        else
                                        {
                                            s.Append('\b');
                                        }
                                    }
                                    else
                                    {
                                        if (c2 != 'n')
                                        {
                                            switch (c2)
                                            {
                                                case 'r':
                                                    s.Append('\r');
                                                    break;
                                                case 't':
                                                    s.Append('\t');
                                                    break;
                                                case 'u':
                                                    {
                                                        char[] hex = new char[4];
                                                        for (int i = 0; i < 4; i++)
                                                        {
                                                            hex[i] = this.NextChar;
                                                        }
                                                        s.Append((char)Convert.ToInt32(new string(hex), 16));
                                                        break;
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            s.Append('\n');
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        parsing = false;
                    }
                }
                return s.ToString();
            }
            private object ParseNumber()
            {
                string number = this.NextWord;
                object result;
                if (number.IndexOf('.') == -1)
                {
                    int parsedInt;
                    int.TryParse(number, out parsedInt);
                    result = parsedInt;
                }
                else
                {
                    float parsedDouble;
                    float.TryParse(number, out parsedDouble);
                    result = parsedDouble;
                }
                return result;
            }
            private void EatWhitespace()
            {
                while (char.IsWhiteSpace(this.PeekChar))
                {
                    this.json.Read();
                    if (this.json.Peek() == -1)
                    {
                        break;
                    }
                }
            }
        }
        private sealed class Serializer
        {
            private StringBuilder builder;
            private Serializer()
            {
                this.builder = new StringBuilder();
            }
            public static string Serialize(object obj)
            {
                Json.Serializer instance = new Json.Serializer();
                instance.SerializeValue(obj);
                return instance.builder.ToString();
            }
            private void SerializeValue(object value)
            {
                if (value == null)
                {
                    this.builder.Append("null");
                }
                else
                {
                    string asStr;
                    if ((asStr = (value as string)) != null)
                    {
                        this.SerializeString(asStr);
                    }
                    else
                    {
                        if (value is bool)
                        {
                            this.builder.Append(((bool)value) ? "true" : "false");
                        }
                        else
                        {
                            IList asList;
                            if ((asList = (value as IList)) != null)
                            {
                                this.SerializeArray(asList);
                            }
                            else
                            {
                                IDictionary asDict;
                                if ((asDict = (value as IDictionary)) != null)
                                {
                                    this.SerializeObject(asDict);
                                }
                                else
                                {
                                    if (value is char)
                                    {
                                        this.SerializeString(new string((char)value, 1));
                                    }
                                    else
                                    {
                                        this.SerializeOther(value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            private void SerializeObject(IDictionary obj)
            {
                bool first = true;
                this.builder.Append('{');
                foreach (object e in obj.Keys)
                {
                    if (!first)
                    {
                        this.builder.Append(',');
                    }
                    this.SerializeString(e.ToString());
                    this.builder.Append(':');
                    this.SerializeValue(obj[e]);
                    first = false;
                }
                this.builder.Append('}');
            }
            private void SerializeArray(IList anArray)
            {
                this.builder.Append('[');
                bool first = true;
                foreach (object obj in anArray)
                {
                    if (!first)
                    {
                        this.builder.Append(',');
                    }
                    this.SerializeValue(obj);
                    first = false;
                }
                this.builder.Append(']');
            }
            private void SerializeString(string str)
            {
                this.builder.Append('"');
                char[] charArray = str.ToCharArray();
                char[] array = charArray;
                int i = 0;
                while (i < array.Length)
                {
                    char c = array[i];
                    char c2 = c;
                    switch (c2)
                    {
                        case '\b':
                            this.builder.Append("\\b");
                            break;
                        case '\t':
                            this.builder.Append("\\t");
                            break;
                        case '\n':
                            this.builder.Append("\\n");
                            break;
                        case '\v':
                            goto IL_ED;
                        case '\f':
                            this.builder.Append("\\f");
                            break;
                        case '\r':
                            this.builder.Append("\\r");
                            break;
                        default:
                            if (c2 != '"')
                            {
                                if (c2 != '\\')
                                {
                                    goto IL_ED;
                                }
                                this.builder.Append("\\\\");
                            }
                            else
                            {
                                this.builder.Append("\\\"");
                            }
                            break;
                    }
                IL_145:
                    i++;
                    continue;
                IL_ED:
                    int codepoint = Convert.ToInt32(c);
                    if (codepoint >= 32 && codepoint <= 126)
                    {
                        this.builder.Append(c);
                    }
                    else
                    {
                        this.builder.Append("\\u");
                        this.builder.Append(codepoint.ToString("x4"));
                    }
                    goto IL_145;
                }
                this.builder.Append('"');
            }
            private void SerializeOther(object value)
            {
                if (value is float)
                {
                    this.builder.Append(((float)value).ToString("R"));
                }
                else
                {
                    if (value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong)
                    {
                        this.builder.Append(value);
                    }
                    else
                    {
                        if (value is double || value is decimal)
                        {
                            this.builder.Append(Convert.ToDouble(value).ToString("R"));
                        }
                        else
                        {
                            this.SerializeString(value.ToString());
                        }
                    }
                }
            }
        }
        public static object Deserialize(string json)
        {
            object result;
            if (json == null)
            {
                result = null;
            }
            else
            {
                result = Json.Parser.Parse(json);
            }
            return result;
        }
        public static string Serialize(object obj)
        {
            return Json.Serializer.Serialize(obj);
        }
    }
}
