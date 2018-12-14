using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using MultiPlayer;

namespace NetworkTestGameServer
{
    public class GameServer
    {
        public const int SyncDuration = 100;
        public const int Frame = 60;
        public double Time = 0;
        public int Tick = 0;
        public Dictionary<Guid, PlayerSession> Players;
        public List<PlayerSession> HandShakeList = new List<PlayerSession>();
        Thread GameThread;
        NetworkServer Server;
        private int syncCount = 0;
        
        public GameServer(NetworkServer server)
        {
            Players = new Dictionary<Guid, PlayerSession>();
            Server = server;
        }
        public void Start()
        {
            GameThread = new Thread(GameLoop);
            GameThread.Start();
            Server.Start();
            while (true)
            {
                var session = Server.Listen();
                var player = new PlayerSession(session);
                lock (HandShakeList)
                {
                    HandShakeList.Add(player);
                }
                player.GameServer = this;
                ServerLog.Log($"Player {player.ID} joined.");
                /*
                lock (Players)
                {
                    Players.Add(player.ID, player);
                    ServerLog.Log($"Player {player.ID} joined.");
                }*/
            }

        }
        void GameLoop()
        {
            while (true)
            {
                if (Time / SyncDuration * 1000 >= syncCount)
                {
                    syncCount++;
                    lock (Players)
                    {
                        var sync = new Sync()
                        {
                            Tick = Tick,
                            syncStates = Players.Values
                        .Select(player => player.GetState())
                        .Where(state => state != null)
                        .SelectMany(state => new PlayerState[]
                            {
                                state,
                                new PlayerState()
                                {
                                    ID=Players[Guid.Parse(state.ID)].MirrorID.ToString(),
                                    Aim=-state.Aim,
                                    Fire = state.Fire,
                                    Movement=-state.Movement,
                                    Position=-state.Position,
                                    Tick = Tick
                                }
                            })
                        .ToArray()
                        };
                        foreach (var player in Players.Values)
                        {
                            player.Sync(sync);
                        }
                    }
                }

                // Remove disconnected players
                lock (Players)
                {
                    var removeList = new List<Guid>();
                    foreach(var pair in Players)
                    {
                        if (!pair.Value.Online)
                        {
                            removeList.Add(pair.Key);
                            ServerLog.Log($"Player \"{pair.Value.Name}\" disconnected. ");
                        }
                    }
                    removeList.ForEach(key => Players.Remove(key));
                }

                // Handle handshake
                lock (HandShakeList)
                {
                    for(var i = 0; i < HandShakeList.Count; i++)
                    {
                        var player = HandShakeList[i];
                        if (player.TryHandShake())
                        {
                            lock (Players)
                            {
                                Players.Add(player.ID, player);
                            }
                            HandShakeList.RemoveAt(i--);
                            ServerLog.Log($"Player \"{player.Name}\" joined.");
                        }
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(1.0 / Frame));
                Time += 1.0 / Frame;
                Tick += 1;
            }
        }
    }
}
