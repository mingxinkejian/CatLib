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
    public class StringUInt64ConverterTests
    {
        [TestMethod]
        public void TestStringUInt64ConvertTo()
        {
            var converter = new StringUInt64Converter();

            Assert.AreEqual(ulong.Parse(ulong.MinValue.ToString()), converter.ConvertTo(ulong.MinValue.ToString(), typeof(uint)));
            Assert.AreEqual(ulong.Parse("1711318"), converter.ConvertTo("1711318", typeof(ulong)));
            Assert.AreEqual(ulong.Parse(ulong.MaxValue.ToString()), converter.ConvertTo(ulong.MaxValue.ToString(), typeof(uint)));
        }
    }
}
