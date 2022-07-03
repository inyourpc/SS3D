using FishNet.Broadcast;

namespace SS3D.Core.Systems.Chat.Messages
{
    public struct ChatMessageReceived : IBroadcast
    {
        public readonly ChatMessage Message;

        public ChatMessageReceived(ChatMessage message)
        {
            Message = message;
        }
    }
}