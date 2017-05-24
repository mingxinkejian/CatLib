﻿
namespace CatLib.API.Config
{
    /// <summary>
    /// 配置定位器
    /// </summary>
    public interface IConfigLocator
    {
        /// <summary>
        /// 设定值
        /// </summary>
        /// <param name="name">配置名</param>
        /// <param name="value">配置值</param>
        void Set(string name, string value);

        /// <summary>
        /// 根据配置名获取配置的值
        /// </summary>
        /// <param name="name">配置名</param>
        /// <param name="value">配置值</param>
        /// <returns>是否获取到配置</returns>
        bool TryGetValue(string name , out string value);

        /// <summary>
        /// 保存配置
        /// </summary>
        void Save();
    }
}
