using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Cytar.Serialization;

namespace NetworkTestGameServer
{
    public class TCPSession:NetworkSession
    {
        bool connected = true;
        public override bool Connected => connected;
        TcpClient Client;
        byte[] buffer = null;
        int rest = 0;

        public TCPSession(TcpClient client)
        {
            this.Client = client;
        }

        public override T GetPackage<T>()
        {
            var data = ReceiveData();
            if (data != null)
                return CytarDeserialize.Deserialize<T>(data);
            return null;
        }

        public override void SendPackage<T>(T package)
        {
            SendData(CytarSerialize.Serialize(package));
        }

        public override void SendData(byte[] data)
        {
            try
            {
                var buffer = new byte[data.Length + 4];
                using (var ms = new MemoryStream(buffer))
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(data.Length);
                    bw.Write(data);
                }
                Client.GetStream().Write(buffer, 0, buffer.Length);
                Client.GetStream().Flush();

            }
            catch (Exception ex)
            {
                connected = false;
            }
        }

        public override byte[] ReceiveData()
        {
            try
            {
                // Get header
                if (rest == 0)
                {
                    if (Client.Available < 4)
                        return null;
                    rest = new BinaryReader(Client.GetStream()).ReadInt32();
                    buffer = new byte[rest];
                }

                rest -= Client.GetStream().Read(buffer, buffer.Length - rest, Math.Min(rest, Client.Available));
                if (rest > 0)
                    return null;
                return buffer;
            }
            catch (Exception ex)
            {
                connected = false;
            }
            return null;
        }
    }
}
