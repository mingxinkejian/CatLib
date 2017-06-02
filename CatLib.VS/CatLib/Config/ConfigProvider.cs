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

using CatLib.API.Config;

namespace CatLib.Config
{
    /// <summary>
    /// 配置服务提供商
    /// </summary>
    public sealed class ConfigProvider : ServiceProvider
    {
        /// <summary>
        /// 注册配置服务
        /// </summary>
        public override void Register()
        {
            RegisterManager();
            RegisterConfig();
            RegisterLocator();
        }

        /// <summary>
        /// 注册管理器
        /// </summary>
        private void RegisterManager()
        {
            App.Singleton<ConfigManager>().Alias<IConfigManager>().OnResolving((bind, obj) =>
            {
                var configManager = obj as ConfigManager;
                if (configManager == null)
                {
                    return null;
                }

                configManager.Extend(() =>
                {
                    var config = App.Make<IConfig>();
                    config.Reg(App.Make<CodeConfigLocator>());
                    return config;
                });

                return configManager;
            }).Alias("config.manager");
        }

        /// <summary>
        /// 注册配置
        /// </summary>
        private void RegisterConfig()
        {
            App.Bind<Config>((app, param) => new Config()).Alias<IConfig>().Alias("config.container");
        }

        /// <summary>
        /// 注册定位器
        /// </summary>
        private void RegisterLocator()
        {
            App.Bind<CodeConfigLocator>().Alias("config.locator.code");
            App.Singleton<UnitySettingLocator>().Alias("config.locator.unity");
        }
    }
}