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
using CatLib.Stl;
using NUnit.Framework;

namespace CatLib.Test.Stl
{
    /// <summary>
    /// 跳跃结点测试
    /// </summary>
    [TestFixture]
    [Category("SkipList")]
    public class SkipNodeTest
    {
        /// <summary>
        /// 新建一个跳跃结点
        /// </summary>
        [Test]
        public void NewSkipNode()
        {
            Assert.DoesNotThrow(() =>
            {
                new SkipNode<string, string>(32);
                new SkipNode<string, string>(32, "hello", "world");
            });
        }

        /// <summary>
        /// 测试无效的结点数据
        /// </summary>
        [Test]
        public void CheckIllegalSkipNodeData()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SkipNode<string, string>(32, null, "world");
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new SkipNode<string, string>(0, "", "world");
            });
        }
    }
}