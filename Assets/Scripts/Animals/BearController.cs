using System;
using System.Collections.Generic;
using Locations;
using Main;
using UnityEngine;

namespace Animals
{
    public class BearController : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Player player;
        [SerializeField] private List<TriggerObject> arms;
        [SerializeField] private TriggerObject endLevelObject;
        

        private void Awake()
        {
            foreach (var arm in arms)
            {
                arm.PlayerTriggered += PlayerTriggered;
            }
            
            endLevelObject.PlayerTriggered += FinishLevel;
        }

        private void OnDestroy()
        {
            foreach (var arm in arms)
            {
                arm.PlayerTriggered -= PlayerTriggered;
            }

            endLevelObject.PlayerTriggered -= FinishLevel;
        }

        private void FinishLevel()
        {
            SceneLoader.Instance.LoadScene(nextSceneName);
        }

        private void PlayerTriggered()
        {
            SceneLoader.Instance.Reload();
        }
    }
}