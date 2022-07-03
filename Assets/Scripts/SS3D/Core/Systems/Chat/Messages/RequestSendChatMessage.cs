using FishNet.Broadcast;

namespace SS3D.Core.Systems.Chat.Messages
{
    public struct RequestSendChatMessage : IBroadcast
    {
        public readonly ChatMessage Message;

        public RequestSendChatMessage(ChatMessage message)
        {
            Message = message;
        }
    }
}