using NTBlogWeb.Configs.Models;
using NTBlogWeb.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NTBlogWeb.Core.Util;
using Microsoft.Extensions.Caching.Memory;

namespace NTBlogWeb.Configs
{
    public class ConfigHelper
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static Setting GetBasicConfig()
        {
            var settingCache = MemoryCacheHelper.Cache.Get<Setting>("Setting"); // HttpContext.Current.Cache["Setting"];
            if (settingCache == null)
            {
                var settingString = FileHelper.ReadFile(WebHelper.GetFilePath("setting.json"));
                var setting = JsonConvert.DeserializeObject<Setting>(settingString);

                MemoryCacheHelper.Cache.Set("Setting", setting);
                //HttpContext.Current.Cache.Insert("Setting", setting);

                return setting;
            }

            return (Setting)settingCache;
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="setting"></param>
        public static void SetBasicConfig(Setting setting)
        {
            var path = WebHelper.GetFilePath("~/Configs/Files/") + "setting.json";
            using (var fs = File.Create(path))
            {
                fs.Close();
            }

            var jsonStr = JsonConvert.SerializeObject(setting) ?? throw new ArgumentNullException("JsonConvert.SerializeObject(setting)");
            using (var fr = new StreamWriter(path))
            {
                fr.Write(jsonStr);
                fr.Close();
            }

            //清除缓存
            //HttpContext.Current.Cache.Remove("Setting");
        }
    }
}