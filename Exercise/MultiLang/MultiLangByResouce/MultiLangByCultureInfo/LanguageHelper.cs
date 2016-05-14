using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace MultiLangByCultureInfo
{
    class LanguageHelper
    {
        private static ResourceManager _rm;

        private static string _currentLan;

        public static ResourceManager Rm
        {
            get
            {
                return _rm;
            }

            set
            {
                _rm = value;
            }
        }

        public static string CurrentLan
        {
            get
            {
                return _currentLan;
            }

            set
            {
                _currentLan = value;
            }
        }

        /// <summary>
        /// 默认构造函数，加载特定多语言文件
        /// </summary>
        static LanguageHelper()
        {
            //语言版本
            //_currentLan = "en-US";
            CurrentLan = "zh-CN";

            if (string.IsNullOrEmpty(CurrentLan) == true)
            {
                CurrentLan = "zh-CN";
            }
            //初始化语言公共资源文件
            
        }

        /// <summary>
        /// 根据key获取value,如果在资源文件中不存在"Key:,那么将返回空的字符串
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static string GetLan(string key)
        {
            Rm = new ResourceManager("MultiLangByCultureInfo.MainWindow", Assembly.GetExecutingAssembly());
            string resourceValue = string.Empty;
            if (key != "")
            {
                try
                {
                    resourceValue = Rm.GetString(key.Trim(), new CultureInfo(CurrentLan, true));
                }
                catch (Exception)
                {
                    resourceValue = string.Empty;
                }
            }
            return resourceValue;
        }
    }
}
