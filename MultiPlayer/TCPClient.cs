using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Cytar.Serialization;
using System.IO;

namespace MultiPlayer
{
    public class TCPClient : NetworkClient
    {
        TcpClient Client;
        byte[] buffer = null;
        int rest = 0;

        public TCPClient()
        {
            Client = new TcpClient();
        }

        public override void Connect(string host, int port)
        {
            Client.Connect(host, port);
        }

        public override byte[] GetData()
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

        public override T GetPackage<T>()
        {
            var data = GetData();
            if (data == null)
                return null;
            return CytarDeserialize.Deserialize<T>(buffer);
        }

        public override void SendData(byte[] data)
        {
            var bw = new BinaryWriter(Client.GetStream());
            bw.Write(data.Length);
            bw.Write(data);
        }

        public override void SendPackage<T>(T package)
        {
            var data = CytarSerialize.Serialize(package);

            var bw = new BinaryWriter(Client.GetStream());
            bw.Write(data.Length);
            bw.Write(data);
        }
    }
}
