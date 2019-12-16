using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
  public static class C
  {
    public static long MaxLength { get; set; } = 9223372036854775807;

    public static bool IsNullOrEmpty(this string input)
    {
      return String.IsNullOrEmpty(input);
    }
    public static string DateTimeFormat { get; set; } = "yyyy-MM-dd hh:mm:ss";
    public static string DateFormat { get; set; } = "yyyy-MM-dd";
    public static string DateConvertFormats { get; set; } = "yyyy-MM-ddTHH:mm:ss.ffZ";
    public static T GetAsyncValue<T>(this Task<T> task)
    {
      return Task.Run(async () => await task).ConfigureAwait(false).GetAwaiter().GetResult();
    }
    public static void GetAsyncValue(this Task task)
    {
      Task.Run(async () => await task).ConfigureAwait(false).GetAwaiter().GetResult();
    }
    /// <summary>
    /// 将字符按照camel case 的分隔方式拆开. AA 将被拆分成 A A. 如果为空则返回"", 首字母大写
    /// </summary>
    /// <param name="value">camel case的字符串</param>
    /// <returns>
    /// string
    /// </returns>
    public static string SpacesFromCamel(this string value)
    {
      var returnValue = "";
      if (value.IsNullOrEmpty())
      {
        returnValue = value;
        goto returnvalue;
      }
      if (value.Length >= 2)
      {
        var result = new List<char>();
        result.Add(value[0].ToString().ToUpper()[0]);
        char[] array = value.ToCharArray();
        for (var i = 1; i < value.Length; i++)
        {
          var item = value[i];
          if (char.IsUpper(item))
          {
            result.Add(' ');
          }
          result.Add(item);
        }

        returnValue = new string(result.ToArray());
      }
      else
      {
        returnValue = value.ToUpper();
      }
      returnvalue:
      return returnValue.Text("");
    }
    #region 经常需要把dynamic或者其他乱七八糟的转成字符,trim 或者如果是空取默认值或者第几个字母开始大写等等
    /// <summary>
    /// 如果输入为空则返回默认值
    /// </summary>
    /// <param name="input">检测的字符串</param>
    /// <param name="defaultValue">默认字符串</param>
    /// <param name="captalString">大写选项 -1 为不变 0 为全小写 大于0 则超之前(包括本身)为大写 其余为小写</param>
    /// <param name="trim">是否收空格</param>
    /// <returns></returns>
    public static string Text(this string input, string defaultValue = "", int captalString = -1, bool trim = true)
    {
      var result = input;
      if (String.IsNullOrEmpty(input) || String.IsNullOrWhiteSpace(input))
      {
        result = defaultValue;
      }
      if (trim)
        result = result.Trim();
      return result.CaptalString(captalString);
    }
    /// <summary>
    /// 如果输入为空则返回默认值
    /// </summary>
    /// <param name="input">检测的字符串</param>
    /// <param name="defaultValue">默认字符串</param>
    /// <param name="captalString">大写选项 -1 为不变 0 为全小写 大于0 则超之前(包括本身)为大写 其余为小写</param>
    /// <param name="trim">是否收空格</param>
    /// <returns></returns>
    public static string Text(this object input, string defaultValue = "", int captalString = -1, bool trim = true)
    {
      var result = "";
      if (input == null)
      {
        result = defaultValue;
      }
      else
      {
        result = Convert.ToString(input);
      }
      return result.Text(defaultValue, captalString, trim);
    }
    /// <summary>
    /// 将字符串大写
    /// </summary>
    /// <param name="input">输入字符 如果为空则返回""</param>
    /// <param name="captalString">开始大写的字符(1 based index)</param>
    /// <returns></returns>
    public static string CaptalString(this string input, int captalString = -1)
    {
      if (String.IsNullOrEmpty(input))
      {
        input = "";
      }
      if (captalString < 0)
        return input;
      if (captalString == 0)
        return input.ToLower();
      if (captalString >= input.Length)
        return input.ToUpper();
      return $"{input.Substring(0, captalString).ToUpper()}{input.Substring(captalString - 1, input.Length - captalString).ToLower()}";
    }
    #endregion

    public static int Int32(this string input, int defaultValue = 0)
    {
      if (String.IsNullOrEmpty(input))
      {
        return defaultValue;
      }
      if (System.Int32.TryParse(input, out var value))
      {
        return value;
      }
      return defaultValue;
    }
    public static int Int32(this bool input)
    {
      if (input)
        return 1;
      return 0;
    }
    public static bool ToBool(this object input)
    {
      try { return Convert.ToBoolean(input); }
      catch
      {
        if (input.Text() == "1")
        {
          return true;
        }
        return false;
      }
    }


    //不想用链表又不想用集合字典,又想添加唯一值
    public static void AddToStringList(this string input, List<string> target)
    {
      var result = input.Text();
      if (result != "")
        target.Add(result);
    }
    //不想用三目怎么办呢
    public static string Compare(this string input, string check, string trueResult = "", string errorResult = "")
    {
      return C.Text(String.Equals(input, check) ? trueResult : errorResult);
    }
    public static string Compare(this bool input, bool check, string trueResult = "", string errorResult = "")
    {
      return C.Text(input == check ? trueResult : errorResult);
    }
    public static string Compare(this int input, int check, IntCompareType type = IntCompareType.equal, string trueResult = "", string errorResult = "")
    {
      bool checkResult = false;
      int sub = input - check;
      switch (type)
      {
        case IntCompareType.equal:
          if (sub == 0)
            checkResult = true;
          break;
        case IntCompareType.less:
          if (sub < 0)
            checkResult = true;
          break;
        case IntCompareType.more:
          if (sub > 0)
            checkResult = true;
          break;
        default:
          break;
      }
      if (checkResult)
        return trueResult.Text();
      return errorResult.Text();
    }
    //想要变成现金又不想写Format.
    public static string DefaultMoneyFormat { get; set; } = "#,##0.00";
    #region
    public static string MoneyString(this string input, string moneyString = "")
    {
      decimal result = 0;
      Decimal.TryParse(input.Text(), out result);
      return result.MoneyString(moneyString.Text(DefaultMoneyFormat));
    }
    public static string MoneyString(this object input, string moneyString = "")
    {
      var stringValue = "";
      if (input != null)
      {
        stringValue = Convert.ToString(input);
      }
      return MoneyString(stringValue, moneyString.Text(DefaultMoneyFormat));
    }
    public static string MoneyString(this Int16 input, string moneyString = "")
    {
      return input.ToString(moneyString.Text(DefaultMoneyFormat));
    }
    public static string MoneyString(this Int32 input, string moneyString = "")
    {
      return input.ToString(moneyString.Text(DefaultMoneyFormat));
    }
    public static string MoneyString(this Int64 input, string moneyString = "")
    {
      return input.ToString(moneyString.Text(DefaultMoneyFormat));
    }
    public static string MoneyString(this double input, string moneyString = "")
    {
      return input.ToString(moneyString.Text(DefaultMoneyFormat));
    }
    public static string MoneyString(this decimal input, string moneyString = "")
    {
      return input.ToString(moneyString.Text(DefaultMoneyFormat));
    }
    #endregion
    //左右pad,还想补齐
    public static string CPadLeft(this object input, int padNumber, char step = '\0')
    {
      var result = Convert.ToString(input);
      if (result.Length > padNumber)
      {
        result = result.Substring(result.Length - padNumber, padNumber);
      }
      else
      {
        result = result.PadLeft(padNumber, step);
      }
      return result;
    }
    public static string CPadRight(this object input, int padNumber, char step = '\0')
    {
      var result = Convert.ToString(input);
      if (result.Length > padNumber)
      {
        result = result.Substring(0, padNumber);
      }
      else
      {
        result = result.PadRight(padNumber, step);
      }
      return result;
    }
    //纯蛋疼没卵用
    #region
    public static string GetMonth(this int month)
    {
      switch (month)
      {
        case 1:
          return "January";
        case 2:
          return "February";
        case 3:
          return "March";
        case 4:
          return "April";
        case 5:
          return "May";
        case 6:
          return "June";
        case 7:
          return "July";
        case 8:
          return "August";
        case 9:
          return "September";
        case 10:
          return "October";
        case 11:
          return "November";
        case 12:
          return "December";
        default:
          return "January";
      }
    }
    public static string GetDay(this int day)
    {
      return day.ToString().PadLeft(2, '0');
    }

    #endregion
    [Obsolete]
    public static string RenderTemplate(string temp, IDictionary<string, string> model, string separatFormat = "[{0}]")
    {
      foreach (var item in model)
      {
        temp = temp.Replace(String.Format(separatFormat, item.Key), item.Value);
      }
      return temp;
    }
    ///<summary>
    /// Replace placeholder to value from templete. if the value is null, then leave the placeholder.
    ///</summary>
    ///<remarks>
    /// nested delimiters will ignore out side delimiters. and delimiters will only accectped {} key will ignore space or any empty char
    ///</remarks>
    ///<example>
    /// {{a}} will be {value of a here} \r\n
    /// {aa{b}{a}} will be "{aavalue of b herevalue of a here}\r\n
    /// {a a}{b} will be "{a a}value b here"\r\n
    /// </example>
    public static String PlaceHolderReplace(this string template, Func<string, string> getValueByKey)
    {
      if (String.IsNullOrEmpty(template))
      {
        return template;
      }
      Regex rx = new Regex(@"(?<=\{)[^\{\} \f\n\r\t\v]+(?=\})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      MatchCollection matches = rx.Matches(template);
      var parameters = new Dictionary<string, string>();
      var keys = matches.Cast<Match>()
              .Select(m => m.Value).GroupBy(b => b).Select(b => b.Key).ToList();
      foreach (var k in keys)
      {
        var value = getValueByKey(k);
        if (value != null)
        {
          parameters.Add($"{{{k}}}", value);
        }
      }
      return parameters.Aggregate(template, (current, parameter) => current.Replace(parameter.Key, parameter.Value.ToString()));
    }

    public static string ToLowerNth(this string input, int index = 1)
    {
      if (index <= 0)
        return input.ToLower();
      var chars = Char.ToLowerInvariant(input[index - 1]) + input.Substring(index);
      return chars;
    }

    public static IEnumerable<string> StringValueToList(this string input, char mark = ',')
    {
      var stringValue = input.Text();
      if (stringValue.Length >= 1 && stringValue[0] == ',')
      {
        stringValue = stringValue.Substring(1);
      }
      if (stringValue.Length >= 1 && stringValue[stringValue.Length - 1] == ',')
      {
        stringValue = stringValue.Substring(0, stringValue.Length - 1);
      }
      return stringValue.Split(mark);
    }

    public static string EntityNameSpace { get; set; } = "System.Data.Entity.DynamicProxies";

    public static Type GetRealType(this Type type)
    {
      if (type.Namespace == EntityNameSpace)
        return type.BaseType;
      return type;
    }
  }

  public enum IntCompareType
  {
    equal = 0,
    less = 1,
    more = 2
  }
}
