using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        /// <summary>
        /// Quantidade de vida do inimigo
        /// </summary>
        private float life = 50f;

        private PhaseController phaseController;

        void Awake()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();
        }

        void Update()
        {
            if (life <= 0)
                die();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            // Quer dizer que chegou até o player e vai "bater" nele
            // Colocar animação do inimigo "batendo" depois
            if (col.gameObject.CompareTag(Tags.Player))
                col.gameObject.GetComponent<PlayerController>().receiveDamage(10f);

            // A arma do jogador está batendo no inimigo
            if (col.gameObject.CompareTag(Tags.PlayerWeapon))
                receiveDamage(20f);
        }

        public void receiveDamage(float damage)
        {
            life -= damage;
        }

        private void die()
        {
            Destroy(gameObject);
            phaseController?.addPoints(10);
        }
    }
}