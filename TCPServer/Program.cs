using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Afgift.Afgift;

namespace TCPServer
{
    class Program
    {
        private static Afgift.Afgift afgift = new Afgift.Afgift();
         private static TcpListener serverSocket = new TcpListener(IPAddress.Any, 6789);




        static void Main(string[] args)
        {

            Program.serverSocket.Start();

            Console.WriteLine("Server strated");

            AccepterKlient();

        }

        private static void AccepterKlient()
        {
            while (true)
            {
                TcpClient newClient = serverSocket.AcceptTcpClient();
                Thread t = new Thread(new ParameterizedThreadStart(Nyklient));
                t.Start(newClient);
            }
        }

        private static void Nyklient(object obj)
        {
            TcpClient client = (TcpClient) obj;
            Console.WriteLine("Server activated by client");
            Stream ns = client.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw =new StreamWriter(ns);
            {
                sw.AutoFlush = true;
            }
            while (true)
            {
                sw.WriteLine("Person bil eller Elbil");
                string msg = sr.ReadLine();
                if (msg.ToLower().Contains("person"))
                {
                    sw.WriteLine("prisen på personbil?");
                    string pris = sr.ReadLine();
                    sw.WriteLine("Afgift:" + afgift.BilAfgift(Convert.ToInt32(pris)));
                    sr.ReadLine();
                }

                else if (msg.ToLower().Contains("el"))
                {
                    sw.WriteLine("Prisen på elbien");
                    string pris = sr.ReadLine();

                    sw.WriteLine("Afgift" + afgift.ElBilAfgift(Convert.ToInt32(pris)));
                    sr.ReadLine();
                }
            }



    
        }
    }
}
