using FishNet;
using FishNet.Object;
using SS3D.Core.Rounds.Messages;
using SS3D.Core.Systems.Rounds;
using TMPro;
using UnityEngine;

namespace SS3D.Core.Systems.Lobby.View
{
    /// <summary>
    /// Handles the lobby countdown view
    /// </summary>
    public class LobbyCountdownView : NetworkBehaviour
    {
        [SerializeField] private TMP_Text _roundCountdownText;

        private int _roundSeconds;
        private RoundState _roundState;

        private RoundSystem _roundSystem;

        private void Start()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            InstanceFinder.ClientManager.RegisterBroadcast<RoundStateUpdatedMessage>(HandleRoundStateUpdated);
            InstanceFinder.ClientManager.RegisterBroadcast<RoundTickUpdatedMessage>(HandleRoundTickUpdated);
        }

        private void HandleRoundTickUpdated(RoundTickUpdatedMessage m)
        {
            // probably discontinued
            _roundSeconds = m.Seconds;

            UpdateRoundCountDownText();
        }

        private void HandleRoundStateUpdated(RoundStateUpdatedMessage m)
        {
            // probably discontinued
            _roundState = m.RoundState;
            
            UpdateRoundCountDownText();
        }

        private void UpdateRoundCountDownText()
        {
            _roundSystem = GameSystems.RoundSystem;

            _roundState = _roundSystem.RoundState;
            _roundSeconds = _roundSystem.RoundTime;

            _roundCountdownText.text = $"{_roundState} - {_roundSeconds}";
        }
    }
}