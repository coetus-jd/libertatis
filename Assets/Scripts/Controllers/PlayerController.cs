using System.Collections;
using System.Collections.Generic;
using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Localiza o PhaseController
        /// </summary>
        private PhaseController phaseController;

        /// <summary>
        /// Quantidade de vida do jogador
        /// </summary>
        [Header("Life")]
        [SerializeField]
        private float life = 100;

        /// <summary>
        /// A velocidade com que o player irá se mover
        /// </summary>
        [Header("Move")]
        [SerializeField]
        private float velocity = 0.9f;

        private float horizontalMovement;

        /// <summary>
        /// Configurações
        /// </summary>
        [Header("Jump")]
        [SerializeField]
        private float jumpForce = 6.5f;

        private bool jumpMove;

        /// <summary>
        /// Localizar o chão
        /// </summary>
        [Header("Ground")]
        [SerializeField]
        private LayerMask groundLayer;

        [SerializeField]
        private Transform Feet;

        private bool feetGround;

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

        private Rigidbody2D rBody;

        void Start()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();

            rBody = GetComponent<Rigidbody2D>();
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

            feetGround = Physics2D.OverlapCircle(Feet.position, 0.1f, groundLayer);

            jumpPlayer();
            movePlayer();
        }

        public void receiveDamage(float damage)
        {
            life -= damage;
        }

        private void movePlayer()
        {
            horizontalMovement = Input.GetAxis("Horizontal");

            isRunning = Input.GetKey(KeyCode.LeftShift);

            handleMovement();
            handleAnimation();


        }

        private void jumpPlayer()
        {
            if (feetGround && Input.GetKeyDown(KeyCode.Space))
            {
                rBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpMove = true;
            }
            else
            {
                jumpMove = false;
            }
        }

        private void handleMovement()
        {
            float localVelocity = isRunning ? velocity * 2f : velocity;

            transform.Translate(((Vector3.right * localVelocity) * horizontalMovement) * Time.deltaTime);
        }

        private void handleAnimation()
        {
            if (horizontalMovement != 0.0f && feetGround)
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

            if (jumpMove == true)
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("jump", false);
            }
        }

        public void die()
        {
            animator.SetBool("running", false);
            animator.SetBool("walking", false);
            animator.SetBool("die", true);
            phaseController.youLosePanel.SetActive(true);
            Destroy(gameObject, 1);
        }
    }
}
