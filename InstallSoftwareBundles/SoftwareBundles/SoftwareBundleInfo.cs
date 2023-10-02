namespace Skyline.DataMiner.Utils.SoftwareBundle
{
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using NuGet.Versioning;

	/// <summary>
	/// Represents the information on the software bundle.
	/// </summary>
	public class SoftwareBundleInfo
	{
		/// <summary>
		/// Initialize a new instance of <see cref="SoftwareBundleInfo"/>.
		/// </summary>
		/// <param name="name">The name of the software bundle.</param>
		/// <param name="version">The version of the software bundle.</param>
		/// <param name="os">The operating system for which the software bundle is intended.</param>
		/// <param name="arch">The architecture of the system for which the software bundle is intended.</param>
		public SoftwareBundleInfo(string name, SemanticVersion version, string os = null, string arch = null)
		{
			Name = name;
			Version = version;
			OS = os;
			Arch = arch;
			SoftwareBundleVersion = new SemanticVersion(0,0,0);
		}

		/// <summary>
		/// The name of the software bundle.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// The version of the software bundle.
		/// </summary>
		[JsonProperty("version")]
		public SemanticVersion Version { get; set; }

		/// <summary>
		/// The system to which this software bundle applies if applicable.
		/// </summary>
		[JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
		public string OS { get; set; }

		/// <summary>
		/// The architecture to which this software bundle applies if applicable.
		/// </summary>
		[JsonProperty("arch", NullValueHandling = NullValueHandling.Ignore)]
		public string Arch { get; set; }

		/// <summary>
		/// The dependencies of the software bundle.
		/// </summary>
		[JsonProperty("dependencies", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, SemanticVersion> Dependencies { get; set; }

		/// <summary>
		/// The version of the software bundle. This is added to be future proof.
		/// </summary>
		[JsonProperty("software_bundle_version")]
		internal SemanticVersion SoftwareBundleVersion { get; set; }

		/// <summary>
		/// Get the serialized string of the object.
		/// </summary>
		/// <returns>The serialized string.</returns>
		public string Serialize()
		{
			return JsonConvert.SerializeObject(this);
		}

		/// <summary>
		/// Deserialize the string to an instance of <see cref="SoftwareBundleInfo"/>.
		/// </summary>
		/// <param name="json">The serialized JSON string.</param>
		/// <returns>The software bundle info.</returns>
		public static SoftwareBundleInfo Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<SoftwareBundleInfo>(json);
		}
	}
}