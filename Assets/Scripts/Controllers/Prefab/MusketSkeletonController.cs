using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Prefab
{
    public class MusketSkeletonController : MonoBehaviour
    {   
        private float life = 6f;

        /// <summary>
        /// Referência para o player
        /// </summary>
        [SerializeField]
        private GameObject player;

        [Header("Bullet")]
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
        private const int shootQuantityToRecharge = 1;

        /// <summary>
        /// Quantidade de tiros que o esqueleto já deu
        /// </summary>
        [SerializeField]
        private int shootQuantity;

        [Header("Life Bar")]
        /// <summary>
        /// GameObject que tem tanto a barra de vida verde quanto vermelha
        /// </summary>
        [SerializeField]
        private GameObject lifeBarsFather;

        /// <summary>
        /// Barra de vida com a cor verde
        /// </summary>
        [SerializeField]
        private Transform greenLifeBar;

        /// <summary>
        /// Tamanho da barra de vida
        /// </summary>
        private Vector3 lifeBarScale;

        /// <summary>
        /// Guarda o valor de 1% referente a vida cheia do personagem
        /// </summary>
        private float lifePercentUnit;

        void Start()
        {
            lifeBarScale = greenLifeBar.localScale;
            lifePercentUnit = lifeBarScale.x / life;
            lifeBarsFather.SetActive(false);
        }

        void Update()
        {
            if (life <= 0f)
            {
                die();
                return;
            }

            if (!isShooting && !isRecharging && player != null)
                attackPlayer();
        }

        void OnBecameInvisible()
        {
            if (life <= 0)
                Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.PlayerWeapon))
                return;

            receiveDamage(3f);
        }

        /// <summary>
        /// Atualiza a barra de vida do esqueleto
        /// </summary>
        private void updateLifeBar()
        {
            lifeBarScale.x = lifePercentUnit * life;
            greenLifeBar.localScale = lifeBarScale;
        }

        private void receiveDamage(float damage)
        {
            life -= damage;
            lifeBarsFather.SetActive(true);
            updateLifeBar();
        }

        private void recharge()
        {
            animator.SetBool("recharge", true);
            isRecharging = true;
        }

        private void attackPlayer()
        {
            if (shootQuantity >= shootQuantityToRecharge)
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

        private void die()
        {
            lifeBarsFather.SetActive(false);
            animator.SetBool("shooting", false);
            animator.SetBool("recharge", false);
            animator.SetBool("die", true);
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        }
    }
}