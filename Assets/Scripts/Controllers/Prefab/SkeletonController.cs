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

        [Header("Movement")]
        [SerializeField]
        private float speed;

        void Update()
        {
            checkWhereIsPlayer();
        }

        void FixedUpdate()
        {
            move();
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void move()
        {
            rigidBody.velocity = new Vector2(speed * direction, rigidBody.velocity.y);
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
        }
    }
}