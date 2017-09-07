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

using System.Text;

namespace CatLib.API.Hashing
{
    /// <summary>
    /// 哈希
    /// </summary>
    public interface IHashing
    {
        /// <summary>
        /// 计算校验和
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="checksum">使用校验类类型</param>
        /// <returns>校验和</returns>
        long Checksum(byte[] buffer , Checksums checksum = Checksums.Crc32);

        /// <summary>
        /// 对输入值进行加密性Hash
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="factor">加密因子</param>
        /// <returns>哈希值</returns>
        string HashPassword(string input, int factor = 10);

        /// <summary>
        /// 验证输入值和加密性哈希值是否匹配
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="hash">哈希值</param>
        /// <returns>是否匹配</returns>
        bool CheckPassword(string input, string hash);

        /// <summary>
        /// 对输入值进行非加密哈希
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="hash">使用的哈希算法</param>
        /// <returns>哈希值</returns>
        uint HashString(string input, Hashes hash = Hashes.MurmurHash);

        /// <summary>
        /// 对输入值进行非加密哈希
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="encoding">编码</param>
        /// <param name="hash">使用的哈希算法</param>
        /// <returns>哈希值</returns>
        uint HashString(string input, Encoding encoding, Hashes hash = Hashes.MurmurHash);

        /// <summary>
        /// 对输入值进行非加密哈希
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="hash">使用的哈希算法</param>
        /// <returns>哈希值</returns>
        uint HashByte(byte[] input, Hashes hash = Hashes.MurmurHash);
    }
}
