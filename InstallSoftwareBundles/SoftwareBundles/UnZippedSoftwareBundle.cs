namespace Skyline.DataMiner.Utils.SoftwareBundle
{
	using System.Collections.Generic;
	using System.IO;
	using System.IO.Compression;
	using System.Linq;

	internal class UnZippedSoftwareBundle : IUnZippedSoftwareBundle
	{
		private SoftwareBundleInfo _info;

		public UnZippedSoftwareBundle(string folderPath)
		{
			Path = folderPath;
		}

		public string Path { get; private set; }

		public SoftwareBundleInfo SoftwareBundleInfo
		{
			get
			{
				if (_info == null)
				{
					GetInfoFile();
				}

				return _info;
			}
		}

		private void GetInfoFile()
		{
			var filePath = System.IO.Path.Combine(Path, "info.json");
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"Unable to find '{filePath}'");
			}

			_info = SoftwareBundleInfo.Deserialize(File.ReadAllText(filePath));
		}

		public IZippedSoftwareBundle CreateZippedSoftwareBundle(string destinationPath)
		{
			ZipFile.CreateFromDirectory(Path, destinationPath);
			return new ZippedSoftwareBundle(destinationPath);
		}

		public void Delete()
		{
			DirectoryInfo directory = new DirectoryInfo(Path);
			directory.Delete(true);
		}

		public IEnumerable<string> GetFiles()
		{
			return Directory.GetFiles(Path).Where(f => System.IO.Path.GetFileName(f) != "info.json");
		}
	}
}
