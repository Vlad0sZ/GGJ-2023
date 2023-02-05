using System;
using UnityEngine;

namespace Main
{
    public class TriggerObject : MonoBehaviour
    {
        public Action PlayerTriggered;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("Player"))
                PlayerTriggered?.Invoke();
        }
    }
}