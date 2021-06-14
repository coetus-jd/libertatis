using System.Collections;
using System.Collections.Generic;
using PirateCave.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

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
        [SerializeField]
        private Collider2D pCollider;

        private float horizontalMovement;

        /// <summary>
        /// Configurações
        /// </summary>
        [Header("Jump")]
        [SerializeField]
        private float jumpForce = 6.5f;

        /// <summary>
        /// Indica se o jogador está pulando
        /// </summary>
        private bool isJumping;

        /// <summary>
        /// Localizar o chão
        /// </summary>
        [Header("Ground")]
        [SerializeField]
        private LayerMask groundLayer;

        /// <summary>
        /// Guarda a referência para o objeto que representa o pé do player
        /// </summary>
        [SerializeField]
        private Transform Feet;

        /// <summary>
        /// Indica se o jogador está com os pés no chão
        /// </summary>
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

        private Rigidbody2D rigidBody;

        [Header("Lash")]
        /// <summary>
        /// Collider usado para dar danos nos personagens com a corrente
        /// </summary>
        [SerializeField]
        private GameObject slashCollider;

        /// <summary>
        /// Collider usado para poder se dependurar
        /// </summary>
        [SerializeField]
        private GameObject slashDiagonalCollider;
        
        /// <summary>
        /// Verifica se o player está atacando
        /// </summary>
        private bool isLashing;

        /// <summary>
        /// Verifica se o player está balançando
        /// </summary>
        private bool isSwinging;
        

        private void Start()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();

            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (life <= 0)
            {
                die();
                return;
            }
            movePlayer();
            lash();
            lashDiagonal();
        }

        // void OnBecameInvisible()
        // {
        //     die();
        // }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.CorsairSlash))
                receiveDamage(10f);

            if (col.gameObject.CompareTag(Tags.SkeletonLash))
                receiveDamage(1f);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(Tags.CorsairBullet))
            {
                Destroy(col.gameObject);
                receiveDamage(10f);
            }
        }

        public void receiveDamage(float damage)
        {
            life -= damage;
        }

        public void activateSwinging()
        {
            isSwinging = true;
            animator.SetBool("swing", true);
        }

        public void die()
        {
            phaseController?.youLosePanel?.SetActive(true);
            if (phaseController?._buttonYouLose)
            {
                EventSystem.current.SetSelectedGameObject(phaseController?._buttonYouLose);
            }
            animator.SetFloat("walking", 0f);
            animator.SetBool("running", false);
            animator.SetBool("die", true);
            Destroy(gameObject, 1);
        }

        private void movePlayer()
        {
            horizontalMovement = Input.GetAxis("Horizontal");

            feetGround = Physics2D.OverlapCircle(Feet.position, 0.1f, groundLayer);
            if (feetGround && !isJumping)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, 0f);
            }


            isRunning = Input.GetKey(KeyCode.LeftShift);


            handleMovement();
            handleAnimation();
            handlePlayerJump();
        }

        private void handleMovement()
        {
            if (horizontalMovement != 0)
            {
                float localVelocity = isRunning ? velocity * 2f : velocity;
                transform.Translate(new Vector2(localVelocity * Time.deltaTime, 0f));
                if (horizontalMovement > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);

                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }

            }
            


        }

        private void handlePlayerJump()
        {
            if (feetGround && Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }

        
        private void handleAnimation()
        {
            if (feetGround)
            {
                animator.SetBool("walk", Mathf.Abs(horizontalMovement) > 0);
                animator.SetBool("Ground", feetGround);
                animator.SetBool("running", isRunning && Mathf.Abs(horizontalMovement) > 0);
                
            }
            else
            {
                animator.SetFloat("walking", 0f);
                animator.SetBool("running", false);
            }

            animator.SetBool("jump", isJumping);
        }

        /// <summary>
        /// Essa função será chamada ao terminar a animação de pulo
        /// </summary>
        private void stopJump() => isJumping = false;

        #region Lash attack

        private void lash()
        {
            if (Input.GetKeyDown(KeyCode.K) && !isLashing && feetGround)
            {
                isLashing = true;
                animator.SetBool("lash", true);
            }
        }

        private void lashDiagonal()
        {
            if (feetGround)
            {
                animator.SetBool("swing", false);
                return;
            }

            if (Input.GetKeyDown(KeyCode.K) && !feetGround && !isSwinging)
                enableLashDiagonalCollider();
        }

        /// <summary>
        /// Essa função será chamada automaticamente pela animação quando
        /// a corrente estiver totalmente esticada
        /// </summary>
        private void enableLashCollider()
        {
            if (horizontalMovement < 0f)
                slashCollider.transform.Rotate(0f, 180f, 0);
            else
                slashCollider.transform.Rotate(0f, 0f, 0);

            slashCollider.GetComponent<BoxCollider2D>().enabled = true;
        }

        /// <summary>
        /// Essa função será chamada automaticamente ao final da animação de lash
        /// </summary>
        private void disableLashCollider()
        {
            isLashing = false;
            slashDiagonalCollider.GetComponent<BoxCollider2D>().enabled = false;
            stopTriggerAnimation("lash");
        }

        private void enableLashDiagonalCollider()
        {
            animator.SetBool("lashDiagonal", true);
            slashDiagonalCollider.GetComponent<BoxCollider2D>().enabled = true;
        }

        private void disableLashDiagonalCollider()
        {
            slashDiagonalCollider.GetComponent<BoxCollider2D>().enabled = false;
            stopTriggerAnimation("lashDiagonal");
        }

        #endregion Lash attack

        // <summary>
        /// Função auxiliar chamada por eventos nos finais das animações
        /// </summary>
        /// <param name="parameterName"></param>
        private void stopTriggerAnimation(string parameterName) => animator.SetBool(parameterName, false);
    }
}