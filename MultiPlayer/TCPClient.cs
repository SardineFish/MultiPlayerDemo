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
        int headerRest = 0;

        public TCPClient()
        {
            Client = new TcpClient();
            buffer = new byte[4];
            headerRest = 4;
        }

        public override void Connect(string host, int port)
        {
            Client.Connect(host, port);
            //Socket.Connect(host, port);
            //Socket.Blocking = false;
        }

        public override byte[] GetData()
        {
            // Get header
            if (headerRest>0)
            {
                headerRest -= Client.GetStream().Read(buffer, buffer.Length - headerRest, Math.Min(Client.Available, headerRest));
                //headerRest -= Socket.Receive(buffer, buffer.Length - headerRest, headerRest, SocketFlags.None);
                if (headerRest > 0)
                    return null;
                using (var ms = new MemoryStream(buffer))
                using (var br = new BinaryReader(ms))
                {
                    rest = br.ReadInt32();
                }
                buffer = new byte[rest];
            }

            
            rest -= Client.GetStream().Read(buffer, buffer.Length - rest, Math.Min(Client.Available, rest));
            if (rest > 0)
                return null;
            var bufferTmp = buffer;
            headerRest = 4;
            buffer = new byte[4];
            return bufferTmp;
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
            var buffer = new byte[data.Length + 4];
            using (var ms = new MemoryStream(buffer))
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(data.Length);
                bw.Write(data);
            }
            Client.GetStream().Write(buffer, 0, buffer.Length);
            
        }

        public override void SendPackage<T>(T package)
        {
            var data = CytarSerialize.Serialize(package);
            SendData(data);
        }
    }
}
