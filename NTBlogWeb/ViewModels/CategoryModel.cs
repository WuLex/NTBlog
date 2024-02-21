using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.ViewModels
{
    public class CategoryModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }
    }
}