# Skyline.DataMiner.ConnectorAPI.Arista.Manager

## About

### About Skyline.DataMiner.Utils.SoftwareBundle

Skyline.DataMiner.Utils.SoftwareBundle is a package available in the public [nuget store](https://www.nuget.org/) that contain assemblies to manage bundled sets of files with versioning.

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exist. In addition, you can leverage DataMiner Development SoftwareBundles to build you own connectors (also known as "protocols" or "drivers").

> [!TIP]
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer)

### About Skyline Communications

At Skyline Communications, we deal in world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.

## Getting started

You will need to add the following NuGet packages to your automation project from the public [nuget store](https://www.nuget.org/):
- Skyline.DataMiner.Utils.SoftwareBundle

Example code could look as following:
 ```cs
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
```