using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MultiPlayer;

namespace NetworkTestGameServer
{
    public class PlayerSession
    {
        public Guid ID;
        public Guid MirrorID;
        public string Name;
        public NetworkSession NetworkSession;
        public bool Ready = false;
        public bool Online => NetworkSession.Connected;
        public GameServer GameServer;

        public PlayerSession(NetworkSession networkSession)
        {
            NetworkSession = networkSession;
            ID = Guid.NewGuid();
            MirrorID = Guid.NewGuid();
        }
        public PlayerState GetState()
        {
            var packages = new List<PlayerState>();
            PlayerState state = null;
            while ((state = NetworkSession.GetPackage<PlayerState>()) != null)
                packages.Add(state);
            if (packages.Count <= 0)
                return null;
            var latest = packages.Max(p => p.Tick);
            return packages.Where(p => p.Tick == latest).FirstOrDefault();
        }
        public void Sync(Sync syncState)
        {
            NetworkSession.SendPackage(syncState);
        }
        public bool TryHandShake()
        {
            var handshake = NetworkSession.GetPackage<HandShakeClient>();
            if (handshake == null)
                return false;
            Name = handshake.Name;
            handshake.ID = ID.ToString();
            NetworkSession.SendPackage(new HandShakeServer()
            {
                ID = ID.ToString(),
                Name = Name,
                Players = GameServer.Players.Values.Select(player => new PlayerData()
                {
                    ID = player.ID.ToString(),
                    Name = player.Name
                })
                .ToArray()
            });
            return true;
        }
    }
}
