namespace Examples
{
	using System;
	using NuGet.Versioning;
	using Skyline.DataMiner.Utils.SoftwareBundle;

	public class ExampleClass
	{
		public void ReadAllFromSoftwareBundle()
		{
			string softwareBundleLocation = @"TestSoftwareBundles\zippedSoftwareBundle.zip";
			string folderLocation = @"TestSoftwareBundles\UnzippedFolder";
			var zippedSoftwareBundle = SoftwareBundles.GetZippedSoftwareBundle(softwareBundleLocation);
			if (zippedSoftwareBundle.SoftwareBundleInfo.Version < new SemanticVersion(2, 0, 0))
			{
				throw new NotSupportedException("SoftwareBundles older than 2.0.0 are not supported");
			}

			var unzippedSoftwareBundle = zippedSoftwareBundle.CreateUnzippedSoftwareBundle(folderLocation);
			foreach (var file in unzippedSoftwareBundle.GetFiles())
			{
				System.IO.File.ReadAllText(file);
			}
		}
	}
}