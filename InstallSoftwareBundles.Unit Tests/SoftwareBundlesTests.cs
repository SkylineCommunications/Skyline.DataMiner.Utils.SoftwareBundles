using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NuGet.Versioning;
using Skyline.DataMiner.Utils.SoftwareBundle;

namespace Skyline.DataMiner.Utils.SoftwareBundle.Tests
{
	[TestClass]
	public class SoftwareBundlesTests
	{
		[TestMethod()]
		public void GetUnZippedSoftwareBundleTest()
		{
			string folderLocation = @"TestSoftwareBundles\UnzippedFolder";
			var unzippedSoftwareBundle = SoftwareBundles.GetUnZippedSoftwareBundle(folderLocation);
			Assert.IsNotNull(unzippedSoftwareBundle, "unzippedSoftwareBundle IsNotNull");

			// SoftwareBundleInfo
			Assert.IsNotNull(unzippedSoftwareBundle.SoftwareBundleInfo, "unzippedSoftwareBundle.SoftwareBundleInfo IsNotNull");
			Assert.AreEqual("reaper", unzippedSoftwareBundle.SoftwareBundleInfo.Name, "unzippedSoftwareBundle.SoftwareBundleInfo.Name AreEqual");
			Assert.AreEqual(new SemanticVersion(3, 3, 1), unzippedSoftwareBundle.SoftwareBundleInfo.Version, "unzippedSoftwareBundle.SoftwareBundleInfo.Version AreEqual");
			Assert.AreEqual("Debian", unzippedSoftwareBundle.SoftwareBundleInfo.OS, "unzippedSoftwareBundle.SoftwareBundleInfo.OS AreEqual");
			Assert.AreEqual("x86_64", unzippedSoftwareBundle.SoftwareBundleInfo.Arch, "unzippedSoftwareBundle.SoftwareBundleInfo.OS AreEqual");
			Assert.AreEqual(new SemanticVersion(0, 0, 0), unzippedSoftwareBundle.SoftwareBundleInfo.SoftwareBundleVersion, "unzippedSoftwareBundle.SoftwareBundleInfo.SoftwareBundleVersion AreEqual");

			// Unzipped
			var files = unzippedSoftwareBundle.GetFiles();
			Assert.AreEqual(1, files.Count(), "files.Count() AreEqual");
			Assert.AreEqual("TestFile", File.ReadAllText(files.First()), "files AreEqual");

			// Create zipped
			string zippedLocation = @"TestSoftwareBundles\NewZippedSoftwareBundle";
			var zipped = unzippedSoftwareBundle.CreateZippedSoftwareBundle(zippedLocation);
			Assert.IsTrue(File.Exists(zippedLocation));
			Assert.IsNotNull(zipped, "zipped IsNotNull");
			Assert.IsNotNull(zipped.SoftwareBundleInfo, "zipped.SoftwareBundleInfo IsNotNull");
			Assert.AreEqual("reaper", zipped.SoftwareBundleInfo.Name, "zipped.SoftwareBundleInfo.Name AreEqual");
			Assert.AreEqual(new SemanticVersion(3, 3, 1), zipped.SoftwareBundleInfo.Version, "zipped.SoftwareBundleInfo.Version AreEqual");
			Assert.AreEqual("Debian", zipped.SoftwareBundleInfo.OS, "zipped.SoftwareBundleInfo.OS AreEqual");
			Assert.AreEqual("x86_64", zipped.SoftwareBundleInfo.Arch, "zipped.SoftwareBundleInfo.OS AreEqual");
			Assert.AreEqual(new SemanticVersion(0, 0, 0), zipped.SoftwareBundleInfo.SoftwareBundleVersion, "zipped.SoftwareBundleInfo.SoftwareBundleVersion AreEqual");

			// Delete zipped
			zipped.Delete();
			Assert.IsFalse(File.Exists(zippedLocation));
		}

		[TestMethod()]
		public void GetZippedSoftwareBundleTest()
		{
			string zipSoftwareBundleLocation = @"TestSoftwareBundles\zippedSoftwareBundle.zip";
			var zippedSoftwareBundle = SoftwareBundles.GetZippedSoftwareBundle(zipSoftwareBundleLocation);
			Assert.IsNotNull(zippedSoftwareBundle, "unzippedSoftwareBundle IsNotNull");

			// SoftwareBundleInfo
			Assert.IsNotNull(zippedSoftwareBundle.SoftwareBundleInfo, "unzippedSoftwareBundle.SoftwareBundleInfo IsNotNull");
			Assert.AreEqual("reaper", zippedSoftwareBundle.SoftwareBundleInfo.Name, "unzippedSoftwareBundle.SoftwareBundleInfo.Name AreEqual");
			Assert.AreEqual(new SemanticVersion(3, 3, 1), zippedSoftwareBundle.SoftwareBundleInfo.Version, "unzippedSoftwareBundle.SoftwareBundleInfo.Version AreEqual");
			Assert.AreEqual("Debian", zippedSoftwareBundle.SoftwareBundleInfo.OS, "unzippedSoftwareBundle.SoftwareBundleInfo.OS AreEqual");
			Assert.AreEqual("x86_64", zippedSoftwareBundle.SoftwareBundleInfo.Arch, "unzippedSoftwareBundle.SoftwareBundleInfo.OS AreEqual");
			Assert.AreEqual(new SemanticVersion(0, 0, 0), zippedSoftwareBundle.SoftwareBundleInfo.SoftwareBundleVersion, "unzippedSoftwareBundle.SoftwareBundleInfo.SoftwareBundleVersion AreEqual");

			// Create unzipped
			string unzippedLocation = @"TestSoftwareBundles\NewUnZippedFolder";
			var unzipped = zippedSoftwareBundle.CreateUnzippedSoftwareBundle(unzippedLocation);
			Assert.IsTrue(Directory.Exists(unzippedLocation));
			Assert.IsNotNull(unzipped, "unzipped IsNotNull");
			Assert.IsNotNull(unzipped.SoftwareBundleInfo, "unzipped.SoftwareBundleInfo IsNotNull");
			Assert.AreEqual("reaper", unzipped.SoftwareBundleInfo.Name, "unzipped.SoftwareBundleInfo.Name AreEqual");
			Assert.AreEqual(new SemanticVersion(3, 3, 1), unzipped.SoftwareBundleInfo.Version, "unzipped.SoftwareBundleInfo.Version AreEqual");
			Assert.AreEqual("Debian", unzipped.SoftwareBundleInfo.OS, "unzipped.SoftwareBundleInfo.OS AreEqual");
			Assert.AreEqual("x86_64", unzipped.SoftwareBundleInfo.Arch, "unzipped.SoftwareBundleInfo.OS AreEqual");
			Assert.AreEqual(new SemanticVersion(0, 0, 0), unzipped.SoftwareBundleInfo.SoftwareBundleVersion, "unzipped.SoftwareBundleInfo.SoftwareBundleVersion AreEqual");

			// Delete zipped
			unzipped.Delete();
			Assert.IsFalse(File.Exists(unzippedLocation));
		}
	}
}