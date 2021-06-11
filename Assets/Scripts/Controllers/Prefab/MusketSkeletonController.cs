using UnityEngine;

namespace PirateCave.Controllers.Prefab
{
    public class MusketSkeletonController : MonoBehaviour
    {
        /// <summary>
        /// A posição da qual a bala irá sair
        /// </summary>
        private Transform musketBulletShootPosition;

        /// <summary>
        /// Prefab da bala que o esqueleto irá disparar
        /// </summary>
        private GameObject bulletPrefab;

        /// <summary>
        /// Animator com todas as animações do corsário
        /// </summary>
        private Animator animator;

        private bool isShooting;

        void Update()
        {
            if (!isShooting)
                attackPlayer();
        }

        private void attackPlayer()
        {
            animator.SetBool("shooting", true);
            isShooting = true;
        }

        /// <summary>
        /// Essa função será chamada no momento em que a arma disparar na animação
        /// de atirar
        /// </summary>
        private void shoot()
        {
            Instantiate(bulletPrefab, musketBulletShootPosition.position, Quaternion.identity);
        }
        
        /// <summary>
        /// Essa função será chamada no final da animação de atirar
        /// </summary>
        private void stopShoot()
        {
            animator.SetBool("shooting", false);
            isShooting = false;
        }
    }
}