﻿
using UnityEngine;
using CatLib.API.Flux;

namespace CatLib.Flux {

    /// <summary>
    /// 视图
    /// </summary>
    public abstract class View : MonoBehaviour {

        /// <summary>
        /// 关注的容器
        /// </summary>
        /// <returns></returns>
        protected virtual IStore[] Observer
        {
            get
            {
                return new IStore[] { };
            }
        }

        /// <summary>
        /// 当起始时
        /// </summary>
        public void Start()
        {
            for(int i = 0; i < Observer.Length; i++)
            {
                Observer[i].AddListener(OnChange);
            }
        }

        /// <summary>
        /// 当释放时
        /// </summary>
        public void OnDestroy()
        {
            for (int i = 0; i < Observer.Length; i++)
            {
                Observer[i].RemoveListener(OnChange);
            }
        }

        /// <summary>
        /// 当存储发生变更时
        /// </summary>
        /// <param name="notification"></param>
        protected virtual void OnChange(INotification notification) { }

    }

}
