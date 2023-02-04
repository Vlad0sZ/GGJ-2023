using System;
using UnityEngine;

namespace Main
{
    public class Rope : MonoBehaviour, IRope
    {
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private Transform upPoint;
        [SerializeField] private Transform downPoint;
        
        public Rigidbody2D Rigidbody2D => rb2D;
        public Transform UpPoint => upPoint;
        public Transform DownPoint => downPoint;
    }
}