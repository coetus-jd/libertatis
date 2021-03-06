using System.Collections;
using PirateCave.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PirateCave.Controllers
{
    public class CorsairController : MonoBehaviour
    {
        private PhaseController phaseController;

        [SerializeField]
        private float life = 260f;

        /// <summary>
        /// Velocidade com que o corsário irá se mover
        /// </summary>
        private float velocity = 0.9f;

        /// <summary>
        /// Indica se o corsário deve andar ou não
        /// </summary>
        public bool shouldWalk;

        /// <summary>
        /// Indica se o corsário deve atacar ou não
        /// </summary>
        private bool shouldAttack;

        /// <summary>
        /// Animator com todas as animações do corsário
        /// </summary>
        private Animator animator;

        /// <summary>
        /// A posição do jogador
        /// </summary>
        [SerializeField]
        private GameObject player;

        [Header("Skeleton")]
        [SerializeField]
        private GameObject skeletonPrefab;

        /// <summary>
        /// Todas as possíveis posições para um esqueleto aparecer quando
        /// for invocado pelo corsário
        /// </summary>
        [SerializeField]
        private Transform[] skeletonsPositions;

        [Header("Bullet")]
        [SerializeField]
        private Transform bulletInitialPosition;

        [SerializeField]
        private GameObject bulletPrefab;

        [Header("Slash")]
        /// <summary>
        /// Collider usado para dar danos no player com a espada
        /// </summary>
        [SerializeField]
        private BoxCollider2D slashCollider;

        /// <summary>
        /// O sprite do personagem
        /// </summary>
        private SpriteRenderer spriteRenderer;

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

        [SerializeField]
        private GameObject jadeSkull;

        private void Start()
        {
            lifeBarScale = greenLifeBar.localScale;
            lifePercentUnit = lifeBarScale.x / life;
            lifeBarsFather.SetActive(false);

            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            jadeSkull.SetActive(true);
        }

        private void Update()
        {
            if (life <= 0)
            {
                defeated();
                return;
            }

            if (life == 260f && shouldWalk)
                handleMovement(true);

            if (life == 200f || life == 139f)
                invokeSkeletons(life);

            if (life <= 150f)
                shouldAttack = true;

            if (shouldAttack)
                attackPlayer();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.PlayerWeapon))
                return;

            receiveDamage(10f);
        }

        /// <summary>
        /// Atualiza a barra de vida do corsário
        /// </summary>
        private void updateLifeBar()
        {
            lifeBarScale.x = lifePercentUnit * life;
            greenLifeBar.localScale = lifeBarScale;
        }

        private void handleMovement(bool runFromSlave = false)
        {
            spriteRenderer.flipX = runFromSlave;
            Vector3 direction = (runFromSlave ? Vector3.right : Vector3.left);

            animator.SetFloat("walking", 1);

            transform.Translate(((direction * velocity) * 1f) * Time.deltaTime);

            StartCoroutine(stopWalking());
        }

        /// <summary>
        /// Artificialmente paramos o andar do corsário
        /// </summary>
        /// <returns></returns>
        private IEnumerator stopWalking()
        {
            yield return new WaitForSeconds(2f);

            spriteRenderer.flipX = false;
            animator.SetFloat("walking", 0);
            shouldWalk = false;
        }

        private void receiveDamage(float damage)
        {
            life -= damage;
            lifeBarsFather.SetActive(true);
            updateLifeBar();
            animator.SetBool("hit", true);
        }

        private void attackPlayer()
        {
            if (player == null || !player.activeSelf)
                return;

            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= 2.9f)
                slash();
            else
                animator.SetBool("shooting", true);
        }

        /// <summary>
        /// Essa função será chamada no final da animação de atirar
        /// </summary>
        private void shoot()
        {
            Instantiate(bulletPrefab, bulletInitialPosition.position, Quaternion.identity);
        }

        private void slash()
        {
            animator.SetBool("slash", true);
            slashCollider.enabled = true;
        }

        private void invokeSkeletons(float corsairLife)
        {
            life = corsairLife - 1;
            animator.SetBool("pointing", true);
            Instantiate(skeletonPrefab, skeletonsPositions[0].position, Quaternion.identity);
            Instantiate(skeletonPrefab, skeletonsPositions[1].position, Quaternion.identity);
        }

        private void defeated()
        {
            animator.SetBool("defeat", true);
            animator.SetBool("shooting", false);
            animator.SetBool("slash", false);
            animator.SetBool("pointing", false);

            // phaseController?.youWinPanel?.SetActive(true);
            // if (phaseController?._buttonWin)
            // {
            //     EventSystem.current.SetSelectedGameObject(phaseController?._buttonWin);
            // }

            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }

        #region Animations

        /// <summary>
        /// Função auxiliar chamada por eventos nos finais das animações
        /// </summary>
        /// <param name="parameterName"></param>
        private void stopTriggerAnimation(string parameterName) => animator.SetBool(parameterName, false);

        /// <summary>
        /// Essa função será chamada no final da animação de ataque com a espada
        /// </summary>
        private void stopSlash()
        {
            animator.SetBool("slash", false);
            slashCollider.enabled = false;
        }

        #endregion Animations
    }
}