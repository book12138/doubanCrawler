using System;
using System.Threading;
using Crawler;

namespace doubanPachongSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("已启动");
            PagesProcessing pagesProcessing = new PagesProcessing();
            const int max = 25 * 50;
            int i = 0;
            while (true)
            {
                pagesProcessing.Resolver($"https://www.douban.com/group/lvxing/discussion?start={i}");
                i += 25;
                if (i == max)
                    break;
                Console.WriteLine($"第{i / 25 + 1}页已经开始");
                Thread.Sleep(10000);
            }
                
            Console.ReadKey();
        }
    }
}
