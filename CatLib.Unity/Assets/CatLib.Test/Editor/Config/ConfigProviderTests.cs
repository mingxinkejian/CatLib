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

using System;
using CatLib.API.Config;
using CatLib.Config;
using CatLib.Core;
#if UNITY_EDITOR || NUNIT
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Category = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
#endif

namespace CatLib.Tests.Config
{
    [TestClass]
    public class ConfigProviderTests
    {

        [TestInitialize]
        public void TestInitialize()
        {
            var app = new Application().Bootstrap();
            app.OnFindType((t) =>
            {
                return Type.GetType(t);
            });
            app.Register(typeof(ConfigProvider));
            app.Init();
        }

        
        [TestMethod]
        public void GetConfig()
        {
            var config = App.Instance.Make<IConfig>();
            Assert.AreEqual(typeof(CatLib.Config.Config), config.GetType());
        }

        [TestMethod]
        public void SetDefault()
        {
            var configManager = App.Instance.Make<IConfigManager>();

            configManager.SetDefault("catlib");
            configManager.Extend(() =>
            {
                return new CatLib.Config.Config();
            });

            Assert.AreEqual(typeof(CatLib.Config.Config), configManager.Get().GetType());
            Assert.AreEqual(typeof(CatLib.Config.Config), configManager.Get("default").GetType());
            Assert.AreNotSame(configManager.Get(), configManager["default"]);

            configManager.SetDefault(string.Empty);
            Assert.AreSame(configManager.Get(), configManager["default"]);
        }
    }
}
