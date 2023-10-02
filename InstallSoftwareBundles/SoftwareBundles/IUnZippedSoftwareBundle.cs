namespace Skyline.DataMiner.Utils.SoftwareBundle
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents an unzipped software bundle.
	/// </summary>
	public interface IUnZippedSoftwareBundle : ISoftwareBundle
	{
		/// <summary>
		/// Get the files from the unzipped software bundle folder.
		/// </summary>
		/// <returns>The full paths of all the files.</returns>
		IEnumerable<string> GetFiles();

		/// <summary>
		/// Create a zipped software bundle from the unzipped software bundle folder.
		/// </summary>
		/// <param name="destinationPath">The full path (incl. filename with extension) to for the zipped software bundle.</param>
		/// <returns>The zipped software bundle.</returns>
		IZippedSoftwareBundle CreateZippedSoftwareBundle(string destinationPath);

		/// <summary>
		/// Delete the unzipped software bundle folder.
		/// </summary>
		void Delete();
	}
}