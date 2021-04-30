using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// A velocidade com que o player irá se mover
        /// </summary>
        [SerializeField]
        private float velocity;

        void Update()
        {
            move();
        }

        private void move()
        {
            float currentXPosition = gameObject.transform.position.x;

            gameObject.transform.Translate(new Vector3(
                currentXPosition + (1 * velocity),
                gameObject.transform.position.y,
                gameObject.transform.position.z)
            );
        }
    }
}