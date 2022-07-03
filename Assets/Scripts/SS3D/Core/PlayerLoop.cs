using System;
using UnityEngine;

namespace SS3D.Core
{
    /// <summary>
    /// Used to avoid using "private void Update()", because Unity tried to find all objects with that method every frame
    /// in order to run them, making it based on events makes sure everything runs smoother and faster. Thank you Unity.
    /// </summary>
    public class PlayerLoop : MonoBehaviour
    {
        public static Action OnUpdate;
        public static Action OnLateUpdate;
        public static Action OnFixedUpdate;

        public static Action<bool> OnApplicationFocusChanged;
        public static Action<bool> OnApplicationPauseChanged;

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            OnApplicationFocusChanged?.Invoke(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            OnApplicationPauseChanged?.Invoke(pauseStatus);
        }
    }
}