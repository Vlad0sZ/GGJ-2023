using System;
using Effects;
using UnityEngine;
using UnityEngine.Playables;

namespace Locations
{
    public class LocationController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector[] stoppedDirectors;
        [SerializeField] private PlayerTrigger[] reloadTriggers;

        [SerializeField] private PlayableDirector finishDirector;
        [SerializeField] private string nextLevel;

        private bool isFinishPlayed;

        private void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                isFinishPlayed = true;
                FinalizeLocation();
            }
            
            if(Input.GetKey(KeyCode.T)) ReloadLocation();
        }
        
        private void OnEnable()
        {
            if (finishDirector == null)
            {
                isFinishPlayed = true;
                return;
            }
            
            
            if (finishDirector.playOnAwake) isFinishPlayed = true;
            else finishDirector.played += director => { isFinishPlayed = true; };
            finishDirector.stopped += director => { FinalizeLocation(); };
        }


        public void ReloadLocation()
        {
            isFinishPlayed = false;
            Debug.Log("Reload location");
            if (stoppedDirectors.Length == 0 && reloadTriggers.Length == 0)
            {
                SceneLoader.Instance.Reload();
                return;
            }

            foreach (var playableDirector in stoppedDirectors)
            {
                Debug.Log($"stop {playableDirector}");
                playableDirector.Stop();
                playableDirector.DeferredEvaluate();
            }

            foreach (var reloadTrigger in reloadTriggers)
            {
                reloadTrigger.Reload();
            }
        }

        public void FinalizeLocation()
        {
            if (!isFinishPlayed) return;
            
            Debug.Log($"finish locations. Next is = {nextLevel}");
            SceneLoader.Instance.LoadScene(nextLevel);
        }
    }
}