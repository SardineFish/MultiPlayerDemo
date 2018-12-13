using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetworkTestGameServer
{
    public class PlayerSession
    {
        public Guid ID;
        public NetworkSession NetworkSession;
        public PlayerSession(NetworkSession networkSession)
        {
            NetworkSession = networkSession;
            ID = Guid.NewGuid();
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

        }
    }
}
