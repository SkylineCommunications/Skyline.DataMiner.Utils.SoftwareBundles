namespace Skyline.DataMiner.Utils.SoftwareBundle
{
	/// <summary>
	/// Represents a zipped software bundle.
	/// </summary>
	public interface IZippedSoftwareBundle : ISoftwareBundle
	{
		/// <summary>
		/// Create an unzipped software bundle folder from the zipped software bundle.
		/// </summary>
		/// <param name="destinationFolder">The destination folder of the software bundle.</param>
		/// <returns>The unzipped software bundle.</returns>
		IUnZippedSoftwareBundle CreateUnzippedSoftwareBundle(string destinationFolder);

		/// <summary>
		/// Delete the zipped software bundle folder.
		/// </summary>
		void Delete();
	}
}