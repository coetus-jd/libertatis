using UnityEngine;

namespace PirateCave.Controllers
{
    public class CorsairController : MonoBehaviour
    {
        /// <summary>
        /// Animator com todas as animações do corsário
        /// </summary>
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void shoot()
        {
            animator.SetBool("shooting", true);
        }
    }
}