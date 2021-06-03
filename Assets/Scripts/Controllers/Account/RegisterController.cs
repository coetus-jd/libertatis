using System.Collections;
using System.Text;
using PirateCave.Base;
using PirateCave.Controllers;
using PirateCave.Enums;
using PirateCave.Models;
using PirateCave.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace PirateCave.Controllers.Account
{
    public class RegisterController : MonoBehaviour
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
        private GameObject inputNick;

        /// <summary>
        /// Input do usuário com o valor do nome
        /// </summary>
        [SerializeField]
        private GameObject inputName;

        /// <summary>
        /// Botão de cadastrar
        /// </summary>
        [SerializeField]
        private Button buttonRegister;

        /// <summary>
        /// Registra um novo jogador
        /// </summary>
        public void register()
        {
            buttonRegister.interactable = false;

            Player player = new Player()
            {
                nick = inputNick.GetComponent<TMP_InputField>().text,
                name = inputName.GetComponent<TMP_InputField>().text
            };

            StartCoroutine(Request.post("/players", player, handleRegisterResponse));
        }

        /// <summary>
        /// Trata a resposta vinda do servidor
        /// </summary>
        private void handleRegisterResponse(Response response)
        {
            buttonRegister.interactable = true;
            message.SetActive(true);

            if (response == null || !string.IsNullOrEmpty(response.data.error))
            {
                message.GetComponentInChildren<Image>().sprite = error;
                message.GetComponent<TMP_InputField>().text = response?.data.error ?? "Não foi possível se comunicar com o servidor";
                return;
            }

            message.GetComponentInChildren<Image>().sprite = success;
            message.GetComponent<TMP_InputField>().text = response.data.message;
        }
    }
}
