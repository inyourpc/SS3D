using System;
using Cysharp.Threading.Tasks;
using FishNet;
using FishNet.Managing.Client;
using FishNet.Object;
using SS3D.Core.Systems.Chat.Messages;
using TMPro;
using UnityEngine;

namespace SS3D.Core.Systems.Chat.View
{
    public class LobbyChatView : NetworkBehaviour
    {
        [SerializeField] private ChatChannels _messageChannel;
        [SerializeField] private TMP_InputField _messageInputField;

        private ClientManager _clientManager;

        private void Start()
        {
            Setup();
            AddEventListeners();
        }

        private void Setup()
        {
            _clientManager = InstanceFinder.ClientManager;
        }

        private void AddEventListeners()
        {
            PlayerLoop.OnUpdate += HandleOnUpdate;
        }

        private void HandleOnUpdate()
        {
            ProcessChatInput();
        }

        private void ProcessChatInput()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SendRequestMessage();
            }
        }

        /// <summary>
        /// Sends the message to the server, the server will then decide what to do with it 
        /// </summary>
        private void SendRequestMessage()
        {
            string message = _messageInputField.text; 

            ChatMessage chatMessage = new(_messageChannel, LocalConnection, message);

            _clientManager.Broadcast(new RequestSendChatMessage(chatMessage));
        }
    }
}