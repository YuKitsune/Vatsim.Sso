namespace Vatsim.Sso
{
	using Newtonsoft.Json;

	/// <summary>
	///		The token data.
	/// </summary>
	public class TokenData
	{
		/// <summary>
		///		Gets ot sets the token.
		/// </summary>
		[JsonProperty("oauth_token")]
		public string Token { get; set; }


		/// <summary>
		///		Gets ot sets the token secret.
		/// </summary>
		[JsonProperty("oauth_token_secret")]
		public string TokenSecret { get; set; }


		/// <summary>
		///		Gets ot sets a valud indicating whether or not the callback was confirmed.
		/// </summary>
		// Todo: Can this be made into a bool?
		[JsonProperty("oauth_callback_confirmed")]
		public string CallbackConfirmed { get; set; }
	}
}
