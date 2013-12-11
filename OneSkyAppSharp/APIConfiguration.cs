using System;

namespace com.lemonmojo.OneSkyAppSharp
{
	public class APIConfiguration
	{
		public static APIConfiguration DefaultConfiguration { get; set; }

		public string PublicKey { get; private set; }
		public string SecretKey { get; private set; }

		public APIConfiguration(string publicKey, string secretKey)
		{
			if (string.IsNullOrWhiteSpace(publicKey)) {
				throw new ArgumentException("Public Key must not be empty.");
			}

			if (string.IsNullOrWhiteSpace(secretKey)) {
				throw new ArgumentException("Secret Key must not be empty.");
			}

			this.PublicKey = publicKey;
			this.SecretKey = secretKey;
		}

		public void SetAsDefault()
		{
			APIConfiguration.DefaultConfiguration = this;
		}

		public static void ResetDefault()
		{
			APIConfiguration.DefaultConfiguration = null;
		}
	}
}