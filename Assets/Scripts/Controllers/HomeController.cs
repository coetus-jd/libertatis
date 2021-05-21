using UnityEngine;

namespace PirateCave.Controllers
{
    public class HomeController : MonoBehaviour
    {
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

        void Start()
        {
            if (GameController.loggedPlayer == null)
                loggedOut.SetActive(true);
            else
                loggedUi.SetActive(true);
        }
    }
}