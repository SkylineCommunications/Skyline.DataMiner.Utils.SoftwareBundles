namespace Skyline.DataMiner.Utils.SoftwareBundle
{
	/// <summary>
	/// Defines the methods available for SoftwareBundles.
	/// </summary>
	public static class SoftwareBundles
	{
		/// <summary>
		/// Get the zipped software bundle object from the full path.
		/// </summary>
		/// <param name="path">The full path of the zipped software bundle (e.g. C:\myzippedsoftwarebundle.zip).</param>
		/// <returns>The zipped software bundle object.</returns>
		public static IZippedSoftwareBundle GetZippedSoftwareBundle(string path)
		{
			return new ZippedSoftwareBundle(path);
		}

		/// <summary>
		/// Get the unzipped software bundle object from the folder path.
		/// </summary>
		/// <param name="path">The path of the folder where the software bundle is unzipped. (e.g. C:\myzippedsoftwarebundle).</param>
		/// <returns>The unzipped software bundle object.</returns>
		public static IUnZippedSoftwareBundle GetUnZippedSoftwareBundle(string path)
		{
			return new UnZippedSoftwareBundle(path);
		}
	}
}