using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 帖子信息
    /// </summary>
    public class PostInfo
    {
        /// <summary>
        /// 发帖时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 帖子名称
        /// </summary>
        public string Name { get; set; }
    }
}
