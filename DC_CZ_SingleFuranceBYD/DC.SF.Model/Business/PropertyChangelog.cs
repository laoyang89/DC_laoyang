﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model.Business
{
    /// <summary>
    /// 属性改变日志泛型类
    /// </summary>
    /// <typeparam name="T">指定类型</typeparam>
    public class PropertyChangelog<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PropertyChangelog()
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="displayName">属性对应页面上的名称</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        public PropertyChangelog(string propertyName, string displayName, string oldValue, string newValue)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="className">类名称</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="displayName">属性对应页面上的名称</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <param name="changedTime">修改时间</param>
        public PropertyChangelog(string className, string propertyName, string displayName, string oldValue, string newValue, DateTime changedTime)
            : this(propertyName, displayName, oldValue, newValue)
        {
            ClassName = className;
            ChangedTime = changedTime;
        }

        /// <summary>
        /// 类名称
        /// </summary>
        public string ClassName { get; set; } = typeof(T).FullName;

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性对应页面上的名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 旧值
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ChangedTime { get; set; } = DateTime.Now;
    }
}
