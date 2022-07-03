namespace SS3D.Core.Systems.Chat
{
    
    // ReSharper disable InconsistentNaming
    /// <summary>
    /// Used by the chat system to identify where to send the message
    /// </summary>
    public enum ChatChannels
    {
        /// <summary>
        /// Out of character
        /// </summary>
        OOC = 0,
        /// <summary>
        /// Local out of character
        /// </summary>
        LOOC = 1,
        // Radio chats
        /// <summary>
        /// General radio chat, used in SS13 with ";"
        /// </summary>
        General = 2,
    }
}