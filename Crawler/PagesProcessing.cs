using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Crawler
{
    /// <summary>
    /// 处理每一页中的内容
    /// </summary>
    public class PagesProcessing
    {
        /// <summary>
        /// 解析器
        /// </summary>
        /// <param name="url">每一页的url</param>
        /// <returns></returns>
        public Task Resolver(string url)
            => Task.Run(() =>
            {
                try
                {
                    //Task.Delay(11000);//延迟十一秒
                    PageHtmlDownLoad pageHtmlDownLoad = new PageHtmlDownLoad();

                    /* 请求豆瓣服务器，获得gzip压缩包，然后解压得到HTML */
                    string html = pageHtmlDownLoad.DownLoad(url);

                    /* 使用xpath定位 */
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    List<HtmlNode> htmlNodes = new List<HtmlNode>();
                    for (int i = 0; i < 25; i++)
                    {
                        var node = doc.DocumentNode.SelectSingleNode($"//*[@id=\"content\"]/div/div[1]/div[2]/table/tr[{i + 2}]/td[1]/a");                        
                        if (node != null)
                            htmlNodes.Add(node);
                        else
                            break;
                    }                    

                    /* 提取出该页所有链接地址，并进行下一步信息抓取 */
                    PostDetailsProcessing postDetailsProcessing = new PostDetailsProcessing();
                    foreach (var item in htmlNodes)
                    {
                        if (item != null)
                        {
                            var href = item.GetAttributeValue("href", "none");
                            if (href != "none")
                                postDetailsProcessing.Resolver(href);
                            Task.Delay(5000);
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"在抓取某一页的数据时，出现未知的错误，系统错误信息为：{e.Message}");
                }
            });
    }
}
