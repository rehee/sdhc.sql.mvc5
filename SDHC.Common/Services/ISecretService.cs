using SDHC.Common.Configs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SDHC.Common.Services
{
  public interface ISecretService
  {
    string Encrypt(string inputString, bool base32 = true);
    string Decrypt(string inputString, bool base32 = true);
  }

  public class SecretService : ISecretService
  {
    private byte[] deKey { get; }
    private byte[] deIV { get; }
    public SecretService(SystemConfig config)
    {
      deKey = Encoding.Default.GetBytes(config.SecretdeKey);
      deIV = Encoding.Default.GetBytes(config.SecretdeIV);
    }

    public string Encrypt(string inputString, bool base32 = true)
    {
      MemoryStream ms = new MemoryStream();
      DESCryptoServiceProvider key = new DESCryptoServiceProvider();
      //SymmetricAlgorithm key = SymmetricAlgorithm.Create();

      CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(deKey, deIV), CryptoStreamMode.Write);
      StreamWriter sw = new StreamWriter(encStream);

      sw.WriteLine(inputString);
      sw.Close();
      encStream.Close();

      byte[] buffer = ms.ToArray();
      ms.Close();
      var result = Convert.ToBase64String(buffer);

      if (base32)
      {
        return Base32.Encode(buffer);
      }
      return result;
    }

    public string Decrypt(string cypherText, bool base32 = true)
    {

      string text;
      if (base32)
        text = Convert.ToBase64String(Base32.Decode(cypherText));
      else
        text = cypherText;
      MemoryStream ms = new MemoryStream(Convert.FromBase64String(text));
      DESCryptoServiceProvider key = new DESCryptoServiceProvider();
      //SymmetricAlgorithm key = SymmetricAlgorithm.Create();

      CryptoStream encStream = new CryptoStream(ms, key.CreateDecryptor(deKey, deIV), CryptoStreamMode.Read);
      StreamReader sr = new StreamReader(encStream);

      string val = sr.ReadLine();

      sr.Close();
      encStream.Close();
      ms.Close();
      return val;
    }
  }


  public static class Base32
  {

    private static readonly char[] DIGITS;
    private static readonly int MASK;
    private static readonly int SHIFT;
    private static Dictionary<char, int> CHAR_MAP = new Dictionary<char, int>();
    private const string SEPARATOR = "-";

    static Base32()
    {
      DIGITS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();
      MASK = DIGITS.Length - 1;
      SHIFT = numberOfTrailingZeros(DIGITS.Length);
      for (int i = 0; i < DIGITS.Length; i++) CHAR_MAP[DIGITS[i]] = i;
    }

    private static int numberOfTrailingZeros(int i)
    {
      // HD, Figure 5-14
      int y;
      if (i == 0) return 32;
      int n = 31;
      y = i << 16; if (y != 0) { n = n - 16; i = y; }
      y = i << 8; if (y != 0) { n = n - 8; i = y; }
      y = i << 4; if (y != 0) { n = n - 4; i = y; }
      y = i << 2; if (y != 0) { n = n - 2; i = y; }
      return n - (int)((uint)(i << 1) >> 31);
    }

    public static byte[] Decode(string encoded)
    {
      // Remove whitespace and separators
      encoded = encoded.Trim().Replace(SEPARATOR, "");

      // Remove padding. Note: the padding is used as hint to determine how many
      // bits to decode from the last incomplete chunk (which is commented out
      // below, so this may have been wrong to start with).
      encoded = Regex.Replace(encoded, "[=]*$", "");

      // Canonicalize to all upper case
      encoded = encoded.ToUpper();
      if (encoded.Length == 0)
      {
        return new byte[0];
      }
      int encodedLength = encoded.Length;
      int outLength = encodedLength * SHIFT / 8;
      byte[] result = new byte[outLength];
      int buffer = 0;
      int next = 0;
      int bitsLeft = 0;
      foreach (char c in encoded.ToCharArray())
      {
        if (!CHAR_MAP.ContainsKey(c))
        {
          throw new DecodingException("Illegal character: " + c);
        }
        buffer <<= SHIFT;
        buffer |= CHAR_MAP[c] & MASK;
        bitsLeft += SHIFT;
        if (bitsLeft >= 8)
        {
          result[next++] = (byte)(buffer >> (bitsLeft - 8));
          bitsLeft -= 8;
        }
      }
      // We'll ignore leftover bits for now.
      //
      // if (next != outLength || bitsLeft >= SHIFT) {
      //  throw new DecodingException("Bits left: " + bitsLeft);
      // }
      return result;
    }


    public static string Encode(byte[] data, bool padOutput = false)
    {
      if (data.Length == 0)
      {
        return "";
      }

      // SHIFT is the number of bits per output character, so the length of the
      // output is the length of the input multiplied by 8/SHIFT, rounded up.
      if (data.Length >= (1 << 28))
      {
        // The computation below will fail, so don't do it.
        throw new ArgumentOutOfRangeException("data");
      }

      int outputLength = (data.Length * 8 + SHIFT - 1) / SHIFT;
      StringBuilder result = new StringBuilder(outputLength);

      int buffer = data[0];
      int next = 1;
      int bitsLeft = 8;
      while (bitsLeft > 0 || next < data.Length)
      {
        if (bitsLeft < SHIFT)
        {
          if (next < data.Length)
          {
            buffer <<= 8;
            buffer |= (data[next++] & 0xff);
            bitsLeft += 8;
          }
          else
          {
            int pad = SHIFT - bitsLeft;
            buffer <<= pad;
            bitsLeft += pad;
          }
        }
        int index = MASK & (buffer >> (bitsLeft - SHIFT));
        bitsLeft -= SHIFT;
        result.Append(DIGITS[index]);
      }
      if (padOutput)
      {
        int padding = 8 - (result.Length % 8);
        if (padding > 0) result.Append(new string('=', padding == 8 ? 0 : padding));
      }
      return result.ToString();
    }

    private class DecodingException : Exception
    {
      public DecodingException(string message) : base(message)
      {
      }
    }
  }
}
