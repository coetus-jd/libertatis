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
        [Header ("Life")]
        [SerializeField]
        private float life = 100;

        /// <summary>
        /// A velocidade com que o player irá se mover
        /// </summary>
        [Header ("Move")]
        [SerializeField]
        private float velocity = 0.9f;
        private float horizontalMovement;

        /// <summary>
        /// Configurações
        /// </summary>
        [Header ("Jump")]
        [SerializeField]
        private float jumpForce = 6.5f;
        private bool jumpMove;


        private Rigidbody2D rBody;

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

            handleMovement(horizontalMovement);
            handleAnimation(horizontalMovement);  
            if(Input.GetButtonDown("Jump"))
            {
                float currentForce = Mathf.Lerp(jumpForce, 0.0f, velocity);

                rBody.AddForce(Vector3.up * currentForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }

        private void handleMovement(float horizontalMovement)
        {
            float localVelocity = isRunning ? velocity * 2f : velocity;

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
