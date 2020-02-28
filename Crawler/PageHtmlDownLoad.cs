using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Crawler
{
    /// <summary>
    /// 页面HTML下载
    /// </summary>
    public class PageHtmlDownLoad
    {
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url">网页url</param>
        /// <returns></returns>
        public string DownLoad(string url)
        {
            string html = string.Empty;
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                var ip = new Common.IpAgent().GetIpAgent();
                //request.Proxy = new WebProxy(ip.Item1, ip.Item2);

                request.Timeout = 30 * 1000;//设置30s的超时
                request.ContentType = "text/html; charset=utf-8";
                request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
                request.UserAgent = new Common.UserAgent().GetUserAgent();
                request.Headers.Add("Cookie", "bid=qPv-n0-QSBU; douban-fav-remind=1; trc_cookie_storage=taboola%2520global%253Auser-id%3Dcb888559-a666-412d-913e-09cc7b22e6fe-tuct479d4b8; __utmz=30149280.1570435268.1.1.utmcsr=baidu|utmccn=(organic)|utmcmd=organic; __yadk_uid=1FkijIOtCHRFz9haX3JXTBhcWJg2TTR4; __gads=ID=0ed21b9999b6f6ea:T=1582854389:S=ALNI_Mb7Yij3BQSXnI-Qug7f5VT0j90uuQ; ll=\"118297\"; dbcl2=\"212159950:IaZUDw96Rm4\"; push_doumail_num=0; push_noty_num=0; __utmv=30149280.21215; _ga=GA1.2.467793693.1570435268; _gid=GA1.2.1818635008.1582880890; _pk_ref.100001.8cb4=%5B%22%22%2C%22%22%2C1582886679%2C%22https%3A%2F%2Fwww.baidu.com%2Flink%3Furl%3DmT0AsXXyRr1P8vmPCHLsInIdx2MZ0B1qcj18YnsDrPB-OEHcUPZEiqATT6bMzCMl%26wd%3D%26eqid%3Dedb93f62000f2ca4000000065dc56012%22%5D; _pk_ses.100001.8cb4=*; ap_v=0,6.0; __utma=30149280.467793693.1570435268.1582879406.1582886681.5; __utmc=30149280; __utmt=1; _pk_id.100001.8cb4=f04c04cbf9dc885f.1570435257.6.1582889619.1582882443.; __utmb=30149280.27.5.1582889618946");
                //request.Connection = "keep-alive";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Host = "www.douban.com";
                request.Referer = "https://www.douban.com/group/lvxing/";
                request.Method = "GET";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)//发起请求
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        Console.WriteLine($"服务端错误响应，地址为：{url}");
                    else
                    {
                        try
                        {
                            Common.GZipCompress gZipCompress = new Common.GZipCompress();
                            html = gZipCompress.DeCompress(response.GetResponseStream());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"响应报文获取失败，地址为：{url},错误信息为：{ex.Message}");
                            html = null;
                        }
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return html;
        }
    }
}
