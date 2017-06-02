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
using UnityEngine;

namespace CatLib.Bootstrap
{
    /// <summary>
    /// 程序入口
    /// Program Entry
    /// </summary>
    public sealed class Program : MonoBehaviour
    {
        /// <summary>
        /// 初始化程序
        /// </summary>
        public void Awake()
        {
            var application = new Core.Application(this);
            application.OnFindType((type) => Type.GetType(type));
            application.Bootstrap(Bootstrap.BootStrap).Init();
        }
    }
}