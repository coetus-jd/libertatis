using System.Collections;
using System.Collections.Generic;
using PirateCave.Controllers.Colliders;
using PirateCave.Enums;
using PirateCave.UI;
using TMPro;
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

        private ChainRenderer chainRenderer;

        /// <summary>
        /// Quantidade de vida do jogador
        /// </summary>
        [Header("Life")]
        [SerializeField]
        private float life = 150;

        /// <summary>
        /// Texto da UI aonde será exibido a vida
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI lifeText;

        /// <summary>
        /// A velocidade com que o player irá se mover
        /// </summary>
        [Header("Move")]
        [SerializeField]
        private float velocity = 4f;

        /// <summary>
        /// Velocidade com que o player irá se mover enquanto estiver pendurado
        /// </summary>
        private float swingVelocity = 0.6f;

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
        [SerializeField]
        private bool isLashing;

        private bool playerStop;

        /// <summary>
        /// Verifica se o player está balançando
        /// </summary>
        private bool isSwinging;

        private GameObject chain;

        private void Start()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();
                
            chainRenderer = GetComponent<ChainRenderer>();

            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            lifeText.text = life.ToString();

            if (life <= 0)
            {
                die();
                return;
            }

            movePlayer();
            lash();
            lashDiagonal();

            if (Input.GetKeyDown(KeyCode.Space) && isSwinging)
                desactivateSwinging();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.CorsairSlash))
                receiveDamage(3f);

            if (col.gameObject.CompareTag(Tags.SkeletonLash))
                receiveDamage(1f);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(Tags.CorsairBullet))
            {
                Destroy(col.gameObject);
                receiveDamage(4f);
            }

            if (col.gameObject.CompareTag(Tags.SkeletonBullet))
            {
                Destroy(col.gameObject);
                receiveDamage(2f);
            }
        }

        void OnBecameInvisible()
        {
            die();
        }

        public void receiveDamage(float damage)
        {
            life -= damage;
        }

        public void activateSwinging(GameObject hookMiddlePosition)
        {
            isSwinging = true;
            animator.SetBool("swing", true);
            animator.SetBool("lashDiagonal", false);
            // chainRenderer.changeEndPosition(hookMiddlePosition.transform);
            chain = hookMiddlePosition;
            // chainRenderer.toggleLineRenderer(true);
            GetComponent<Rigidbody2D>().gravityScale = 2;
            GetComponent<DistanceJoint2D>().connectedAnchor = (Vector2)hookMiddlePosition.transform.position;
            GetComponent<DistanceJoint2D>().enabled = true;
        }

        public void desactivateSwinging()
        {
            isSwinging = false;
            isJumping = false;
            // chainRenderer.toggleLineRenderer();

            chain?.GetComponentInParent<HookController>().destroyChain();
            chain = null;
 
            GetComponent<Rigidbody2D>().gravityScale = 5;
            GetComponent<DistanceJoint2D>().enabled = false;
        }

        public void die()
        {
            if (phaseController?.youLosePanel)
            {
                phaseController?.youLosePanel?.SetActive(true);
            }

            if (phaseController?._buttonYouLose)
                EventSystem.current?.SetSelectedGameObject(phaseController?._buttonYouLose);

            animator.SetBool("walk", false);
            animator.SetBool("running", false);
            animator.SetBool("die", true);
            Destroy(gameObject, 1);
        }

        private void movePlayer()
        {
            horizontalMovement = Input.GetAxis("Horizontal");

            feetGround = Physics2D.OverlapCircle(Feet.position, 0.4f, groundLayer);

            isRunning = Input.GetKey(KeyCode.LeftShift);

            handleRotate();
            handleMovement();
            handleAnimation();
            handlePlayerJump();
        }

        private void stopMoviment()
        {
            playerStop = true;
        }

        private void ReMoviment()
        {
            playerStop = false;
        }

        private void handleMovement()
        {
            if (horizontalMovement != 0 && playerStop == false)
            {
                float localVelocity = isRunning ? velocity * 4f : velocity;
                transform.Translate(new Vector2((localVelocity * Time.deltaTime), 0f));

            }
            else if (!feetGround && isSwinging)
                GetComponent<Rigidbody2D>().AddForce(transform.right * horizontalMovement * swingVelocity);
        }

        private void handleRotate()
        {

            if (horizontalMovement > 0 && isRunning == false)
                transform.rotation = Quaternion.Euler(0, 0, 0);

            else if (horizontalMovement < 0 && isRunning == false)
                transform.rotation = Quaternion.Euler(0, 180, 0);

            else if (horizontalMovement > 0 && isRunning == true)
            {
                if (transform.eulerAngles.y == 180)
                {
                    animator.SetBool("rotate", true);
                }
            }
            else if (horizontalMovement < 0 && isRunning == true)
            {
                if (transform.eulerAngles.y == 0)
                {
                    animator.SetBool("rotate", true);
                }
            }

        }

        private void fimRotate()
        {
            if (horizontalMovement > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);

            else
                transform.rotation = Quaternion.Euler(0, 180, 0);

            animator.SetBool("rotate", false);
        }

        private void handlePlayerJump()
        {
            if (feetGround && Input.GetKeyDown(KeyCode.Space) && playerStop == false)
            {
                rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
            else if (!feetGround && rigidBody.velocity.y < 0)
            {
                animator.SetBool("Falling", true);
                isJumping = false;
            }

            else
            {
                animator.SetBool("Falling", false);

            }
        }

        private void handleAnimation()
        {
            animator.SetBool("walk", Mathf.Abs(horizontalMovement) > 0);
            animator.SetBool("Ground", feetGround);
            animator.SetBool("running", isRunning && Mathf.Abs(horizontalMovement) > 0);
            animator.SetBool("jump", isJumping);
        }

        /// <summary>
        /// Essa função será chamada ao terminar a animação de pulo
        /// </summary>
        private void stopJump() => isJumping = false;

        #region Lash attack

        private void lash()
        {
            if (!isLashing && Input.GetKeyDown(KeyCode.K) && feetGround)
            {
                isLashing = true;
                animator.SetBool("lash", true);
            }
            else
            
            animator.SetBool("lash", false);
            isLashing = false;

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
            slashCollider.GetComponent<BoxCollider2D>().enabled = true;
        }

        /// <summary>
        /// Essa função será chamada automaticamente ao final da animação de lash
        /// </summary>
        private void disableLashCollider()
        {   

            slashCollider.GetComponent<BoxCollider2D>().enabled = false;
            
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