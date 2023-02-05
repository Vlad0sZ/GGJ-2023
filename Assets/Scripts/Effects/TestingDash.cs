using System;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public class TestingDash : MonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private Transform player;
        [SerializeField] private GhostDash dashEffect;
        
        [SerializeField] private bool dash;
        
        private void Update()
        {
            if (dash) Dash();
        }

        private void Dash()
        {
            dash = false;
            var d = Instantiate(dashEffect, Vector3.zero, Quaternion.identity);
            d.CreateDash(start.position, end.position, DashesRoutines, true);
        }

        private IEnumerator DashesRoutines(GhostDash.DashState state)
        {
            switch (state)
            {
                case GhostDash.DashState.PreDash:
                    player.gameObject.SetActive(false);
                    yield return null;
                    break;
                case GhostDash.DashState.Dash:
                    player.position = end.position;
                    yield return null;
                    break;
                case GhostDash.DashState.PostDash:
                    player.gameObject.SetActive(true);
                    yield return null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}