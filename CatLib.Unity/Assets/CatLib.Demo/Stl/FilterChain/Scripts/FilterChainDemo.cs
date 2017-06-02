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

using CatLib.API;
using CatLib.Stl;

namespace CatLib.Demo.FilterChain
{

    public class FilterChainDemo : ServiceProvider
    {

        public override void Init()
        {
            App.On(ApplicationEvents.OnApplicationStartComplete, (sender, e) =>
            {
                var filters = new FilterChain<string>();

                bool isCall = true;
                filters.Add((data, next) =>
                {
                    //这里模拟一个递归调用
                    if (isCall)
                    {
                        isCall = false;
                        filters.Do("sub", (d) => UnityEngine.Debug.Log("filter end , " + d));
                    }
                    UnityEngine.Debug.Log("hello this is filter chain 1 , " + data);
                    next(data);
                    UnityEngine.Debug.Log("back filter chain 1");
                });

                filters.Add((data, next) =>
                {
                    UnityEngine.Debug.Log("hello this is filter chain 2 , " + data);
                    next(data);
                    UnityEngine.Debug.Log("back filter chain 2");
                });

                filters.Do("hello world", (data) => UnityEngine.Debug.Log("filter end , " + data));

            });
        }

        public override void Register(){ }
    }
}