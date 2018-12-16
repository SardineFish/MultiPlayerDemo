using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;
using System.Diagnostics;
using System.IO;

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

        long lastReceive;

        // Use this for initialization
        void Start()
        {
            /*Client = new TCPClient();
            Client.Connect("server.sardinefish.com", 5325);*/
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
                var syncPackages = Session.GetPackages<Sync>();
                syncPackages.ForEach(sync =>
                {
                    //RemotePlayers.ForEach(p => p.GetComponent<NetworkController>().PlayerState = null);
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
                                player.ID = state.ID;
                                player.name = state.ID;
                                RemotePlayers.Add(player);
                            }
                            state.Tick = sync.Tick;

                            player.GetComponent<NetworkController>().PlayerState = new PlayerControlState()
                            {
                                Aim = new Vector2(state.Aim.x, state.Aim.y),
                                Movement = new Vector2(state.Movement.x, state.Movement.y),
                                Position = new Vector2(state.Position.x, state.Position.y),
                                Fire = state.Fire
                            };
                            player.GetComponent<NetworkController>().Tick = sync.Tick;
                            //Debug.Log($"Sync {sync.Tick}");
                        });
                });

                
            }
        }


        // Update is called once per frame
        void Update()
        {
            /*
            var frameWatcher = Stopwatch.StartNew();
            while (true)
            {
                var watcher = Stopwatch.StartNew();
                var data = Client.GetData();
                watcher.Stop();
                UnityEngine.Debug.Log($"Receive {watcher.Elapsed.TotalMilliseconds}ms");
                if(data == null)
                    break;

                var timestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;

                using (var ms = new MemoryStream(data))
                using (var br = new BinaryReader(ms))
                {
                    var serverTime = br.ReadInt64();
                    UnityEngine.Debug.Log($"Receive at {timestamp}, server-time: {serverTime}, dt: {timestamp - serverTime}, size: {data.Length}, duration: {timestamp - lastReceive}, using {watcher.Elapsed.TotalMilliseconds}ms.");
                    lastReceive = timestamp;
                }
            }
            frameWatcher.Stop();
            UnityEngine.Debug.Log($"Frame {frameWatcher.Elapsed.TotalMilliseconds}ms");*/
        }
    }

}