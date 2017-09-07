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

#if CATLIB
using CatLib.API.Hashing;

namespace CatLib.Hashing
{
    /// <summary>
    /// 哈希服务提供者
    /// </summary>
    public sealed class HashingProvider : IServiceProvider
    {
        /// <summary>
        /// 服务提供者初始化
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// 当注册服务提供者
        /// </summary>
        public void Register()
        {
            App.Singleton<Hashing>().Alias<IHashing>();
        }
    }
}
#endif