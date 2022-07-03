using TMPro;
using UnityEngine;

namespace SS3D.Core.Systems.Chat.View
{
    public class GenericChatMessageView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _messageContent;

        public void SetMessage(string message)
        {
            _messageContent.text = message;
        }
    }
}