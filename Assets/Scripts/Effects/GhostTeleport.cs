using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(Collider2D))]
    public class GhostTeleport : PlayerTrigger
    {
        [SerializeField] private Transform targetPosition;
        [SerializeField] private GhostDash ghostDash;
        [SerializeField] private float preDashWaiting = 0.1f;
        [SerializeField] private float postDashWaiting = 0.2f;

        [SerializeField] private CinemachineVirtualCamera vCamera;
        
        private Coroutine _ghostRoutine;
        private GameObject _player;
        private GhostDash _ghostDash;
        
        protected override void PlayerTriggered(GameObject player)
        {
            if (targetPosition == null) return;

            _player = player;
            _ghostDash = Instantiate(ghostDash, transform);
            _ghostDash.CreateDash(player.transform.position, targetPosition.position, DashesRoutines, true);
        }


        
        private IEnumerator DashesRoutines(GhostDash.DashState state)
        {
            switch (state)
            {
                case GhostDash.DashState.Prepare:
                    _player.gameObject.SetActive(false);
                    break;
                
                case GhostDash.DashState.PreDash:
                    yield return null;
                    yield return new WaitForSeconds(preDashWaiting);
                    break;

                case GhostDash.DashState.Dash:
                    yield return null;
                    
                    if (vCamera)
                    {
                        vCamera.gameObject.SetActive(true);
                        vCamera.Follow = targetPosition;
                    }

                    yield return new WaitForSeconds(postDashWaiting);
                    
                    break;
                
                
                case GhostDash.DashState.PostDash:
                    yield return null;
                    
                    _player.transform.position = targetPosition.position;
                    if (vCamera)
                        vCamera.gameObject.SetActive(false);
                    _player.SetActive(true);
                    yield return null;

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnDrawGizmos()
        {
            if (targetPosition == null) return;

            var pos = transform.position;
            var t = targetPosition.position;
            Gizmos.DrawLine(pos, t);
        }
    }
}