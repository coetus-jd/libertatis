using UnityEngine;

namespace PirateCave.Controllers.Prefab
{
    public class MusketSkeletonController : MonoBehaviour
    {
        /// <summary>
        /// A posição da qual a bala irá sair
        /// </summary>
        [SerializeField]
        private Transform musketBulletShootPosition;

        /// <summary>
        /// Prefab da bala que o esqueleto irá disparar
        /// </summary>
        [SerializeField]
        private GameObject bulletPrefab;

        /// <summary>
        /// Animator com todas as animações do corsário
        /// </summary>
        [SerializeField]
        private Animator animator;

        /// <summary>
        /// Indica se o esqueleto está atirando ou não
        /// </summary>
        private bool isShooting;

        /// <summary>
        /// Indica se o esqueleto está recarregando ou não
        /// </summary>
        private bool isRecharging;

        /// <summary>
        /// Quantidade de tiros que o esqueleto dará até recarregar
        /// </summary>
        private const int shootQuantityToRecharge = 3;

        /// <summary>
        /// Quantidade de tiros que o esqueleto já deu
        /// </summary>
        [SerializeField]
        private int shootQuantity;

        void Update()
        {
            if (!isShooting && !isRecharging)
                attackPlayer();
        }

        private void recharge()
        {
            animator.SetBool("recharge", true);
            isRecharging = true;
        }

        private void attackPlayer()
        {
            if (shootQuantity > shootQuantityToRecharge)
            {
                recharge();
                return;
            }

            animator.SetBool("shooting", true);
            isShooting = true;
            shootQuantity++;
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

        /// <summary>
        /// Essa função será chamada no final da animação de recarregar
        /// </summary>
        private void stopRecharge()
        {
            animator.SetBool("recharge", false);
            shootQuantity = 0;
            isRecharging = false;
        }
    }
}