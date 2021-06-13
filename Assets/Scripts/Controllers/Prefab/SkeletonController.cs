using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Prefab
{
    public class SkeletonController : MonoBehaviour
    {
        /// <summary>
        /// 1 = anda para a direita | -1 = anda para a esquerda
        /// </summary>
        [SerializeField]
        private int direction;

        [SerializeField]
        private float life = 10f;

        /// <summary>
        /// A posição do jogador
        /// </summary>
        [SerializeField]
        private GameObject player;

        [Header("Raycasting")]
        /// <summary>
        /// Irá verficar se o player está na esquerda
        /// </summary>
        private RaycastHit2D leftPlayer;

        /// <summary>
        /// Irá verficar se o player está na direita
        /// </summary>
        private RaycastHit2D rightPlayer;

        /// <summary>
        /// Irá verficar se o esqueleto ainda está colindo com o chão, ou seja, se não caiu
        /// </summary>
        private RaycastHit2D groundSkeleton;
        
        /// <summary>
        /// Referência para a layer aonde fica o player (foreground)
        /// </summary>
        [SerializeField]
        private LayerMask playerLayer;

        /// <summary>
        /// Referência para a layer aonde fica o cenário (chão)
        /// </summary>
        [SerializeField]
        private LayerMask groundLayer;

        [SerializeField]
        private Vector2 offset;

        [Header("Components")]
        [SerializeField]
        private Rigidbody2D rigidBody;

        /// <summary>
        /// Animator com todas as animações do esqueleto
        /// </summary>
        [SerializeField]
        private Animator animator;

        /// <summary>
        /// O sprite do personagem
        /// </summary>
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [Header("Movement")]
        [SerializeField]
        private float speed;

        [Header("Lash")]
        /// <summary>
        /// Diz se o esqueleto está atacando ou não
        /// </summary>
        private bool isAttacking;

        /// <summary>
        /// Colisor utilizado pela espada para dar o dano
        /// </summary>
        [SerializeField]
        private GameObject lashCollider;

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
            if (life <= 0)
            {
                die();
                return;
            }

            if (!isAttacking)
                checkWhereIsPlayer();
            
            attackPlayer();
            animator.SetBool("walking", direction != 0);
        }

        void FixedUpdate()
        {
            move();
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

        private void move()
        {
            rigidBody.velocity = new Vector2(speed * direction, rigidBody.velocity.y);
        }

        private void receiveDamage(float damage)
        {
            life -= damage;
            lifeBarsFather.SetActive(true);
            updateLifeBar();
        }

        private void checkWhereIsPlayer()
        {
            rightPlayer = Physics2D.Raycast(
                new Vector2(transform.position.x + offset.x, transform.position.y + offset.y),
                Vector2.right,
                10f,
                playerLayer
            );

            if (rightPlayer.collider != null && rightPlayer.collider.gameObject.CompareTag(Tags.Player))
                direction = 1;

            leftPlayer = Physics2D.Raycast(
                new Vector2(transform.position.x - offset.x, transform.position.y + offset.y),
                Vector2.left,
                10f,
                playerLayer
            );

            if (leftPlayer.collider != null && leftPlayer.collider.gameObject.CompareTag(Tags.Player))
                direction = -1;

            groundSkeleton = Physics2D.Raycast(
                new Vector2(transform.position.x, transform.position.y - 2),
                Vector2.down,
                1f,
                groundLayer
            );

            if (groundSkeleton.collider == null)
                Destroy(gameObject, 1f);

            if (leftPlayer.collider == null && rightPlayer.collider == null)
                direction = 0;
            
            if (direction != 0)
                spriteRenderer.flipX = (direction > 0);
        }
        private void attackPlayer()
        {
            if (player == null || !player.activeSelf)
                return;

            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer > 2.2f)
            {
                stopLash();
                return;
            }

            if (!isAttacking)
                lash();
        }

        /// <summary>
        /// Essa função será chamada quando a espada estiver estica na animação de ataque
        /// </summary>
        private void lash()
        {
            animator.SetBool("lash", true);
            isAttacking = true;
            direction = 0;
            lashCollider.GetComponent<Collider2D>().enabled = true;

            if (rightPlayer.collider != null)
                lashCollider.transform.Rotate(0, 180f, 0);
            else
                lashCollider.transform.Rotate(0, 0, 0);
        }

        /// <summary>
        /// Essa função será chamada no final da animação de ataque com a espada
        /// </summary>
        private void stopLash()
        {
            isAttacking = false;
            animator.SetBool("lash", false);
            lashCollider.GetComponent<Collider2D>().enabled = false;
        }

        private void die()
        {
            lifeBarsFather.SetActive(false);
            animator.SetBool("lash", false);
            animator.SetBool("walking", false);
            animator.SetBool("die", true);
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        }
    }
}