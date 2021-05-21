using PirateCave.Controllers;
using TMPro;
using UnityEngine;

namespace PirateCave.Utilities
{
    public class ChangeTextWithPlayerName : MonoBehaviour
    {
        /// <summary>
        /// Componente de texto que irá mostrar o nome do jogador
        /// </summary>
        private TextMeshProUGUI uiPlayerName;

        void Awake()
        {
            uiPlayerName = gameObject.GetComponent<TextMeshProUGUI>();

            if (uiPlayerName)
                uiPlayerName.text = GameController.loggedPlayer?.name;
        }
    }
}