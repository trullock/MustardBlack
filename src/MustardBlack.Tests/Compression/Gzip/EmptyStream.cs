﻿using System.IO;
using MustardBlack.Compression;
using Xunit;

namespace MustardBlack.Tests.Compression.Gzip
{
	public class EmptyStream : Specification
	{
		Stream gzipStream;
		MemoryStream memoryStream;

		protected override void Given()
		{
			this.memoryStream = new MemoryStream();
			this.gzipStream = new GzipStreamWrapper(this.memoryStream, () =>
			{
				Assert.False(true, "Should not execute");
			}, true);
		}

		protected override void When()
		{
			this.gzipStream.Flush();
		}

		[Then]
		public void MemoryStreamShouldBeEmpty()
		{
			this.memoryStream.Length.ShouldEqual(0);
		}
	}
}