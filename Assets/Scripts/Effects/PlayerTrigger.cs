using System;
using UnityEngine;
using UnityEngine.Events;

namespace Effects
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class PlayerTrigger : MonoBehaviour
    {
        [SerializeField] private float cooldown = 2f;

        private Collider2D _collider;
        private float _lastTime;

        private void Awake()
        {
            _lastTime = Time.time;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log($"triggered {col}");
            if (!col.CompareTag("Player")) return;
            if (Mathf.Abs(Time.time - _lastTime) <= cooldown) return;
            Debug.Log($"Is Player!");
            _lastTime = Time.time;
            PlayerTriggered(col.gameObject);
        }

        private void OnValidate()
        {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();

            if (_collider && !_collider.isTrigger)
                _collider.isTrigger = true;
        }

        public void Reload() => _lastTime = Time.time;

        
        protected abstract void PlayerTriggered(GameObject player);

    }
}