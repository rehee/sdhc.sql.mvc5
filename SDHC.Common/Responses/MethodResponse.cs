using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Responses
{
  public class MethodResponse
  {
    public static void SetIsSuccess(bool isSuccess, MethodResponse response)
    {
      if (response != null)
      {
        response.IsSuccess = isSuccess;
      }
    }
    public static void SetMessage(string message, MethodResponse response)
    {
      if (response != null)
      {
        response.Message = message;
      }
    }
    public static void SetEnum(int value, MethodResponse response)
    {
      if (response != null)
      {
        response.ResponseEnumValue = value;
      }
    }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int ResponseEnumValue { get; set; }
  }
}
