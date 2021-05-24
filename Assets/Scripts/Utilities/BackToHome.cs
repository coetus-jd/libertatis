using PirateCave.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PirateCave.Utilities
{
    public class BackToHome : MonoBehaviour
    {
        /// <summary>
        /// Componente de texto que irá mostrar o nome do jogador
        /// </summary>
        private Button backButton;

        void Awake()
        {
            backButton = gameObject.GetComponent<Button>();

            if (backButton)
                backButton.onClick.AddListener(
                    delegate { new GameController().playScene("Scenes/Home"); }
                );
        }
    }
}