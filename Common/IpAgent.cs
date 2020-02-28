using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// ip代理
    /// </summary>
    public class IpAgent
    {
        /// <summary>
        /// 获取ip代理
        /// </summary>
        /// <returns>第一个参数是ip，第二个参数是端口号</returns>
        public Tuple<string, int> GetIpAgent()
        {
            Tuple<string, int>[] ipInfos = {
                new Tuple<string, int>("60.208.44.228",80),
                new Tuple<string, int>("218.202.219.82",80),
                new Tuple<string, int>("39.134.108.92",8080),
                new Tuple<string, int>("39.134.169.217",8080),
                new Tuple<string, int>("39.134.108.89",8080),
                new Tuple<string, int>("119.27.170.46",8888),
                new Tuple<string, int>("220.196.42.158",8081),
                new Tuple<string, int>("61.160.190.146",8090),
                new Tuple<string, int>("157.255.144.77",80),
                new Tuple<string, int>("223.111.252.130",80),
                new Tuple<string, int>("221.232.233.159",1080),
                new Tuple<string, int>("118.126.93.245",1080),
                new Tuple<string, int>("222.89.28.218",1080),
                new Tuple<string, int>("111.2.123.27",80),
                new Tuple<string, int>("115.159.201.179",80),
                new Tuple<string, int>("111.11.227.82",80),
                new Tuple<string, int>("111.11.227.112",80),
                new Tuple<string, int>("111.11.227.81",80),
                new Tuple<string, int>("111.11.227.81",8080),
                new Tuple<string, int>("111.11.227.82",8080),
                new Tuple<string, int>("111.11.227.83",80),
                new Tuple<string, int>("111.11.227.114",80),
                new Tuple<string, int>("111.11.227.112",8080),
                new Tuple<string, int>("111.11.227.114",8080),
                new Tuple<string, int>("222.186.31.100",1080),
                new Tuple<string, int>("59.46.0.31",1080),
                new Tuple<string, int>("211.138.61.27",80),
                new Tuple<string, int>("121.40.203.107",80),
                new Tuple<string, int>("211.138.61.27",8080),
                new Tuple<string, int>("58.221.55.58",808),
            };
            Random random = new Random();
            return ipInfos[random.Next(0, ipInfos.Length)];
        }
    }
}
