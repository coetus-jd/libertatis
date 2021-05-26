using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Quantidade de vida do jogador
        /// </summary>
        private float life = 100;

        /// <summary>
        /// A altura máxima da tela
        /// </summary>
        private const float maxYPosition = 1.35f;

        /// <summary>
        /// A altura mínima da tela
        /// </summary>
        private const float minYPosition = -1.16f;

        /// <summary>
        /// A largura máxima da tela
        /// </summary>
        private const float maxXPosition = 2.61f;

        /// <summary>
        /// A largura mínima da tela
        /// </summary>
        private const float minXPosition = -2.64f;

        /// <summary>
        /// A velocidade com que o player irá se mover
        /// </summary>
        [SerializeField]
        private float velocity = 0.9f;

        /// <summary>
        /// O sprite do personagem
        /// </summary>
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Animator com todas as animações do player
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Indica se o jogador está correndo ou não
        /// </summary>
        private bool isRunning = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update() => movePlayer();

        public void receiveDamage(float damage)
        {
            life -= damage;
        }

        private void movePlayer()
        {
            float horizontalMovement = Input.GetAxis("Horizontal");

            isRunning = Input.GetKey(KeyCode.LeftShift);

            handleMovement(horizontalMovement);
            handleAnimation(horizontalMovement);
            restrictUserMovement();
        }

        private void handleMovement(float horizontalMovement)
        {
            float localVelocity = isRunning ? velocity * 1.4f : velocity;

            transform.Translate(((Vector3.right * localVelocity) * horizontalMovement) * Time.deltaTime);
        }

        private void handleAnimation(float horizontalMovement)
        {
            if (horizontalMovement != 0.0f)
            {
                spriteRenderer.flipX = (horizontalMovement < 0);

                animator.SetBool(isRunning ? "running" : "walking", true);
                animator.SetBool(isRunning ? "walking" : "running", false);
            }
            else
            {
                animator.SetBool("running", false);
                animator.SetBool("walking", false);
            }

            animator.SetBool("jump", Input.GetKeyDown(KeyCode.Space));
        }

        private void restrictUserMovement()
        {
            // Vertical
            if (transform.position.y > maxYPosition)
                transform.position = new Vector3(transform.position.x, maxYPosition, 0);

            if (transform.position.y < minYPosition)
                transform.position = new Vector3(transform.position.x, minYPosition, 0);

            // Horizontal 
            if (transform.position.x > maxXPosition)
                transform.position = new Vector3(maxXPosition, transform.position.y, 0);

            if (transform.position.x < minXPosition)
                transform.position = new Vector3(minXPosition, transform.position.y, 0);
        }
    }
}
