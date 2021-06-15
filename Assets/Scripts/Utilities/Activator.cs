using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Utilities
{
    public class Activator : MonoBehaviour
    {
        /// <summary>
        /// Objeto que será ativado ao entrar na área
        /// </summary>
        [SerializeField]
        private GameObject objectToActivate;
        
        /// <summary>
        /// Tag necessária que o colisor tenha para ativar a ação
        /// </summary>
        [SerializeField]
        private string tagToCompare = "Player";

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(tagToCompare))
                objectToActivate.SetActive(true);
        }
    }
}