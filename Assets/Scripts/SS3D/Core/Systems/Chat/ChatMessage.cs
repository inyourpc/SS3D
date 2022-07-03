using FishNet.Connection;

namespace SS3D.Core.Systems.Chat
{
    public struct ChatMessage
    {
        public readonly ChatChannels Channel;
        public readonly NetworkConnection Author;
        public readonly string Message;

        public ChatMessage(ChatChannels channel, NetworkConnection author, string message)
        {
            Channel = channel;
            Author = author;
            Message = message;
        }
    }
}