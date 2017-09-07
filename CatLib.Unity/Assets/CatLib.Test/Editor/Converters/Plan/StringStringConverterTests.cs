﻿/*
 * This file is part of the CatLib package.
 *
 * (c) Yu Bin <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: http://catlib.io/
 */

using System.Globalization;
using CatLib.Converters.Plan;

#if UNITY_EDITOR || NUNIT
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Category = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
#endif

namespace CatLib.Tests.Converters.Plan
{
    [TestClass]
    public class StringStringConverterTests
    {
        [TestMethod]
        public void TestStringStringConvertTo()
        {
            var converter = new StringStringConverter();
            Assert.AreEqual("some in put some out put" ,converter.ConvertTo("some in put some out put", typeof(string)));
            Assert.AreEqual("112.2f", converter.ConvertTo("112.2f", typeof(string)));
        }
    }
}
