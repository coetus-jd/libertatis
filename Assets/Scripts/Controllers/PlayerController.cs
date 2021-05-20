using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PlayerController : MonoBehaviour
    {
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
        private float velocity = 8.0f;

        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update() => move();

        private void move()
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            handleAnimation(verticalInput, horizontalInput);

            transform.Translate(((Vector3.right * velocity) * horizontalInput) * Time.deltaTime);
            transform.Translate(((Vector3.up * velocity) * verticalInput) * Time.deltaTime);

            restrictUserMovement();
        }

        private void handleAnimation(float verticalValue, float horizontalValue)
        {
            animator.SetBool("walking", (horizontalValue != 0.0f));
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