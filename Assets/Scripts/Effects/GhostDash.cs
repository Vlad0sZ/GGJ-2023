using System;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public class GhostDash : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dashEffect;
        [SerializeField] private ParticleSystem preDashEffect;
        [SerializeField] private ParticleSystem postDashEffect;

        public enum DashState
        {
            Prepare,
            PreDash,
            Dash,
            PostDash
        }

        private Coroutine _dashRoutine;
        
        public Vector3 DashPosition { get; private set; }
        
        public Quaternion DashRotation { get; private set; }


        public void CreateDash(Vector3 startPos, Vector3 endPos,
            Func<DashState, IEnumerator> coroutines, bool destroyAfter)
        {
            if (_dashRoutine != null) StopCoroutine(_dashRoutine);
            _dashRoutine = StartCoroutine(Dash(startPos, endPos, coroutines, destroyAfter));
        }

        private IEnumerator Dash(Vector3 startPos, Vector3 endPos,
            Func<DashState, IEnumerator> coroutines, bool destroyAfter)
        {
            var rot = Quaternion.LookRotation(endPos - startPos, Vector3.up);
            var pos = Vector3.Lerp(startPos, endPos, .5f);
            DashPosition = pos;
            DashRotation = rot;

            yield return coroutines(DashState.Prepare); 
            Instantiate(preDashEffect, startPos, Quaternion.identity);

            yield return coroutines(DashState.PreDash);
            Instantiate(dashEffect, pos, rot);
            yield return coroutines(DashState.Dash);

            Instantiate(postDashEffect, endPos, Quaternion.identity);
            yield return coroutines(DashState.PostDash);

            if (destroyAfter)
                Destroy(gameObject);
        }
    }
}