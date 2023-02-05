using System.Collections;
using UnityEngine;

namespace Effects
{
    public class StoneTeleport : MonoBehaviour
    {
        [SerializeField] private Vector3 upBorder;
        [SerializeField] private Vector3 downBorder;
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private int maxCount;
        [SerializeField] private float delay;

        private GameObject[] pool;


        private IEnumerator Start()
        {
            if (prefabs.Length == 0) yield break;

            pool = new GameObject[maxCount];
            for (int i = 0; i < maxCount; i++)
            {
                var index = Random.Range(0, prefabs.Length);
                pool[i] = Instantiate(prefabs[index], GetRandomPosition(), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0f, 1f) * delay);
            }
        }


        private void Update()
        {
            if (pool == null || pool.Length == 0) return;

            for (int i = 0; i < pool.Length; i++)
            {
                var o = pool[i];
                if (o == null) continue;
                if (o.transform.position.y > downBorder.y || Random.Range(0, 1f) > 0.5f) continue;

                o.gameObject.SetActive(false);
                var v3 = transform.TransformPoint(GetRandomPosition());
                v3.z = 0;
                o.transform.localPosition = v3;
                o.gameObject.SetActive(true);
            }
        }

        private Vector3 GetRandomPosition()
        {
            var rX = Random.Range(downBorder.x, upBorder.x);
            var rY = Random.Range(upBorder.y, upBorder.y + 10f);
            return new Vector3(rX, rY, 0);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(new Vector3(downBorder.x, upBorder.y, 0), new Vector3(upBorder.x, upBorder.y + 10, 0));
            Gizmos.DrawWireCube(downBorder, Vector3.one);
        }
    }
}