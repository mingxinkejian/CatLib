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
using System.Collections.Generic;
using CatLib.API;
using CatLib.API.Config;
using CatLib.Stl;

namespace CatLib.Config
{
    /// <summary>
    /// 配置容器
    /// </summary>
    internal sealed class Config : IConfig
    {
        /// <summary>
        /// 配置定位器
        /// </summary>
        private readonly SortSet<IConfigLocator, int> locators;

        /// <summary>
        /// 类型转换器
        /// </summary>
        private readonly Dictionary<Type, ITypeStringConverter> typeStringConverters;

        /// <summary>
        /// 构造一个配置容器
        /// </summary>
        public Config()
        {
            locators = new SortSet<IConfigLocator, int>();
            typeStringConverters = new Dictionary<Type, ITypeStringConverter>
            {
                { typeof(bool), new BoolStringConverter() },
                { typeof(byte), new ByteStringConverter() },
                { typeof(char), new CharStringConverter() },
                { typeof(DateTime), new DateTimeStringConverter() },
                { typeof(decimal), new DecimalStringConverter() },
                { typeof(double), new DoubleStringConverter() },
                { typeof(Enum), new EnumStringConverter() },
                { typeof(short), new Int16StringConverter() },
                { typeof(int), new Int32StringConverter() },
                { typeof(long), new Int64StringConverter() },
                { typeof(sbyte), new SByteStringConverter() },
                { typeof(float), new SingleStringConverter() },
                { typeof(string), new StringStringConverter() },
                { typeof(ushort), new UInt16StringConverter() },
                { typeof(uint), new UInt32StringConverter() },
                { typeof(ulong), new UInt64StringConverter() }
            };
        }

        /// <summary>
        /// 增加转换器
        /// </summary>
        /// <param name="type">类型对应的转换器</param>
        /// <param name="converter">转换器</param>
        public void AddConverter(Type type, ITypeStringConverter converter)
        {
            Guard.NotNull(type, "type");
            Guard.NotNull(converter, "converter");
            typeStringConverters.Remove(type);
            typeStringConverters.Add(type, converter);
        }

        /// <summary>
        /// 注册一个配置定位器
        /// 框架会依次遍历配置定位器来获取配
        /// </summary>
        /// <param name="locator">配置定位器</param>
        /// <param name="priority">查询优先级(值越小越优先)</param>
        public void Reg(IConfigLocator locator , int priority = int.MaxValue)
        {
            Guard.NotNull(locator, "locator");
            locators.Add(locator, priority);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public void Save()
        {
            foreach (var locator in locators)
            {
                locator.Save();
            }
        }

        /// <summary>
        /// 根据配置名获取配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get { return Get<string>(name); }
        }

        /// <summary>
        /// 设定配置的值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="name">配置名</param>
        /// <param name="value">配置的值</param>
        public void Set<T>(string name, T value)
        {
            Guard.NotNull(name, "name");
            if (locators.Count <= 0)
            {
                throw new RuntimeException("No Reg locator. please Reg() locator first.");
            }

            IConfigLocator configLocator = null;
            string val;
            foreach (var locator in locators)
            {
                if (locator.TryGetValue(name, out val))
                {
                    configLocator = locator;
                }
            }

            if (configLocator == null)
            {
                configLocator = locators.Last();
            }

            ITypeStringConverter converter;
            if (!typeStringConverters.TryGetValue(typeof(T), out converter))
            {
                throw new ConverterException("Can not find [" + typeof(T) + "] coverter impl.");
            }

            configLocator.Set(name, converter.ConvertToString(value));
        }

        /// <summary>
        /// 根据配置名获取配置
        /// </summary>
        /// <typeparam name="T">配置最终转换到的类型</typeparam>
        /// <param name="name">配置所属类型的名字</param>
        /// <param name="def">当找不到配置时的默认值</param>
        /// <returns>配置的值，如果找不到则返回默认值</returns>
        public T Get<T>(string name, T def = default(T))
        {
            Guard.NotNull(name, "name");
            try
            {
                string val = null;
                foreach (var locator in locators)
                {
                    if (locator.TryGetValue(name, out val))
                    {
                        break;
                    }
                }

                if (val == null)
                {
                    return def;
                }

                ITypeStringConverter converter;
                if (typeStringConverters.TryGetValue(typeof(T), out converter))
                {
                    return (T)converter.ConvertFromString(val, typeof(T));
                }

                return def;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Field [" + name + "] is can not conversion to " + typeof(T) + ".", ex);
            }
        }
    }
}