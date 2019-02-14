namespace Vatsim.Responses
{
	/// <summary>
	/// 	The VATSIM SSO API response interface.
	/// </summary>
	public interface IReponse
	{
		/// <summary>
		///		Gets ot sets the <see cref="ResponseStatus"/>.
		/// </summary>
		ResponseStatus Status { get; set; }

		/// <summary>
		///		Gets ot sets the raw JSON string received from VATSIM SSO.
		/// </summary>
		string Raw { get; set; }
	}
}
