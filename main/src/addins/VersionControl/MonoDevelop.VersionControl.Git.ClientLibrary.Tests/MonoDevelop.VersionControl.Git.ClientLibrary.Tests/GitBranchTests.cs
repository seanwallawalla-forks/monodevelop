//
// GitBranchTests.cs
//
// Author:
//       Mike Krüger <mikkrg@microsoft.com>
//
// Copyright (c) 2019 Microsoft Corporation. All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using NUnit.Framework;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using System.IO;

namespace MonoDevelop.VersionControl.Git.ClientLibrary.Tests
{
	public class GitTestBase
	{
		protected static async Task RunTest (Func<string, Task> action)
		{
			var path = TestUtil.CreateTempDirectory ();
			try {
				var result = await GitInit.InitAsync (path);
				Assert.IsTrue (result.Success, "git init failed:" + result.ErrorMessage);
				await action (path);
			} finally {
				if (Directory.Exists (path))
					Directory.Delete (path, true);
			}
		}
	}

	[TestFixture]
	public class GitBranchTests : GitTestBase
	{
		[Test]
		public Task GetCurrentBranch()
		{
			return RunTest (async root => {
				var currentBranch = await BranchUtil.GetCurrentBranchAsync (root);
				Assert.AreEqual ("master", currentBranch.Name);
			});
		}
	}
}