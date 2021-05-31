using PirateCave.Controllers;
using TMPro;
using UnityEngine;

namespace PirateCave.Utilities
{
    public class ChangeTextWithPlayerName : MonoBehaviour
    {
        /// <summary>
        /// Define se irá mostrar somente o primeiro nome
        /// </summary>
        [SerializeField]
        private bool showFirstName;

        /// <summary>
        /// Componente de texto que irá mostrar o nome do jogador
        /// </summary>
        private TextMeshProUGUI uiPlayerName;

        void Awake()
        {
            uiPlayerName = gameObject.GetComponent<TextMeshProUGUI>();

            if (uiPlayerName)
            {
                string nameToShow = showFirstName ? GameController.loggedPlayer?.name.Split(' ')[0] : GameController.loggedPlayer?.name;
                uiPlayerName.text = nameToShow ?? "Escravo";
            }
        }
    }
}