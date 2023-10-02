using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Versioning;
using Skyline.DataMiner.Utils.SoftwareBundle;

namespace Skyline.DataMiner.Utils.SoftwareBundle.Tests
{
	[TestClass()]
	public class SoftwareBundleInfoTests
	{
		[TestMethod()]
		public void DeserializeTest()
		{
			var info = new SoftwareBundleInfo("testSoftwareBundle", new SemanticVersion(1, 2, 3));
			string serialized = info.Serialize();
			var deserialized = SoftwareBundleInfo.Deserialize(serialized);
			Assert.IsNotNull(deserialized, "deserialized IsNotNull");
			Assert.AreEqual("testSoftwareBundle", deserialized.Name, "deserialized.Name AreEqual");
			Assert.AreEqual(new SemanticVersion(1, 2, 3), deserialized.Version, "deserialized.Version AreEqual");
			Assert.IsNull(deserialized.OS, "deserialized.OS IsNull");
			Assert.IsNull(deserialized.Arch, "deserialized.Arch IsNull");
			Assert.AreEqual(new SemanticVersion(0,0,0), deserialized.SoftwareBundleVersion, "deserialized.SoftwareBundleVersion AreEqual");
		}

		[TestMethod()]
		public void DeserializeTest2()
		{
			var info = new SoftwareBundleInfo("testSoftwareBundle", new SemanticVersion(1, 2, 3), "DataMiner", "CassandraCluster");
			string serialized = info.Serialize();
			var deserialized = SoftwareBundleInfo.Deserialize(serialized);
			Assert.IsNotNull(deserialized, "deserialized IsNotNull");
			Assert.AreEqual("testSoftwareBundle", deserialized.Name, "deserialized.Name AreEqual");
			Assert.AreEqual(new SemanticVersion(1, 2, 3), deserialized.Version, "deserialized.Version AreEqual");
			Assert.AreEqual("DataMiner", deserialized.OS, "deserialized.OS AreEqual");
			Assert.AreEqual("CassandraCluster", deserialized.Arch, "deserialized.Arch AreEqual");
			Assert.AreEqual(new SemanticVersion(0, 0, 0), deserialized.SoftwareBundleVersion, "deserialized.SoftwareBundleVersion AreEqual");
		}

		[TestMethod()]
		public void DeserializeTestDependancies()
		{
			var info = new SoftwareBundleInfo("testSoftwareBundle", new SemanticVersion(1, 2, 3), "DataMiner", "CassandraCluster");
			info.Dependencies = new Dictionary<string, SemanticVersion>
			{
				{ "Depends1", new SemanticVersion(13,13,13) },
				{ "Depends2", new SemanticVersion(1,1,1) },
			};
			string serialized = info.Serialize();
			var deserialized = SoftwareBundleInfo.Deserialize(serialized);
			Assert.IsNotNull(deserialized, "deserialized IsNotNull");
			Assert.AreEqual("testSoftwareBundle", deserialized.Name, "deserialized.Name AreEqual");
			Assert.AreEqual(new SemanticVersion(1, 2, 3), deserialized.Version, "deserialized.Version AreEqual");
			Assert.AreEqual("DataMiner", deserialized.OS, "deserialized.OS AreEqual");
			Assert.AreEqual("CassandraCluster", deserialized.Arch, "deserialized.Arch AreEqual");
			Assert.AreEqual(new SemanticVersion(0, 0, 0), deserialized.SoftwareBundleVersion, "deserialized.SoftwareBundleVersion AreEqual");
			bool areDependenciesEqual = new DictionaryComparer<string, SemanticVersion>().Equals(info.Dependencies, deserialized.Dependencies);
			Assert.IsTrue(areDependenciesEqual, "deserialized.Dependencies AreEqual");
		}

		public class DictionaryComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>>
		{
			private IEqualityComparer<TValue> valueComparer;

			public DictionaryComparer(IEqualityComparer<TValue> valueComparer = null)
			{
				this.valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
			}

			public bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y)
			{
				if (x.Count != y.Count)
					return false;
				if (x.Keys.Except(y.Keys).Any())
					return false;
				if (y.Keys.Except(x.Keys).Any())
					return false;
				foreach (var pair in x)
					if (!valueComparer.Equals(pair.Value, y[pair.Key]))
						return false;
				return true;
			}

			public int GetHashCode(Dictionary<TKey, TValue> obj)
			{
				throw new NotImplementedException();
			}
		}

		public class ListComparer<T> : IEqualityComparer<List<T>>
		{
			private IEqualityComparer<T> valueComparer;
			public ListComparer(IEqualityComparer<T> valueComparer = null)
			{
				this.valueComparer = valueComparer ?? EqualityComparer<T>.Default;
			}

			public bool Equals(List<T> x, List<T> y)
			{
				return x.SetEquals(y, valueComparer);
			}

			public int GetHashCode(List<T> obj)
			{
				throw new NotImplementedException();
			}
		}
	}

	public static class TestExtensions
	{
		public static bool SetEquals<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
		{
			return new HashSet<T>(second, comparer ?? EqualityComparer<T>.Default)
				.SetEquals(first);
		}
	}
}