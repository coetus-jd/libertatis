using System.Collections;
using System.Collections.Generic;
using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private PhaseController phaseController;

        /// <summary>
        /// Quantidade de vida do jogador
        /// </summary>
        [SerializeField]
        private float life = 100;

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
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();

            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (life <= 0) 
            {
                die();
                return;
            }

            movePlayer();
        }

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

        private void die()
        {
            animator.SetBool("die", true);
            phaseController.youLosePanel.SetActive(true);
            Destroy(gameObject.GetComponent<PlayerController>());
        }
    }
}
