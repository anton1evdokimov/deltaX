using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;

namespace tcp
{
    class tcp_client
    {
        System.Net.IPEndPoint groupEP = null;
        public TcpClient tcp_q; NetworkStream stream_q;
        public int tcp_start(string SET_IP, int port, bool vniim = false)
        {
            try
            {
                groupEP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(SET_IP), port);
                tcp_q = new TcpClient();
                tcp_q.Client.ReceiveTimeout = 1033; tcp_q.Client.SendTimeout = 33;// tcp_q.Connect(groupEP);
                IAsyncResult result = tcp_q.BeginConnect(System.Net.IPAddress.Parse(SET_IP), port, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(3000, true);
                if (!success)
                {
                    // NOTE, MUST CLOSE THE SOCKET
                    tcp_q.Close();
                    if (!vniim)
                    {
                        throw new Exception("Не удается установить связь с ПТК КМ-Дельта-М");
                    }
                }
                return 3;
            }
            catch (SocketException er)
            {
                if (!vniim)
                    MessageBox.Show("Ошибка при соединении! Проверьте связь и IP-адрес КМ-Дельта-М",
                "Ошибка при соединении!");
                return -3;
            }
            catch { if (!vniim) MessageBox.Show("Ошибка при соединении!"); return -3; }
        }
        public void tcp_stop()
        {
            try { tcp_q.Close(); }
            catch (Exception er) { MessageBox.Show(er.ToString()); }
        }
        public bool tcp_status()
        {
            try
            {
                return tcp_q.Connected;
            }
            catch { return false; }
        }
        public int tcp_send(byte[] data, int Length, string SET_IP, int port)
        {
            try
            {
                if (tcp_q.Connected)
                {
                    stream_q = new NetworkStream(tcp_q.Client); //tcp_q.Client.SendTo(data, groupEP);tcp_q.Client.Send(data);.GetStream();
                    stream_q.Write(data, 0, Length); return 0;
                }
                return 1;
            }
            catch (Exception ee)
            {
                return 33;
            }
        }
        public byte[] recive(ref int bytesRead)
        {
            try
            {
                //  System.Net.IPEndPoint groupEP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(SET_IP), dport);         tcp.Client.ReceiveTimeout=timeout;   byte [] rez=  tcp.Receive(ref groupEP);  return rez;
                stream_q = tcp_q.GetStream();
                byte[] bytes = new byte[tcp_q.ReceiveBufferSize];
                bytesRead = stream_q.Read(bytes, 0, tcp_q.ReceiveBufferSize);
                return bytes;
            }
            catch (System.Threading.ThreadAbortException e)     // исключение, вызываемое при завершении потока
            {
                return null;//MessageBox.Show(e.ToString()); 
            }
            catch (System.Net.Sockets.SocketException e)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        public byte[] recive()
        {
            try
            {
                stream_q = tcp_q.GetStream();
                byte[] bytes = new byte[tcp_q.ReceiveBufferSize];
                int bytesRead = stream_q.Read(bytes, 0, tcp_q.ReceiveBufferSize);
                return bytes;
            }
            catch (System.Threading.ThreadAbortException e)     // исключение, вызываемое при завершении потока
            {
                return null;//MessageBox.Show(e.ToString()); 
            }
            catch (SocketException e)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
