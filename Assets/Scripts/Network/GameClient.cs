using UnityEngine;
using System.Collections;

namespace MultiPlayer
{
    public class GameClient : MonoBehaviour
    {
        GameSession Session;
        TCPClient Client;
        public string Host;
        public int Port;
        public string Name;
        public string ID;
        // Use this for initialization
        void Start()
        {
            StartCoroutine(ConnectCoroutine());
        }

        IEnumerator ConnectCoroutine()
        {
            Client = new TCPClient();
            Client.Connect(Host, Port);
            Session = new GameSession(Client);
            Session.SendPackage(new HandShake() { Name = Name, ID = "" });
            yield return Session.WaitPackage();
            var handshake = Session.GetPackage<HandShake>();
            ID = handshake.ID;
        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}