using System;
using System.Security.Cryptography;
using System.Text;

namespace com.lemonmojo.OneSkyAppSharp
{
	internal static class OneSkyAppUtils
	{
		internal static int ToUnixTimestamp(this DateTime dateTime)
		{
			return (int)(dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
		}

		internal static string CalculateMD5Hash(this string input)
		{
			// step 1, calculate MD5 hash from input
			MD5 md5 = MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++) {
				sb.Append(hash[i].ToString("X2"));
			}

			return sb.ToString();
		}
	}
}