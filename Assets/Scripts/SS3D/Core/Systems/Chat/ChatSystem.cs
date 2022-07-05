using System.Collections.Generic;
using FishNet;
using FishNet.Connection;
using FishNet.Managing.Server;
using FishNet.Object;
using SS3D.Core.Systems.Chat.Messages;
using UnityEngine;

namespace SS3D.Core.Systems.Chat
{
    /// <summary>
    /// <para>
    /// Controls the chat system, processing messages that come from clients and sending to all clients.
    /// </para>
    ///
    /// <para>
    /// How it works:
    /// <ul>
    ///     <li>1 - Player sends a chat message request</li>
    ///     <li>2 - Server receives it</li>
    ///     <li>3 - Server processes it</li>
    ///     <li>4 - Server sends to all clients</li>
    /// </ul>
    /// </para>
    /// </summary>
    public class ChatSystem : NetworkBehaviour
    {
        // For cache reasons
        private ServerManager _serverManager;

        private Dictionary<ChatChannels, List<string>> _chatMessages;

        public override void OnStartServer()
        {
            base.OnStartServer();

            ServerAddEventListeners();
        }

        [Server]
        private void ServerAddEventListeners()
        {
            _serverManager = InstanceFinder.ServerManager;

            _serverManager.RegisterBroadcast<RequestSendChatMessage>(HandleRequestChatMessage);
        }

        private void HandleRequestChatMessage(NetworkConnection conn, RequestSendChatMessage chatMessage)
        {
            SendChatMessage(chatMessage);
        }

        [Server]
        private void SendChatMessage(RequestSendChatMessage chatMessage)
        {
            ChatMessage message = chatMessage.Message;

            // TODO: Any necessary message checks

            Debug.Log($"[{nameof(ChatSystem)}] - Sending message to clients: {message.FullMessage}");
            _serverManager.Broadcast(new ChatMessageReceived(message));
        }
    }
}
