namespace Vatsim.Sso
{
	/// <summary>
	/// 	The area received from VATSIM SSO.
	/// </summary>
	/// <remarks>
	/// 	Used to define regions, divisions, countries, etc.
	/// </remarks>
	public class Area
	{
		/// <summary>
		/// 	Gets or sets the area code.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 	Gets or sets the area name.
		/// </summary>
		public string Name { get; set; }
	}
}
