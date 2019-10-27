/********************************************************************
	created:	17:5:2018   
	filename: 	StringBuilderOperator
	author:		jordenwu
	
	purpose:	StringBuilder封装
*********************************************************************/
using System.Text;
using UnityEngine;

namespace JW.Common
{
    public static class StringBuilderOperator
    {
        private static readonly char _decimalSeparator = '.';
        private static readonly char _groupSeparator = ',';
        private static readonly char _padding = '0';

        //全局只有一个 优化性能
        private static readonly StringBuilder StringBuilderCached = new StringBuilder(256);

        public static StringBuilder Formatter
        {
            get
            {
                StringBuilderCached.Length = 0;
                return StringBuilderCached;
            }
        }

        public static void AppendInt(StringBuilder s, int number)
        {
            if (number < 0)
            {
                s.Append('-');
                number = -number;
            }

            int length = s.Length;
            do
            {
                s.Append((char)(number % 10 + 48));
                number /= 10;
            } while (number > 0);

            Reverse(s, length, s.Length - 1);
        }

        public static void AppendInt(StringBuilder s, int number, int digitCount)
        {
            if (number < 0)
            {
                s.Append('-');
                number = -number;
            }

            int length = s.Length;
            int num = 0;
            do
            {
                s.Append((char)(number % 10 + 48));
                number /= 10;
                num++;
            } while (number > 0);

            while (num++ < digitCount)
            {
                s.Append(_padding);
            }

            Reverse(s, length, s.Length - 1);
        }

        public static void AppendIntGrouped(StringBuilder s, int number)
        {
            if (number < 0)
            {
                s.Append('-');
                number = -number;
            }

            int length = s.Length;
            int num = -1;
            do
            {
                if (++num == 3)
                {
                    s.Append(_groupSeparator);
                    num = 0;
                }
                s.Append((char)(number % 10 + 48));
                number /= 10;
            } while (number > 0);

            Reverse(s, length, s.Length - 1);
        }

        public static void AppendIntGrouped(StringBuilder s, int number, int digitCount)
        {
            if (number < 0)
            {
                s.Append('-');
                number = -number;
            }

            int length = s.Length;
            int num = 0;
            int num2 = -1;
            do
            {
                if (++num2 == 3)
                {
                    s.Append(_groupSeparator);
                    num2 = 0;
                }
                s.Append((char)(number % 10 + 48));
                number /= 10;
                num++;
            } while (number > 0);

            while (num++ < digitCount)
            {
                if (++num2 == 3)
                {
                    s.Append(_groupSeparator);
                    num2 = 0;
                }
                s.Append(_padding);
            }

            Reverse(s, length, s.Length - 1);
        }

        public static void AppendFloat(StringBuilder s, float number, int decimalCount)
        {
            int i = (int)number;
            AppendInt(s, i);
            if (decimalCount <= 0)
            {
                return;
            }

            s.Append(_decimalSeparator);

            number -= i;
            for (i = 0; i < decimalCount; i++)
            {
                number *= 10f;
            }
            i = Mathf.FloorToInt(number);

            AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
        }

        public static void AppendFloat(StringBuilder s, float number, int decimalCount, int digitCount)
        {
            int i = (int)number;
            AppendInt(s, i, digitCount);
            if (decimalCount <= 0)
            {
                return;
            }

            s.Append(_decimalSeparator);

            number -= i;
            for (i = 0; i < decimalCount; i++)
            {
                number *= 10f;
            }
            i = Mathf.FloorToInt(number);

            AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
        }

        public static void AppendFloatGrouped(StringBuilder s, float number, int decimalCount)
        {
            int i = (int)number;
            AppendIntGrouped(s, i);
            if (decimalCount <= 0)
            {
                return;
            }

            s.Append(_decimalSeparator);

            number -= i;
            for (i = 0; i < decimalCount; i++)
            {
                number *= 10f;
            }
            i = Mathf.FloorToInt(number);

            AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
        }

        public static void AppendFloatGrouped(StringBuilder s, float number, int decimalCount, int digitCount)
        {
            int i = (int)number;
            AppendIntGrouped(s, i, digitCount);
            if (decimalCount <= 0)
            {
                return;
            }

            s.Append(_decimalSeparator);

            number -= i;
            for (i = 0; i < decimalCount; i++)
            {
                number *= 10f;
            }
            i = Mathf.FloorToInt(number);

            AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
        }

        private static void Reverse(StringBuilder s, int firstIndex, int lastIndex)
        {
            if (lastIndex < s.Length && firstIndex<s.Length)
            {
                while (firstIndex < lastIndex)
                {
                    char value = s[firstIndex];
                    s[firstIndex++] = s[lastIndex];
                    s[lastIndex--] = value;
                }
            }
        }
    }
}
