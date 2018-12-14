using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Cytar.Serialization;

namespace MultiPlayer
{
    public class GameSession
    {
        public NetworkClient Client;
        Queue<byte[]> Packages = new Queue<byte[]>();
        public GameSession(NetworkClient client)
        {
            Client = client;
        }
        public IEnumerator WaitPackage()
        {
            ReceiveAll();
            while (Packages.Count <= 0)
            {
                ReceiveAll();
                yield return null;
            }
        }
        public T GetPackage<T>() where T: class
        {
            ReceiveAll();
            if (Packages.Count<=0)
            {
                return null;
            }
            var data = Packages.Dequeue();
            return CytarDeserialize.Deserialize<T>(data);
        }
        void ReceiveAll()
        {
            for(var data = Client.GetData(); data != null; data = Client.GetData())
            {
                Packages.Enqueue(data);
            }
        }
        public void SendPackage<T>(T package) where T : class
        {
            Client.SendPackage(package);
        }
    }

}