using PirateCave.Base;
using PirateCave.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PirateCave.Controllers
{
    public class LoginController : MonoBehaviour
    {
        [SerializeField]
        private GameController gameController;

        /// <summary>
        /// Objeto no qual será exibido a mensagem do servidor
        /// </summary>
        [SerializeField]
        private GameObject message;

        /// <summary>
        /// Imagem a ser utilizada ao receber uma resposta de sucesso
        /// </summary>
        [SerializeField]
        private Sprite success;

        /// <summary>
        /// Imagem a ser utilizada ao receber uma resposta de erro
        /// </summary>
        [SerializeField]
        private Sprite error;

        /// <summary>
        /// Input do usuário com o valor do nick
        /// </summary>
        [SerializeField]
        private GameObject inputNick;

        /// <summary>
        /// Botão de cadastrar
        /// </summary>
        [SerializeField]
        private Button buttonLogin;

        /// <summary>
        /// Loga um jogador
        /// </summary>
        public void login()
        {
            buttonLogin.interactable = false;

            Player player = new Player()
            {
                nick = inputNick.GetComponent<TMP_InputField>().text
            };

            StartCoroutine(Request.get($"/players/{player.nick}", handleResponse));

            buttonLogin.interactable = true;
        }

        /// <summary>
        /// Trata a resposta vinda do servidor
        /// </summary>
        private void handleResponse(Response response)
        {
            message.SetActive(true);

            if (response == null || !string.IsNullOrEmpty(response.data.error))
            {
                message.GetComponentInChildren<Image>().sprite = error;
                message.GetComponent<TMP_InputField>().text = response?.data.error ?? "Não foi possível se comunicar com o servidor";
                return;
            }

            message.GetComponentInChildren<Image>().sprite = success;
            message.GetComponent<TMP_InputField>().text = "Logado com sucesso";

            redirectToHome((Player)response.data.player);
        }

        private void redirectToHome(Player player)
        {
            PlayerPrefs.SetString("nick", player.nick);

            gameController.playScene("Scenes/Home");
        }
    }
}
