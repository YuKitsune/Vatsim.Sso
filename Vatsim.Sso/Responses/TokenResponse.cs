namespace Vatsim.Sso.Responses
{
	using Newtonsoft.Json;

	/// <summary>
	///		The token response from VATSIM SSO.
	/// </summary>
	public class TokenResponse : IReponse
	{
		/// <summary>
		///		Gets ot sets the <see cref="ResponseStatus"/>.
		/// </summary>
		[JsonProperty("Request")]
		public ResponseStatus Status { get; set; }

		/// <summary>
		///		Gets ot sets the raw JSON string received from VATSIM SSO.
		/// </summary>
		public string Raw { get; set; } = "";

		/// <summary>
		///		Gets ot sets the <see cref="TokenData"/>.
		/// </summary>
		public TokenData Token { get; set; }
	}
}
