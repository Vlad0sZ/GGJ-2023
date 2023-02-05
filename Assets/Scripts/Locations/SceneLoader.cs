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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (audioSource == null || loadingClip == null) return;
            
            if(audioSource.isPlaying) return;
            
            audioSource.clip = loadingClip;
            audioSource.Play();
        }


        public void LoadScene(string byName) => SceneManager.LoadScene(byName);

        public void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}