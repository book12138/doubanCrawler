using HtmlAgilityPack;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Crawler
{
    /// <summary>
    /// 帖子详情页处理
    /// </summary>
    public class PostDetailsProcessing
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
                    var imageElementList = doc.DocumentNode.SelectNodes("//*[@id=\"link-report\"]/div[1]/div[1]/div/div[1]/img");//提取所有图片
                    ////*[@id="link-report"]/div/div/div[1]
                    Model.PostInfo postInfo = new Model.PostInfo()
                    {
                        DateTime = Convert.ToDateTime(doc.DocumentNode.SelectSingleNode("//*[@id=\"topic-content\"]/div[2]/h3/span[2]").InnerText),
                        Name = doc.DocumentNode.SelectSingleNode("//*[@id=\"content\"]/h1").InnerText.Trim(new char[] { ' ', '\n' })
                    };

                    /* 提取出所有的图片信息 */
                    List<ImageInfo> imageInfos = new List<ImageInfo>();
                    ImageInfoResolve imageInfoResolve = new ImageInfoResolve();
                    if (imageElementList != null)
                    {
                        foreach (var item in imageElementList)
                        {
                            string src = item.GetAttributeValue("src", "none");
                            if (src != "none")
                                imageInfos.Add(imageInfoResolve.GetImageInfo(src,url,postInfo));                            
                        }
                    }

                    //Console.WriteLine("一张帖子的数据已经收集完毕，正在准备下载内部图片");                    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"在抓取某一页的数据时，出现未知的错误，系统错误信息为：{e.Message}");
                }

            });
    }
}
