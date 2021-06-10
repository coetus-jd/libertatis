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


        [Header("Archer")]
        /// <summary>
        /// Diz se o esqueleto do tipo arqueiro ou não
        /// </summary>
        [SerializeField]
        private bool isArcher;

        /// <summary>
        /// A posição da qual a flecha irá sair
        /// </summary>
        private Transform arrowShootPosition;

        /// <summary>
        /// Prefab da flecha que o esqueleto irá disparar
        /// </summary>
        private GameObject arrowPrefab;

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

        [Header("Movement")]
        [SerializeField]
        private float speed;

        void Update()
        {
            if (!isArcher)
                checkWhereIsPlayer();
            
            animator.SetBool("walking", direction != 0);
        }

        void FixedUpdate()
        {
            move();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.PlayerWeapon))
                return;

            receiveDamage(3f);
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void move()
        {
            rigidBody.velocity = new Vector2(speed * direction, rigidBody.velocity.y);
        }

        private void receiveDamage(float damage)
        {
            life -= damage;
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
        }

        private void shootArrow()
        {
            Instantiate(arrowPrefab, arrowShootPosition.transform.position, Quaternion.identity);
        }
    }
}