using FishNet.Broadcast;

namespace SS3D.Core.Systems.Chat.Messages
{
    public struct SendChatMessage : IBroadcast
    {
        public readonly ChatMessage Message;

        public SendChatMessage(ChatMessage message)
        {
            Message = message;
        }
    }
}