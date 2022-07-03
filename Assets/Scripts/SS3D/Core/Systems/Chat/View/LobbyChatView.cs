using FishNet;
using FishNet.Managing.Client;
using FishNet.Object;
using SS3D.Core.Systems.Chat.Messages;
using SS3D.Core.Systems.Entities;
using TMPro;
using UnityEngine;

namespace SS3D.Core.Systems.Chat.View
{
    /// <summary>
    /// Controls the message flux on the lobby screen
    /// </summary>
    public class LobbyChatView : NetworkBehaviour
    {
        [SerializeField] private ChatChannels _messageChannel;
        [SerializeField] private TMP_InputField _messageInputField;

        [SerializeField] private GenericChatMessageView _messageUiPrefab;
        [SerializeField] private Transform _messagesUiHolder;

        private ClientManager _clientManager;
        private Soul _soul;

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
            _clientManager.RegisterBroadcast<ChatMessageReceived>(HandleChatMessageReceived);
            _messageInputField.onSubmit.AddListener(ProcessChatInput);
        }

        private void HandleChatMessageReceived(ChatMessageReceived chatMessage)
        {
            Debug.Log($"[{nameof(LobbyChatView)}] - Received chat message {chatMessage.Message.FullMessage}");

            AddChatMessage(chatMessage);
        }

        private void AddChatMessage(ChatMessageReceived chatMessage)
        {
            ChatMessage message = chatMessage.Message;

            if (message.Channel != _messageChannel)
            {
                return;
            }

            GenericChatMessageView chatMessageView = Instantiate(_messageUiPrefab, _messagesUiHolder);

            chatMessageView.SetMessage(message.FullMessage);
        }

        private void ProcessChatInput(string message)
        {
            _messageInputField.text = string.Empty;
            SendRequestMessage(message);
        }

        /// <summary>
        /// Sends the message to the server, the server will then decide what to do with it 
        /// </summary>
        private void SendRequestMessage(string message)
        {
            _soul = LocalConnection.FirstObject.GetComponent<Soul>();

            ChatMessage chatMessage = new(_messageChannel, LocalConnection, _soul.Ckey, message);

            _clientManager.Broadcast(new RequestSendChatMessage(chatMessage));
        }
    }
}