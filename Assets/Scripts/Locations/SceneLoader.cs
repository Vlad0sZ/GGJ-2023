using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Locations
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip loadingClip;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        public void LoadScene(string byName) => SceneManager.LoadScene(byName);

        public void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}