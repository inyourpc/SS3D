﻿using FishNet.Broadcast;

namespace SS3D.Systems.Lobby.Messages
{
    public struct UserLeftLobbyMessage : IBroadcast
    {
        public readonly string Ckey;

        public UserLeftLobbyMessage(string ckey)
        {
            Ckey = ckey;
        }
    }
}