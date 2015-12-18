using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEnd = new IPEndPoint(ip, 5566);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipEnd);
            }
            catch (SocketException e)
            {
                Console.Write("Fail to connect server");
                Console.Write(e.ToString());
                return;
            }
            int recv = socket.Receive(data);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.Write(stringData);
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                socket.Send(Encoding.ASCII.GetBytes(input));
                data = new byte[1024];

                recv = socket.Receive(data);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.Write(stringData);
            }
            Console.Write("disconnect from server");
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
