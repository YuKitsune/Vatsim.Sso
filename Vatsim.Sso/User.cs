namespace Vatsim
{
	using System;

	using Newtonsoft.Json;

	/// <summary>
	/// 	The VATSIM user received from VATSIM SSO.
	/// </summary>
	public class User
	{
		/// <summary>
		/// 	Gets or sets the VATSIM CID.
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// 	Gets or sets the first name.
		/// </summary>
		[JsonProperty("Name_First")]
		public string FirstName { get; set; }

		/// <summary>
		/// 	Gets or sets the last name.
		/// </summary>
		[JsonProperty("Name_Last")]
		public string LastName { get; set; }

		/// <summary>
		/// 	Gets the full name.
		/// </summary>
		public string FullName
		{
			get => ((string.IsNullOrEmpty(FirstName) ? string.Empty : FirstName) + " " + (string.IsNullOrEmpty(LastName) ? string.Empty : LastName)).Trim();
		}

		/// <summary>
		/// 	Gets or sets the <see cref="Vatsim.ATCRating"/>.
		/// </summary>
		[JsonProperty("Rating")]
		public ATCRating ATCRating { get; set; }

		/// <summary>
		/// 	Gets or sets the <see cref="Vatsim.PilotRating"/>
		/// </summary>
		[JsonProperty("Pilot_Rating")]
		public PilotRating PilotRating { get; set; }

		/// <summary>
		/// 	Gets or sets the experience.
		/// </summary>
		public string Experience { get; set; }

		/// <summary>
		/// 	Gets or sets the registration date.
		/// </summary>
		[JsonProperty("Reg_Date")]
		public DateTime RegistrationDate { get; set; }

		/// <summary>
		/// 	Gets or sets the country. See <see cref="Area"/>
		/// </summary>
		public Area Country { get; set; }

		/// <summary>
		/// 	Gets or sets the region. See <see cref="Area"/>
		/// </summary>
		public Area Region { get; set; }

		/// <summary>
		/// 	Gets or sets the division. See <see cref="Area"/>
		/// </summary>
		public Area Division { get; set; }

		/// <summary>
		/// 	Gets or sets the sub-division. See <see cref="Area"/>
		/// </summary>
		public Area Subdivision { get; set; }
	}
}
