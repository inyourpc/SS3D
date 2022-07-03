using FishNet.Connection;

namespace SS3D.Core.Systems.Chat
{
    public struct ChatMessage
    {
        public readonly ChatChannels Channel;
        public readonly NetworkConnection Sender;
        public readonly string Author;
        public readonly string Content;

        public string FullMessage => ToString();

        public ChatMessage(ChatChannels channel, NetworkConnection sender, string author, string content)
        {
            Channel = channel;
            Sender = sender;
            Author = author;
            Content = content;
        }

        public override string ToString()
        {
            return $"[<b>{Author}</b>] | {Content}";
        }
    }
}