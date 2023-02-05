using System;
using System.Collections;
using System.Collections.Generic;
using Locations;
using Main;
using UnityEngine;

namespace Animals
{
    public class BearController : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;
        [SerializeField] private List<TriggerObject> arms;
        [SerializeField] private TriggerObject finishObject;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private PlayerController playerController;

        private void Awake()
        {
            foreach (var arm in arms)
            {
                arm.PlayerTriggered += PlayerTriggered;
            }
            
            finishObject.PlayerTriggered += StartFinishAnimation;
        }

        private void StartFinishAnimation()
        {
            playerController.Disable();
            playerAnimator.SetBool("AddLegs", true);
            StartCoroutine(LoadNewScene());
        }

        private void OnDestroy()
        {
            foreach (var arm in arms)
            {
                arm.PlayerTriggered -= PlayerTriggered;
            }
            
        }

        private void PlayerTriggered()
        {
            SceneLoader.Instance.Reload();
        }

        private IEnumerator LoadNewScene()
        {
            yield return new WaitForSeconds(5);
            SceneLoader.Instance.LoadScene(nextSceneName);

        }
    }
}