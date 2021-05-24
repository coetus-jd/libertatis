using PirateCave.Base;
using PirateCave.Enums;
using PirateCave.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PirateCave.Controllers
{
    public class LoginController : MonoBehaviour
    {
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
        private TMP_InputField inputNick;

        /// <summary>
        /// Botão de cadastrar
        /// </summary>
        [SerializeField]
        private Button buttonLogin;

        /// <summary>
        /// Loga um jogador
        /// </summary>
        /// <param name="nick"></param>
        public void login(string nick = null)
        {
            Player player = new Player()
            {
                nick = !string.IsNullOrEmpty(nick) ? nick : inputNick?.text
            };

            if (string.IsNullOrEmpty(player.nick))
                return;

            buttonLogin.interactable = false;

            StartCoroutine(Request.get($"/players/{player.nick}", handleResponse));
        }

        /// <summary>
        /// Trata a resposta vinda do servidor
        /// </summary>
        private void handleResponse(Response response)
        {
            buttonLogin.interactable = true;
            message.SetActive(true);

            if (response == null || !string.IsNullOrEmpty(response.data.error))
            {
                message.GetComponentInChildren<Image>().sprite = error;
                message.GetComponent<TMP_InputField>().text = response?.data.error ?? "Não foi possível se comunicar com o servidor";
                return;
            }

            message.GetComponentInChildren<Image>().sprite = success;
            message.GetComponent<TMP_InputField>().text = "Logado com sucesso";

            redirectToHome(response.data);
        }

        private void redirectToHome(Data data)
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.Player, JsonUtility.ToJson(data.player));
            PlayerPrefs.SetInt(PlayerPrefsKeys.PlayerPoints, data.todayPoints);

            new GameController().playScene("Scenes/Home");
        }
    }
}
