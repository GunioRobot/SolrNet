﻿using System;
using System.Collections.Generic;
using MbUnit.Framework;
using SolrNet.Impl.FacetQuerySerializers;

namespace SolrNet.Tests
{
	[TestFixture]
	public class SolrFacetPivotQueryTest
	{
        private static readonly SolrFacetPivotQuerySerializer serializer = new SolrFacetPivotQuerySerializer();
		[Test]
		public void SinglePivotTest()
		{
			var q = new SolrFacetPivotQuery()
			{
				Fields = new[] { "manu_exact,inStock" },
				MinCount = 1
			};

            var r = serializer.Serialize(q);
			Assert.Contains(r, KV("facet.pivot", "manu_exact,inStock"));
			Assert.Contains(r, KV("facet.pivot.mincount", "1"));
		}

		[Test]
		public void SinglePivotTestWithoutMinCount()
		{
			var q = new SolrFacetPivotQuery()
			{
				Fields = new[] { "manu_exact,inStock" }
			};

            var r = serializer.Serialize(q);
			Assert.Contains(r, KV("facet.pivot", "manu_exact,inStock"));
			foreach(var kvPair in r)
			{
				Assert.DoesNotContain(kvPair.Key, "facet.pivot.mincount");
			}

		}
		[Test]
		public void MultiplePivotTest()
		{
			var q = new SolrFacetPivotQuery()
			{
				Fields = new[] { "manu_exact,inStock", "inStock,cat" },
				MinCount = 1
			};

            var r = serializer.Serialize(q);
			Assert.Contains(r, KV("facet.pivot", "manu_exact,inStock"));
			Assert.Contains(r, KV("facet.pivot", "inStock,cat"));
			Assert.Contains(r, KV("facet.pivot.mincount", "1"));
		}

		public KeyValuePair<K, V> KV<K, V>(K key, V value)
		{
			return new KeyValuePair<K, V>(key, value);
		}
	}
}
