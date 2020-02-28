using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;

namespace Crawler
{
    /// <summary>
    /// 图片信息解析
    /// </summary>
    public class ImageInfoResolve
    {
        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="url">网页url</param>
        /// <param name="refererUrl">发起请求的地址</param>
        /// <param name="postInfo">帖子信息模型</param>
        /// <returns></returns>
        public Model.ImageInfo GetImageInfo(string url,string refererUrl,Model.PostInfo postInfo)
        {
            Model.ImageInfo imageInfo = new Model.ImageInfo();
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                var ip = new Common.IpAgent().GetIpAgent();
                //request.Proxy = new WebProxy(ip.Item1, ip.Item2);

                request.Timeout = 30 * 1000;//设置30s的超时
                request.UserAgent = new Common.UserAgent().GetUserAgent();
                request.Referer = refererUrl;
                request.Headers.Add("Cookie", "bid=qPv-n0-QSBU; douban-fav-remind=1; trc_cookie_storage=taboola%2520global%253Auser-id%3Dcb888559-a666-412d-913e-09cc7b22e6fe-tuct479d4b8; __utmz=30149280.1570435268.1.1.utmcsr=baidu|utmccn=(organic)|utmcmd=organic; __yadk_uid=1FkijIOtCHRFz9haX3JXTBhcWJg2TTR4; __gads=ID=0ed21b9999b6f6ea:T=1582854389:S=ALNI_Mb7Yij3BQSXnI-Qug7f5VT0j90uuQ; ll=\"118297\"; dbcl2=\"212159950:IaZUDw96Rm4\"; push_doumail_num=0; push_noty_num=0; __utmv=30149280.21215; _ga=GA1.2.467793693.1570435268; _gid=GA1.2.1818635008.1582880890; _pk_ref.100001.8cb4=%5B%22%22%2C%22%22%2C1582886679%2C%22https%3A%2F%2Fwww.baidu.com%2Flink%3Furl%3DmT0AsXXyRr1P8vmPCHLsInIdx2MZ0B1qcj18YnsDrPB-OEHcUPZEiqATT6bMzCMl%26wd%3D%26eqid%3Dedb93f62000f2ca4000000065dc56012%22%5D; _pk_ses.100001.8cb4=*; ap_v=0,6.0; __utma=30149280.467793693.1570435268.1582879406.1582886681.5; __utmc=30149280; __utmt=1; _pk_id.100001.8cb4=f04c04cbf9dc885f.1570435257.6.1582889619.1582882443.; __utmb=30149280.27.5.1582889618946"); request.Method = "GET";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)//发起请求
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        Console.WriteLine($"图片抓取失败，地址为：{url}");
                    else
                    {
                        try
                        {
                            imageInfo.Type = response.Headers.GetValues("content-type")[0];//图片类型
                            imageInfo.Size = response.Headers.GetValues("content-length")[0];//字节大小

                            /* 保存图片 */
                            string imageType = "image/png image/jpg image/jpeg image/gif image/ image/webp";//允许的图片类型
                            const int size = 10240;//10kb
                            if (imageType.IndexOf(imageInfo.Type) >= 0 && Convert.ToInt64(imageInfo.Size) > size)// 图片类型符合，且大小大于10k
                            {
                                string folder = $@"D:\douban\{postInfo.DateTime.ToString("yyyyMMdd")}\{postInfo.Name}";
                                if (!Directory.Exists(folder))//不存在此文件夹则创建
                                    Directory.CreateDirectory(folder);

                                using (FileStream fs = System.IO.File.Create($"{folder}\\{imageInfo.Size}.{imageInfo.Type.Substring(6)}"))//注意路径里面最好不要有中文
                                {
                                    response.GetResponseStream().CopyTo(fs);                                    
                                    fs.Flush();//清空文件流
                                }
                                //System.Drawing.Image img = System.Drawing.Image.FromStream(stream);                               
                                //switch (imageInfo.Type)
                                //{
                                //    case "image/png":
                                //        img.Save($"{folder}\\{imageInfo.Size}.{imageInfo.Type.Substring(6)}", ImageFormat.Png);
                                //        break;
                                //    case "image/jpg":
                                //        img.Save($"{folder}\\{imageInfo.Size}.{imageInfo.Type.Substring(6)}", ImageFormat.Jpeg);
                                //        break;
                                //    case "image/jpeg":
                                //        img.Save($"{folder}\\{imageInfo.Size}.{imageInfo.Type.Substring(6)}", ImageFormat.Jpeg);
                                //        break;
                                //    case "image/gif":
                                //        img.Save($"{folder}\\{imageInfo.Size}.{imageInfo.Type.Substring(6)}", ImageFormat.Gif);
                                //        break;
                                //    case "image/webp":
                                //        //img.Save($"{folder}\\{imageInfo.Size}.{imageInfo.Type.Substring(6)}", ImageFormat.);
                                //        break;
                                //    default:
                                //        Console.WriteLine("此图片文件格式不支持");
                                //        break;
                                //}
                                Console.WriteLine("一张图片已保存");
                            }                                                
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"图片抓取失败,图片地址为：{url},错误信息为：{ex.Message}");
                        }
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return imageInfo;
        }
    }
}
