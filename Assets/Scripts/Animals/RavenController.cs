using System;
using Locations;
using UnityEngine;

namespace Animals
{
    public class RavenController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            SceneLoader.Instance.LoadScene("Intro");
        }
    }
}