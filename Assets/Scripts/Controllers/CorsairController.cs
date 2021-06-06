using System.Collections;
using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class CorsairController : MonoBehaviour
    {
        [SerializeField]
        private float life = 300f;

        /// <summary>
        /// Velocidade com que o corsário irá se mover
        /// </summary>
        private float velocity = 0.9f;

        /// <summary>
        /// Indica se o corsário deve andar ou não
        /// </summary>
        private bool shouldWalk = true;

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

        [Header("Slash")]
        /// <summary>
        /// Collider usado para dar danos no player com a espada
        /// </summary>
        [SerializeField]
        private BoxCollider2D slashCollider;

        [Header("UI")]
        [SerializeField]
        private GameObject youWinPanel;

        /// <summary>
        /// O sprite do personagem
        /// </summary>
        private SpriteRenderer spriteRenderer;

        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            // somente pra teste
            if (Input.GetKeyDown(KeyCode.W))
                slash();
            
            // somente pra teste
            if (Input.GetKeyDown(KeyCode.T))
                animator.SetBool("shooting", true);

            if (life <= 0)
                defeated();

            // somente pra teste
            if (life == 300 && shouldWalk)
                handleMovement(true);
        }

        void OnTriggerStay2D(Collider2D col)
        {
            Debug.Log(col.gameObject.tag);

            if (!col.gameObject.CompareTag(Tags.PlayerWeapon))
                return;

            receiveDamage(1f);
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
            animator.SetBool("hit", true);
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

        private void invokeSkeletons()
        {
            animator.SetBool("pointing", true);
            Instantiate(skeletonPrefab, transform.position, Quaternion.identity);
            Instantiate(skeletonPrefab, transform.position, Quaternion.identity);
        }

        private void defeated()
        {
            animator.SetBool("defeat", true);
            youWinPanel.SetActive(true);
            Destroy(gameObject.GetComponent<CorsairController>());
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

        #endregion
    }
}