// // <copyright>
// //   Copyright (c) 2012 Codefarts
// //   All rights reserved.
// //   contact@codefarts.com
// //   http://www.codefarts.com
// // </copyright>

namespace Codefarts.ExtensionMethods.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Base Types")]
    public class StringExtensionTests
    {
        [TestMethod]
        public void IndexOfFolderInPath_Root_PathIsNull()
        {
            string path = null;
            Assert.AreEqual(-1, path.IndexOfFolderInPath("Root"));
        }

        [TestMethod]
        public void IndexOfFolderInPath_Root_PathIsEmpty()
        {
            Assert.AreEqual(-1, string.Empty.IndexOfFolderInPath("Root"));
        }

        [TestMethod]
        public void IndexOfFolderInPath_SingleSlash()
        {
            Assert.AreEqual(-1, @"\".IndexOfFolderInPath("Root"));
        }

        [TestMethod]
        public void IndexOfFolderInPath_Root()
        {
            Assert.AreEqual(0, @"\Root\Child\ChildB".IndexOfFolderInPath("Root"));
        }

        [TestMethod]
        public void IndexOfFolderInPath_Root_NoPreceedingSlash()
        {
            Assert.AreEqual(0, @"Root\Child\ChildB".IndexOfFolderInPath("Root"));
        }

        [TestMethod]
        public void IndexOfFolderInPath_Child()
        {
            Assert.AreEqual(1, @"\Root\Child\ChildB".IndexOfFolderInPath("Child"));
        }

        [TestMethod]
        public void IndexOfFolderInPath_ChildB()
        {
            Assert.AreEqual(2, @"\Root\Child\ChildB".IndexOfFolderInPath("ChildB"));
        }

        [TestMethod]
        public void IndexOfFolderInPath_ChildB_TrailingSlash()
        {
            Assert.AreEqual(2, @"\Root\Child\ChildB".IndexOfFolderInPath("ChildB"));
        }

        [TestMethod]
        public void PathContainsFolder_NullPath()
        {
            string path = null;
            Assert.IsFalse(path.PathContainsFolder("Root"));
        }

        [TestMethod]
        public void PathContainsFolder_EmptyString()
        {
            Assert.IsFalse(string.Empty.PathContainsFolder("Root"));
        }

        [TestMethod]
        public void PathContainsFolder_Root_NoPreceedingSlash()
        {
            Assert.IsTrue(@"Root\Child\ChildB".PathContainsFolder("Root"));
        }

        [TestMethod]
        public void PathContainsFolder_Root()
        {
            Assert.IsTrue(@"\Root\Child\ChildB".PathContainsFolder("Root"));
        }

        [TestMethod]
        public void PathContainsFolder_Child()
        {
            Assert.IsTrue(@"\Root\Child\ChildB".PathContainsFolder("Child"));
        }

        [TestMethod]
        public void PathContainsFolder_Child_MixedSlashes()
        {
            Assert.IsTrue(@"\Root/Child\ChildB".PathContainsFolder("Child"));
        }

        [TestMethod]
        public void PathContainsFolder_ChildB()
        {
            Assert.IsTrue(@"\Root\Child\ChildB".PathContainsFolder("ChildB"));
        }

        [TestMethod]
        public void PathContainsFolder_ChildB_WithTrailingSlash()
        {
            Assert.IsTrue(@"\Root\Child\ChildB\".PathContainsFolder("ChildB"));
        }

        [TestMethod]
        public void AllTheSame_Empty_String()
        {
            Assert.IsFalse(string.Empty.AllTheSame());
        }

        [TestMethod]
        public void AllTheSame_Null_String()
        {
            string value = null;
            Assert.IsFalse(value.AllTheSame());
        }

        [TestMethod]
        public void AllTheSame_Minus_String()
        {
            Assert.IsTrue(new String('-', 10).AllTheSame());
        }

        [TestMethod]
        public void AllTheSame_Mixed_String()
        {
            Assert.IsFalse("1234567890".AllTheSame());
        }

        [TestMethod]
        public void RemoveCharsFromStart()
        {
            Assert.AreEqual("Nums0123456789", "1234567890Nums0123456789".RemoveCharactersFromStart("0123456789".ToCharArray()));
        }

        [TestMethod]
        public void RemoveCharsFromStart_SingleSlash()
        {
            Assert.AreEqual(string.Empty, @"\".RemoveCharactersFromStart(@"\".ToCharArray()));
        }

        [TestMethod]
        public void RemoveCharsFromStart_EmptyString()
        {
            Assert.AreEqual(string.Empty, string.Empty.RemoveCharactersFromStart("0123456789".ToCharArray()));
        }

        [TestMethod]
        public void RemoveCharsFromStart_StartingCharacterNotSpecified()
        {
            Assert.AreEqual("*1234567890Nums0123456789", "*1234567890Nums0123456789".RemoveCharactersFromStart("0123456789".ToCharArray()));
        }

        [TestMethod]
        public void StartsWithAny()
        {
            Assert.IsTrue("System.IO".StartsWithAny(new[] { "System", "Microsoft" }));
        }

        [TestMethod]
        public void StartsWithAny_FailureCase()
        {
            Assert.IsFalse("System.IO".StartsWithAny(new[] { "Codefarts", "Created by: X" }));
        }
    }
}
