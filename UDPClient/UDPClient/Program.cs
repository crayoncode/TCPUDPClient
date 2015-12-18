using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
          SendBroadcast("hello");
           Console.ReadKey();
        }

        public static string SendBroadcast(string msgStr)
        {
            try
            {
                //New一个Socket配置连接方式
                using (Socket socks = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    int recv;
                    byte[] recvBytes = new byte[1024];
                    string ipValue = "127.0.0.1";
                    //获取广播需要的端口
                    string port = "1514";
                    //创建连接
                    IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ipValue), int.Parse(port));
                    EndPoint remote = (EndPoint)(iep);
                    //将字符转化为可传输的格式
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(msgStr);
                    socks.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                    socks.SendTo(data, iep);
                    recv = socks.ReceiveFrom(recvBytes, ref remote);
                    string str = System.Text.Encoding.ASCII.GetString(recvBytes, 0, recv);
                    Console.WriteLine("444:{0}", str);
                    socks.Dispose();
                    return "CMD Send Succeed!";
                }               
            }
            catch { return "CMD Send Fail!"; }
        }
    }
}
