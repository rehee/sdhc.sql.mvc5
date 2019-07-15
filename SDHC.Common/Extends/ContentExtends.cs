namespace System
{
  public static class ContentExtends
  {
    public static string GetValueByKey(this object input, string key)
    {
      
      if (input == null)
      {
        return "";
      }
      var type = input.GetType().GetRealType();
      foreach (var p in type.GetProperties())
      {
        if (p.Name != key)
        {
          continue;
        }
        var value = p.GetValue(input);
        return value.MyTryConvert<string>();
      }
      return "";
    }
    public static string GetUrlPath(this string input)
    {
      if (string.IsNullOrEmpty(input))
      {
        return "/";
      }
      if (input.ToLower().IndexOf("http://") == 0)
      {
        return input;
      }
      return $"/{input}";
    }
  }
}
