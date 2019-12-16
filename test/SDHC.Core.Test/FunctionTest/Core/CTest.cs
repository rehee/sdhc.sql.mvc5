using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace SDHC.Core.Test.FunctionTest.Core
{
  [TestClass]
  public class CTest
  {
    [TestCase(null, "")]
    [TestCase("a", "A")]
    [TestCase("AA", "A A")]
    [TestCase("aA", "A A")]
    [TestCase("SpacesFromCamel", "Spaces From Camel")]
    [TestCase("spacesFromCamel", "Spaces From Camel")]
    public void SpacesFromCamel_text(string input, string expect)
    {

      Assert.AreEqual(expect, input.SpacesFromCamel());
    }

    [TestCase("1", "", "1", -1, true)]
    [TestCase(null, "1", "1", -1, true)]
    [TestCase("", "1", "1", -1, true)]
    [TestCase("A", "1", "A", -1, true)]
    [TestCase("a", "1", "A", 1, true)]
    [TestCase("aa", "1", "Aa", 1, true)]
    [TestCase("aa", "1", "AA", 2, true)]
    [TestCase("aa ", "1", "aa ", -1, false)]
    [TestCase("aa ", "1", "aa", -1, true)]
    public void Text_Test(string inputString, string placeHolder, string expect, int capStringstring = -1, bool isTrim = true)
    {
      var actual = inputString.Text(placeHolder, capStringstring, isTrim);
      Assert.AreEqual(expect, actual);
    }

    [TestCase("", -1, "")]
    [TestCase(null, -1, "")]
    [TestCase("abC", -1, "abC")]
    [TestCase("abC", 0, "abc")]
    [TestCase("abC", 1, "Abc")]
    [TestCase("abC", 2, "ABc")]
    [TestCase("abC", 3, "ABC")]
    public void CaptalString_Test(string input, int caps, string expect)
    {
      var actual = input.CaptalString(caps);
      Assert.AreEqual(expect, actual);
    }

    [TestCase(null, 0, 0)]
    [TestCase(null, 1, 1)]
    [TestCase("1", 0, 1)]
    [TestCase("1 ", 0, 1)]
    [TestCase(" 1 ", 0, 1)]
    [TestCase(" 21 ", 0, 21)]
    [TestCase(" 1 2", 0, 0)]
    [TestCase(" 21a ", 0, 0)]
    public void Int32_string(string inputString, Int32 defaultValue, Int32 expectValue)
    {
      var actual = inputString.Int32(defaultValue);
      Assert.AreEqual(expectValue, actual);
    }

    [TestCase(true, 1)]
    [TestCase(false, 0)]
    public void Int32_bool(bool input, int expectValue)
    {
      var actual = input.Int32();
      Assert.AreEqual(expectValue, actual);
    }
    [TestCase(1, true)]
    [TestCase(0, false)]
    [TestCase("1", true)]
    public void ToBool_test(object input, bool expectValue)
    {
      var actual = input.ToBool();
      Assert.AreEqual(expectValue, actual);
    }

    [TestCase("1", false)]
    [TestCase("2", false)]
    [TestCase("3", false)]
    [TestCase("4", true)]
    public void AddToStringList_Test(string input, bool added)
    {
      var list = new List<string>()
      {
        "1","2","3",
      };
      input.AddToStringList(list);
      bool actual = list.Count == 4;
      Assert.IsTrue(actual);
    }
    [TestCase("1","1.00")]
    [TestCase("1.01", "1.01")]
    public void MoneyString_Test(object input, string expect)
    {
      Assert.AreEqual(expect, input.MoneyString());
    }

    [TestCase("aaa {a}", "aaa this is a")]
    [TestCase("aaa {b}", "aaa b string here")]
    [TestCase("aaa {c}", "aaa value for c")]
    [TestCase("aaa {a} {a}", "aaa this is a this is a")]
    [TestCase("aaa {a} {0}", "aaa this is a {0}")]
    [TestCase("aaa {a}", "aaa this is a")]
    [TestCase("aaa {b}", "aaa b string here")]
    [TestCase("aaa {c}", "aaa value for c")]
    [TestCase("aaa {a} {a}", "aaa this is a this is a")]
    [TestCase("aaa {a} {0}", "aaa this is a {0}")]
    [TestCase("aaa {a}", "aaa this is a")]
    [TestCase("aaa {b}", "aaa b string here")]
    [TestCase("aaa {c}", "aaa value for c")]
    [TestCase("aaa {a} {a}", "aaa this is a this is a")]
    [TestCase("aaa {a} {0}", "aaa this is a {0}")]
    [TestCase("aaa [a]", "aaa [a]")]
    [TestCase("aaa [b]", "aaa [b]")]
    [TestCase("aaa [c]", "aaa [c]")]
    [TestCase("aaa [a] [a]", "aaa [a] [a]")]
    [TestCase("aaa [a] [0]", "aaa [a] [0]")]
    [TestCase("aaa [a> {a} [0]", "aaa [a> this is a [0]")]
    [TestCase(null, null)]
    [TestCase("{}", "{}")]
    [TestCase("{\r\n}", "{\r\n}")]
    [TestCase("{{a}}", "{this is a}")]
    [TestCase("{aa{b}{a}}", "{aab string herethis is a}")]
    [TestCase("{aa{b}{0}{a}}", "{aab string here{0}this is a}")]
    [TestCase("{aa{b}}{a}}", "{aab string here}this is a}")]
    [TestCase("{a}{b}", "this is ab string here")]
    [TestCase("{a a}{b}", "{a a}b string here")]
    [TestCase("(a)", "(a)")]
    [TestCase("STARTaEND", "STARTaEND")]
    [TestCase("_a_", "_a_")]
    [TestCase("jaj", "jaj")]
    [TestCase("5a6", "5a6")]
    [TestCase(" a ", " a ")]

    public void DoPlaceHolderReplaced(string templete, string expected)
    {
      Func<string, string> getKeyFromValue = key =>
      {
        switch (key)
        {
          case "a":
            return "this is a";
          case "b":
            return "b string here";
          case "c":
            return "value for c";
          default:
            return null;
        }
      };
      Assert.AreEqual(expected, templete.PlaceHolderReplace(getKeyFromValue));
    }
  }
}
