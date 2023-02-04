using UnityEngine;

namespace Main
{
    public interface IRope
    {
        public Rigidbody2D Rigidbody2D { get; }
        public Transform UpPoint { get; }
        public Transform DownPoint { get; }
    }
}