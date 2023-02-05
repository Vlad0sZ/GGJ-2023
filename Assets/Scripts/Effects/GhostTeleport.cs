using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(Collider2D))]
    public class GhostTeleport : PlayerTrigger
    {
        [SerializeField] private Transform targetPosition;
        [SerializeField] private ParticleSystem ghostParticles;
        [SerializeField] private float speed;

        [SerializeField] private CinemachineVirtualCamera vCamera;
        
        private Coroutine _ghostRoutine;
        private GameObject _player;
        private ParticleSystem _ghostParticle;

        protected override void PlayerTriggered(GameObject player)
        {
            if (targetPosition == null) return;

            _player = player;
            if (_ghostRoutine != null) StopCoroutine(_ghostRoutine);
            _ghostRoutine = StartCoroutine(TeleportPlayerToTarget());
        }


        private IEnumerator TeleportPlayerToTarget()
        {
            if (_player == null) yield break;

            _player.SetActive(false);

            if (_ghostParticle == null)
                _ghostParticle = Instantiate(ghostParticles, transform);

            _ghostParticle.transform.position = _player.transform.position;
            _ghostParticle.gameObject.SetActive(true);
            _ghostParticle.Play(true);
            if (vCamera)
            {
                vCamera.gameObject.SetActive(true);
                vCamera.Follow = _ghostParticle.transform;
            }

            yield return null;

            Transform ghostTransform = _ghostParticle.transform;
            Vector3 target = targetPosition.position;
            Vector3 position = ghostTransform.position;
            float t = 0f;

            while (t <= 1f)
            {
                t += Time.deltaTime * speed;
                ghostTransform.position = Vector3.Slerp(position, target, t);
                yield return null;
            }

            _player.transform.position = target;

            if (vCamera)
                vCamera.gameObject.SetActive(false);


            _ghostParticle.Stop();
            _player.SetActive(true);
            _ghostParticle.gameObject.SetActive(false);
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