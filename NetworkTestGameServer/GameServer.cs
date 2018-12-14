using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace NetworkTestGameServer
{
    public class GameServer
    {
        public const int SyncDuration = 100;
        public const int Frame = 60;
        public double Time = 0;
        public int Tick = 0;
        public Dictionary<Guid, PlayerSession> Players;
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
                lock (Players)
                {
                    Players.Add(player.ID, player);
                    ServerLog.Log($"Player {player.ID} joined.");
                }
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
                        .ToArray()
                        };
                        foreach (var player in Players.Values)
                        {
                            player.Sync(sync);
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
