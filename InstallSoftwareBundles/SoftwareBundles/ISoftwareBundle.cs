namespace Skyline.DataMiner.Utils.SoftwareBundle
{
	/// <summary>
	/// Defines a software bundle containing a bundled set of files.
	/// </summary>
	public interface ISoftwareBundle
	{
		/// <summary>
		/// Defines the path of the software bundle or software bundle folder.
		/// </summary>
		string Path { get; }

		/// <summary>
		/// Defines the information of the software bundle.
		/// </summary>
		SoftwareBundleInfo SoftwareBundleInfo { get; }
	}
}