namespace Skyline.DataMiner.Utils.SoftwareBundle
{
	using System;
	using System.IO;
	using System.IO.Compression;
	using System.Linq;
	using Newtonsoft.Json;

	internal class ZippedSoftwareBundle : IZippedSoftwareBundle
	{
		private SoftwareBundleInfo _info;

		public ZippedSoftwareBundle(string path)
		{
			Path = path;
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
			// Read software bundle and locate info.json
			var serializer = new JsonSerializer();
			using (ZipArchive zip = ZipFile.Open(Path, ZipArchiveMode.Read))
			{
				var infoEntry = zip.GetEntry("info.json");
				using (Stream infoStream = infoEntry.Open())
				{
					using (StreamReader textReader = new StreamReader(infoStream))
					{
						using (JsonTextReader jsonReader = new JsonTextReader(textReader))
						{
							_info = serializer.Deserialize<SoftwareBundleInfo>(jsonReader);
						}
					}
				}
			}
		}

		public IUnZippedSoftwareBundle CreateUnzippedSoftwareBundle(string destinationFolder)
		{
			if (Directory.Exists(destinationFolder))
			{
				var directory = new DirectoryInfo(destinationFolder);
				if (directory.GetFiles("info.json", SearchOption.TopDirectoryOnly).Any())
				{
					throw new ArgumentException($"The destinationFolder '{destinationFolder}' already contains an info.json file.", "destinationFolder");
				}
			}
            else
            {
				Directory.CreateDirectory(destinationFolder);
			}
            
			ZipFile.ExtractToDirectory(Path, destinationFolder);
			return new UnZippedSoftwareBundle(destinationFolder);
		}

		public void Delete()
		{
			File.Delete(Path);
		}
	}
}
