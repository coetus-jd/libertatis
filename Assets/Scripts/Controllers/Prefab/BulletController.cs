using UnityEngine;

namespace PirateCave.Controllers.Prefab
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed = 15.0f;

        void Start()
        {
            /// Se em 5 segundos a bala não tiver atigido o player
            /// quer dizer que ela nem vai, então já podemos destruí-la logo
            Destroy(gameObject, 5f);
        }

        void Update()
        {
            transform.Translate((Vector3.left * bulletSpeed) * Time.deltaTime);
        }

        /// <summary>
        /// Por garantia, caso a bala suma do campo de visão da câmera ela será destruída
        /// </summary>
        /// <returns></returns>
        void OnBecameInvisible() => Destroy(gameObject);
    }
}