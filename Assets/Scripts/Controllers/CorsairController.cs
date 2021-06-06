using UnityEngine;

namespace PirateCave.Controllers
{
    public class CorsairController : MonoBehaviour
    {
        /// <summary>
        /// Animator com todas as animações do corsário
        /// </summary>
        private Animator animator;

        [SerializeField]
        private GameObject skeletonPrefab;

        [Header("Bullet")]
        [SerializeField]
        private Transform bulletInitialPosition;

        [SerializeField]
        private GameObject bulletPrefab;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
                animator.SetBool("shooting", true);
        }
        
        /// <summary>
        /// Essa função será chamada no final da animação de atirar
        /// </summary>
        private void shoot()
        {
            Instantiate(bulletPrefab, bulletInitialPosition.position, Quaternion.identity);
            stopTriggerAnimation("shooting");
        }

        private void invokeSkeletons()
        {
            animator.SetBool("pointing", true);
            Instantiate(skeletonPrefab, transform.position, Quaternion.identity);
            Instantiate(skeletonPrefab, transform.position, Quaternion.identity);
        }

        #region Stop animations

        private void stopTriggerAnimation(string animationName) => animator.SetBool(animationName, false);

        #endregion
    }
}