using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Globalization;

namespace SQLitePCL.Functions.core
{
    /// <summary>
    /// This class is meant to be used by the SQLitePCL Raw and Pretty function libraries, and provides
    /// no SQLite functionality on its own.  These are the just the functions that both libraries rely upon.
    /// </summary>
    public class CoreFn
    {
        public static bool RegexIsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        public static bool DateIsValid(string input, string pipedDatePatterns)
        {
            DateTime currentval;
            string[] formats = pipedDatePatterns.Split('|');
            bool parsed = false;
            foreach (var format in formats)
            {
                parsed = DateTime.TryParseExact(input,
                           format,
                           System.Globalization.CultureInfo.InvariantCulture,
                           System.Globalization.DateTimeStyles.None,
                           out currentval);
                if (parsed) break;
            }
            return parsed;
        }

        public static bool IsBool(string input)
        {
            if ((input == "1") || (input == "0"))
                return true;
            bool currentval;
            return Boolean.TryParse(input, out currentval);
        }

        // https://msdn.microsoft.com/en-us/library/system.uri.scheme(v=vs.110).aspx

        public static bool IsUri(string input, string uriScheme)
        {
            Uri outUri;
            var uriTypeVals = uriScheme.Split('|');
            bool parsed = Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out outUri);
            if (parsed && outUri.IsAbsoluteUri && !String.IsNullOrEmpty(uriTypeVals[0]))
                foreach (var v in uriTypeVals)
                {
                    parsed = (outUri.Scheme.ToLower() == v.ToLower());
                    if (parsed) break;
                }
            return parsed;
        }

        public static bool IsInt(string input)
        {
            int currentval;
            return int.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsUint(string input)
        {
            uint currentval;
            return uint.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsLong(string input)
        {
            long currentval;
            return long.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsUlong(string input)
        {
            ulong currentval;
            return ulong.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsByte(string input)
        {
            byte currentval;
            return byte.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsSbyte(string input)
        {
            sbyte currentval;
            return sbyte.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsShort(string input)
        {
            short currentval;
            return short.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsUshort(string input)
        {
            ushort currentval;
            return ushort.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsFloat(string input)
        {
            float currentval;
            return float.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsDouble(string input)
        {
            double currentval;
            return double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsDecimal(string input)
        {
            decimal currentval;
            return decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out currentval);
        }

        public static bool IsChar(string input)
        {
            char currentval;
            return char.TryParse(input, out currentval);
        }

        public static bool IsISO8601Timespan(string input)
        {
            // example: "P1Y2MT2H"
            bool converted = true;
            TimeSpan currentval;
            try
            {
                currentval = XmlConvert.ToTimeSpan(input);
            }
            catch (Exception e)
            {
                converted = false;
            }
            return converted;
        }

        public static bool IsDotNetTimespan(string input)
        {
            // example: "02:12:30"
            TimeSpan currentval;
            return TimeSpan.TryParse(input, out currentval);
        }

        public static bool IsGuid(string input)
        {
            System.Guid currentval;
            return Guid.TryParse(input, out currentval);
        }

        public static bool DateCompare(string input1, string pipedDatePatterns, string comparator, string input2)
        {
            DateTime input = default(DateTime);
            DateTime compval = default(DateTime);
            string[] formats = pipedDatePatterns.Split('|');
            bool parsed = false;
            foreach (var format in formats)
            {
                parsed = DateTime.TryParseExact(input1,
                           format,
                           System.Globalization.CultureInfo.InvariantCulture,
                           System.Globalization.DateTimeStyles.None,
                           out input);
                if (parsed) break;
            }
            if (parsed && input.Year > 1)
            {
                bool parsedComp = false;
                foreach (var format in formats)
                {
                    parsedComp = DateTime.TryParseExact(input2,
                               format,
                               System.Globalization.CultureInfo.InvariantCulture,
                               System.Globalization.DateTimeStyles.None,
                               out compval);
                    if (parsedComp) break;
                }
                if (parsedComp && compval.Year > 1)
                {
                    var op = comparator;
                    if (op == ">") { parsed = (input > compval); }
                    else if (op == ">=") { parsed = (input >= compval); }
                    else if (op == "<") { parsed = (input < compval); }
                    else if (op == "<=") { parsed = (input <= compval); }
                    else if (op == "=") { parsed = (input == compval); }
                    else if (op == "<>") { parsed = (input != compval); }
                }
            }

            return parsed;
        }

        // Comparison Functions
        public static bool CompareVals(string input1, string comparator, string input2, string dataType)
        {
            bool isGreater = false;
            switch (dataType.ToString())
            {
                case "float":
                    float floatVal1, floatVal2;
                    isGreater = float.TryParse(input1, out floatVal1);
                    isGreater &= float.TryParse(input2, out floatVal2);
                    isGreater &= Compare(comparator, floatVal1, floatVal2);
                    break;
                case "double":
                    double doubleVal1, doubleVal2;
                    isGreater = double.TryParse(input1, out doubleVal1);
                    isGreater &= double.TryParse(input2, out doubleVal2);
                    isGreater &= Compare(comparator, doubleVal1, doubleVal2);
                    break;
                case "decimal":
                    decimal decimalVal1, decimalVal2;
                    isGreater = decimal.TryParse(input1, out decimalVal1);
                    isGreater &= decimal.TryParse(input2, out decimalVal2);
                    isGreater &= Compare(comparator, decimalVal1, decimalVal2);
                    break;
                case "int":
                    int intVal1, intVal2;
                    isGreater = int.TryParse(input1, out intVal1);
                    isGreater &= int.TryParse(input2, out intVal2);
                    isGreater &= Compare(comparator, intVal1, intVal2);
                    break;
                case "uint":
                    uint uintVal1, uintVal2;
                    isGreater = uint.TryParse(input1, out uintVal1);
                    isGreater &= uint.TryParse(input2, out uintVal2);
                    isGreater &= Compare(comparator, uintVal1, uintVal2);
                    break;
                case "byte":
                    byte byteVal1, byteVal2;
                    isGreater = byte.TryParse(input1, out byteVal1);
                    isGreater &= byte.TryParse(input2, out byteVal2);
                    isGreater &= Compare(comparator, byteVal1, byteVal2);
                    break;
                case "sbyte":
                    sbyte sbyteVal1, sbyteVal2;
                    isGreater = sbyte.TryParse(input1, out sbyteVal1);
                    isGreater &= sbyte.TryParse(input2, out sbyteVal2);
                    isGreater &= Compare(comparator, sbyteVal1, sbyteVal2);
                    break;
                case "long":
                    long longVal1, longVal2;
                    isGreater = long.TryParse(input1, out longVal1);
                    isGreater &= long.TryParse(input2, out longVal2);
                    isGreater &= Compare(comparator, longVal1, longVal2);
                    break;
                case "ulong":
                    ulong ulongVal1, ulongVal2;
                    isGreater = ulong.TryParse(input1, out ulongVal1);
                    isGreater &= ulong.TryParse(input2, out ulongVal2);
                    isGreater &= Compare(comparator, ulongVal1, ulongVal2);
                    break;
                case "short":
                    short shortVal1, shortVal2;
                    isGreater = short.TryParse(input1, out shortVal1);
                    isGreater &= short.TryParse(input2, out shortVal2);
                    isGreater &= Compare(comparator, shortVal1, shortVal2);
                    break;
                case "ushort":
                    ushort ushortVal1, ushortVal2;
                    isGreater = ushort.TryParse(input1, out ushortVal1);
                    isGreater &= ushort.TryParse(input2, out ushortVal2);
                    isGreater &= Compare(comparator, ushortVal1, ushortVal2);
                    break;
                default:
                    break;
            }
            return isGreater;
        }

        public static Guid GetGuid() { return System.Guid.NewGuid(); }

        public static Dictionary<string, int> RowNumDictionary;

        public static int GetRowNumber(string key)
        {
            if (RowNumDictionary == null)
                RowNumDictionary = new Dictionary<string, int>();

            int retVal = 0;
            if (RowNumDictionary.ContainsKey(key))
                retVal = RowNumDictionary[key];
            RowNumDictionary[key] = retVal + 1;
            return (retVal + 1);
        }


        // XML specific functions
        public static bool IsNonPositiveInt(string input)
        {
            return (IsLong(input) && Convert.ToInt32(input) <= 0);
        }
        public static bool IsNonNegativeInt(string input)
        {
            return (IsLong(input) && Convert.ToInt32(input) >= 0);
        }
        public static bool IsPositiveInt(string input)
        {
            return (IsLong(input) && Convert.ToInt32(input) > 0);
        }
        public static bool IsNegativeInt(string input)
        {
            return (IsLong(input) && Convert.ToInt32(input) < 0);
        }

        public static bool Compare<T>(string comparator, T lhs, T rhs)
            where T : IComparable<T>
        {
            bool comp = false;
            switch (comparator)
            {
                case ">":
                    comp = lhs.CompareTo(rhs) > 0;
                    break;
                case ">=":
                    comp = lhs.CompareTo(rhs) >= 0;
                    break;
                case "<":
                    comp = lhs.CompareTo(rhs) < 0;
                    break;
                case "<=":
                    comp = lhs.CompareTo(rhs) <= 0;
                    break;
                case "<>":
                    comp = lhs.CompareTo(rhs) != 0;
                    break;
                case "=":
                    comp = lhs.CompareTo(rhs) == 0;
                    break;
                default:
                    break;
            }
            return comp;
        }

    }
}
