using System;
using Coimbra;
using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using SS3D.Core.Networking.PlayerControl.Messages;
using UnityEngine;

namespace SS3D.Core.Networking.Lobby
{
    /// <summary>
    /// Manages all networked lobby stuff
    /// </summary>
    public sealed class LobbySystem : NetworkBehaviour
    {
        // Current lobby players
        [SyncObject]
        private readonly SyncList<string> _players = new();

        [Serializable]
        public struct UserJoinedLobby
        {
            public string Ckey;

            public UserJoinedLobby(string ckey)
            {
                Ckey = ckey;
            }
        }

        [Serializable]
        public struct UserLeftLobby
        {
            public string Ckey;

            public UserLeftLobby(string ckey)
            {
                Ckey = ckey;
            }
        }

        private void Start()
        {
            SyncLobbyPlayers();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            InstanceFinder.ClientManager.RegisterBroadcast<UserJoinedServerMessage>(AddLobbyPlayer);
            InstanceFinder.ClientManager.RegisterBroadcast<UserLeftServerMessage>(RemoveLobbyPlayer);
        }

        /// <summary>
        /// Updates the lobby players on Start
        /// </summary>
        private void SyncLobbyPlayers()
        {
            foreach (string player in _players)
            {
                IEventService eventService = ServiceLocator.Shared.Get<IEventService>();
                eventService?.Invoke(null, new UserJoinedLobby(player));
            }                                                                                   
        }

        [Server]
        private void AddLobbyPlayer(UserJoinedServerMessage userJoinedServerMessage)
        {
              _players.Add(userJoinedServerMessage.Ckey);

              RpcAddLobbyPlayer(new UserJoinedLobby(userJoinedServerMessage.Ckey));
              Debug.Log($"[{typeof(LobbySystem)}] - SERVER - Added player to lobby: {userJoinedServerMessage.Ckey}");
        }

        [ObserversRpc]
        private void RpcAddLobbyPlayer(UserJoinedLobby userJoinedLobby)
        {
            IEventService eventService = ServiceLocator.Shared.Get<IEventService>();
            eventService?.Invoke(null, userJoinedLobby);
            Debug.Log($"[{typeof(LobbySystem)}] - RPC - Added player to lobby: {userJoinedLobby.Ckey}");
        }

        [Server]
        private void RemoveLobbyPlayer(UserLeftServerMessage userLeftServerMessage)
        {
            _players.Remove(userLeftServerMessage.Ckey);

            RpcRemoveLobbyPlayer(new UserLeftLobby(userLeftServerMessage.Ckey));
            Debug.Log($"[{typeof(LobbySystem)}] - SERVER - Removed player from lobby: {userLeftServerMessage.Ckey}");
        }

        [ObserversRpc]
        private void RpcRemoveLobbyPlayer(UserLeftLobby userLeftLobby)
        {
            IEventService eventService = ServiceLocator.Shared.Get<IEventService>();
            eventService?.Invoke(null, userLeftLobby);

            Debug.Log($"[{typeof(LobbySystem)}] - RPC - Removed player from lobby: {userLeftLobby.Ckey}");
        }
    }
}