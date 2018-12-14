using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public GameObject PlayerPrefab;
        public GameObject NetworkPlayerPrefab;
        public float SyncDuration = 0.1f;

        Player Player;
        List<NetworkPlayer> RemotePlayers = new List<NetworkPlayer>();

        // Use this for initialization
        void Start()
        {
            StartCoroutine(NetworkCoroutine());
            
        }

        IEnumerator Sync()
        {
            while (true)
            {
                var state = Player.GetComponent<InputController>().PlayerState;
                if (state!=null)
                {
                    state.ID = ID;
                    Session.SendPackage(state);
                }
                yield return new WaitForSeconds(SyncDuration);
            }
        }

        IEnumerator NetworkCoroutine()
        {
            Client = new TCPClient();
            Client.Connect(Host, Port);
            Session = new GameSession(Client);
            Session.SendPackage(new HandShakeClient() { Name = Name, ID = "" });
            yield return Session.WaitPackage();
            var handshake = Session.GetPackage<HandShakeServer>();
            ID = handshake.ID;
            Player = Instantiate(PlayerPrefab).GetComponent<Player>();
            StartCoroutine(Sync());
            while (true)
            {
                yield return Session.WaitPackage(); 
                var sync = Session.GetPackage<Sync>();
                RemotePlayers.ForEach(p => p.GetComponent<NetworkController>().PlayerState = null);
                sync.syncStates
                    .Where(state => state.ID != ID)
                    .ForEach(state =>
                    {
                        var player = RemotePlayers
                        .Where(p => p.ID == state.ID)
                        .FirstOrDefault();
                        if (!player)
                        {
                            player = Instantiate(NetworkPlayerPrefab).GetComponent<NetworkPlayer>();
                        }
                        player.GetComponent<NetworkController>().PlayerState = state;
                        player.ID = state.ID;
                        player.name = state.ID;
                        RemotePlayers.Add(player);
                    });
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}