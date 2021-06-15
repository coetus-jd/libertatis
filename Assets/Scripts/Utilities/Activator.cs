using System.Collections.Generic;
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
        private List<GameObject> objectsToActivate;
        
        /// <summary>
        /// Tag necessária que o colisor tenha para ativar a ação
        /// </summary>
        [SerializeField]
        private string tagToCompare = "Player";

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(tagToCompare))
            {
                if (objectsToActivate == null || objectsToActivate.Count == 0)
                    return;

                objectsToActivate.ForEach(gameObject =>
                {
                    gameObject.SetActive(true);
                });
            }
        }
    }
}