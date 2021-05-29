using PirateCave.Controllers;
using TMPro;
using UnityEngine;

namespace PirateCave.Utilities
{
    public class ChangeTextWithPlayerNick : MonoBehaviour
    {
        /// <summary>
        /// Componente de texto que irá mostrar o nick do jogador
        /// </summary>
        private TextMeshProUGUI uiPlayerNick;

        void Awake()
        {
            uiPlayerNick = gameObject.GetComponent<TextMeshProUGUI>();

            if (uiPlayerNick)
                uiPlayerNick.text = GameController.loggedPlayer?.nick;
        }
    }
}