using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Colliders
{
    public class DeathFallController : MonoBehaviour
    {
        /// <summary>
        /// Indica se o jogador deve morrer ou não ao colidir
        /// </summary>
        [SerializeField]
        private bool playerShoudDie;

        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(col);

            if (!col.gameObject.CompareTag(Tags.Player))
                return;
            
            if (playerShoudDie)
                col.gameObject.GetComponent<PlayerController>().die();                

            Destroy(gameObject);
        }
    }
}