namespace Vatsim
{
	/// <summary>
	/// 	The ATC Rating received from VATSIM SSO.
	/// </summary>
	public class ATCRating
	{
		/// <summary>
		/// 	Gets or sets the identifier.
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// 	Gets or sets the short code.
		/// </summary>
		public string Short { get; set; }

		/// <summary>
		/// 	Gets or sets the long code.
		/// </summary>
		public string Long { get; set; }

		/// <summary>
		/// 	Gets or sets the GRP code.
		/// </summary>
		public string GRP { get; set; }
	}
}
