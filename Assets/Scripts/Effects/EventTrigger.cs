using UnityEngine;
using UnityEngine.Events;

namespace Effects
{
    public class EventTrigger : PlayerTrigger
    {
        [System.Serializable]
        public class PlayerTriggerEvent : UnityEvent
        {
        }

        [SerializeField] private PlayerTriggerEvent onPlayerTriggered;


        protected override void PlayerTriggered(GameObject player)
        {
            onPlayerTriggered?.Invoke();
        }
    }
}