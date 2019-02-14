namespace Vatsim
{
	using System;
	using System.IO;
	using System.Net;

	using OAuth;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	using Vatsim.ApiResponses.Sso;

	/// <summary>
	/// 	The VATSIM SSO communication class.
	/// </summary>
	public static class Sso
	{
		/// <summary>
		/// 	Gets the VATSIM SSO base url.
		/// </summary>
		public static string BaseUrl { get; private set; }
		
		/// <summary>
		/// 	Initializes the static fields for <see cref="T:Vatsim.Sso"/>.
		/// </summary>
		static Sso() { BaseUrl = "https://cert.vatsim.net/sso/api/"; }

		/// <summary>
		///		Gets the VATSIM SSO token information.
		/// </summary>
		/// <param name="consumerKey">
		///		The OAuth consumer key.
		///	</param>
		/// <param name="consumerSecret">
		///		The OAuth consumer secret.
		///	</param>
		/// <param name="callbackUrl">
		///		The url to redirect to after login.
		///	</param>
		/// <returns>
		///		The <see cref="TokenResponse"/>.
		///	</returns>
		public static TokenResponse GetRequestToken(string consumerKey, string consumerSecret, string callbackUrl)
		{
			// Check the inputs
			if (string.IsNullOrEmpty(consumerKey)) throw new ArgumentNullException(nameof(consumerKey), "The consumer key cannot be null or empty.");
			if (string.IsNullOrEmpty(consumerSecret)) throw new ArgumentNullException(nameof(consumerSecret), "The consumer secret cannot be null or empty.");
			if (string.IsNullOrEmpty(callbackUrl)) throw new ArgumentNullException(nameof(callbackUrl), "The callback url cannot be null or empty.");

			// Create the OAuth request
			OAuthRequest oauthRequest = new OAuthRequest()
			{
				ConsumerKey = consumerKey,
				ConsumerSecret = consumerSecret,
				Method = "GET",
				CallbackUrl = callbackUrl,
				Type = OAuthRequestType.RequestToken,
				SignatureMethod = OAuthSignatureMethod.HmacSha1,
				RequestUrl = BaseUrl + "login_token/",
			};
			
			// Get the response
			TokenResponse res = GetResponse<TokenResponse>(oauthRequest, out string raw);
			res.Raw = raw;

			// Return the response
			return res;
		}

		/// <summary>
		///		Gets the user data.
		/// </summary>
		/// <param name="consumerKey">
		///		The OAuth consumer key.
		///	</param>
		/// <param name="consumerSecret">
		///		The OAuth consumer secret.
		///	</param>
		/// <param name="callbackUrl">
		///		The url to redirect to after login.
		///	</param>
		/// <param name="verifier">
		///		The verifier.
		///	</param>
		/// <param name="token">
		///		The token.
		///	</param>
		/// <param name="tokenSecret">
		///		The token secret.
		///	</param>
		/// <returns>
		///		The <see cref="UserResponse"/>.
		///	</returns>
		public static UserResponse GetUserData(string consumerKey, string consumerSecret, string callbackUrl, string verifier, string token, string tokenSecret)
		{
			// Check the inputs
			if (string.IsNullOrEmpty(consumerKey)) throw new ArgumentNullException(nameof(consumerKey), "The consumer key cannot be null or empty.");
			if (string.IsNullOrEmpty(consumerSecret)) throw new ArgumentNullException(nameof(consumerSecret), "The consumer secret cannot be null or empty.");
			if (string.IsNullOrEmpty(callbackUrl)) throw new ArgumentNullException(nameof(callbackUrl), "The callback url cannot be null or empty.");
			if (string.IsNullOrEmpty(verifier)) throw new ArgumentNullException(nameof(verifier), "The verifier cannot be null or empty.");
			if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token), "The token cannot be null or empty.");
			if (string.IsNullOrEmpty(tokenSecret)) throw new ArgumentNullException(nameof(tokenSecret), "The token secret cannot be null or empty.");

			// Create the OAuth request
			OAuthRequest oauthRequest = new OAuthRequest()
			{
				ConsumerKey = consumerKey,
				ConsumerSecret = consumerSecret,
				Method = "GET",
				Type = OAuthRequestType.ProtectedResource,
				SignatureMethod = OAuthSignatureMethod.HmacSha1,
				Token = token,
				TokenSecret = tokenSecret,
				Verifier = verifier,
				RequestUrl = BaseUrl + "login_return/"
			};

			// Get the response
			UserResponse res = GetResponse<UserResponse>(oauthRequest, out string raw);
			res.Raw = raw;
			
			// Return the user
			return res;
		}

		/// <summary>
		/// 	Gets the response from VATSIM SSO.
		/// </summary>
		/// <param name="oauthRequest">
		/// 	The <see cref="OAuthRequest"/>.
		/// </param>
		/// <param name="raw">
		///		The raw JSON response.
		/// </param>
		/// <returns>
		/// 	The <see cref="IReponse"/>.
		/// </returns>
		private static T GetResponse<T>(OAuthRequest oauthRequest, out string raw)
		{
			// Check that the type is of IResponse
			if (!typeof(IReponse).IsAssignableFrom(typeof(T))) throw new InvalidOperationException("The given type " + typeof(T).Name + " does not implement IResponse.");
			
			// Pre-set the raw output
			raw = string.Empty;
			
			// Get the auth query
			string auth = oauthRequest.GetAuthorizationQuery();

			// Create the web request and add auth query to the url
			string url = oauthRequest.RequestUrl + '?' + auth;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

			// Get the response
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			string json = string.Empty;
			using (StreamReader responseStream = new StreamReader(response.GetResponseStream())) json = responseStream.ReadToEnd();

			// Deserialize the JSON string into an IResponse and account for the date time formatting
			T data = JsonConvert.DeserializeObject<T>(json, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			if (data != null) raw = json;

			return data;
		}

		/// <summary>
		/// 	Sets the base URL.
		/// </summary>
		/// <param name="url">
		/// 	The new URL.
		/// </param>
		public static void SetBaseUrl(string url)
		{
			// Check the url is valid
			if (!VerifyUrl(ref url, out string message)) 
				throw new ArgumentException(message);

			// All is well, set the url
			BaseUrl = url;
		}

		/// <summary>
		/// 	Verifies and cleans the given url.
		/// </summary>
		/// <param name="url">
		///		The URL.
		/// </param>
		/// <param name="message">
		///		The exception message if any.
		/// </param>
		/// <returns></returns>
		private static bool VerifyUrl(ref string url, out string message)
		{
			// Check the url is valid
			bool valid = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
						 (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp);
			
			// If not, add a message
			message = !valid ? "The url \"" + url + "\" is not a valid url." : string.Empty;

			// Check the last character for a /
			if (!url.EndsWith("/", StringComparison.InvariantCulture)) url += "/";

			return valid;
		}
	}
}