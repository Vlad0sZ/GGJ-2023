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

        private void OnEnable()
        {
            if (finishDirector == null) return;
            finishDirector.played += director => { isFinishPlayed = true; };
            finishDirector.stopped += director => { FinalizeLocation(); };
        }


        public void ReloadLocation()
        {
            isFinishPlayed = false;

            if (stoppedDirectors.Length == 0 && reloadTriggers.Length == 0)
            {
                SceneLoader.Instance.Reload();
                return;
            }

            foreach (var playableDirector in stoppedDirectors)
            {
                playableDirector.Stop();
                playableDirector.time = 0f;
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

            Debug.Log("finish locations");
            SceneLoader.Instance.LoadScene(nextLevel);
        }
    }
}