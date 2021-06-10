using PirateCave.Controllers.Account;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class HomeController : MonoBehaviour
    {
        /// <summary>
        /// Botão que redireciona para a tela de histórico do jogador
        /// </summary>
        [SerializeField]
        private GameObject trophyButton;

        /// <summary>
        /// UI para ser exibida quando o usuário estiver logado
        /// </summary>
        [SerializeField]
        private GameObject loggedUi;

        /// <summary>
        /// UI para ser exibida quando o usuário não estiver logado
        /// </summary>
        [SerializeField]
        private GameObject loggedOut;

        [SerializeField]
        /// <summary>
        /// Objeto usado para atualizar os pontos do usuário
        /// </summary>
        private PlayerHistoryController playerHistoryController;

        /// <summary>
        /// Menu que será exibido quando o jogador pausar o game
        /// </summary>
        [SerializeField]
        private GameObject optionsMenu;

        void Start()
        {
            playerHistoryController.verifyIfHavePointsToSave();

            if (GameController.loggedPlayer == null)
                loggedOut.SetActive(true);
            else
            {
                loggedUi.SetActive(true);
                trophyButton.SetActive(true);
            }   
        }

        public void togglePauseGame()
        {
            optionsMenu.SetActive(!optionsMenu.activeSelf);
            Time.timeScale = optionsMenu.activeSelf ? 0f : 1f;
        }
    }
}