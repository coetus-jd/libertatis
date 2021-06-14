using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PirateCave.Controllers
{
    public class HomeController : MonoBehaviour
    {
        [Header("Botão para ativar Menu")]
        public Button _botao;

        [Header("Componentes do Menu")]
        public GameObject _menu;

        public GameObject _botaoMenu;

        [Header("Account")]
        [SerializeField]
        private GameObject loginButton; 

        [SerializeField]
        private GameObject registerButton;

        [SerializeField]
        private GameObject updateButton;

        [SerializeField]
        private GameObject logoutButton;
        

        void Awake()
        {
            if (GameController.isPlayerLoggedIn)
            {
                loginButton.SetActive(false);
                registerButton.SetActive(false);
            }
            else
            {
                updateButton.SetActive(false);
                logoutButton.SetActive(false);
            }
        }

        private void Start()
        {
            _botao.onClick = new Button.ButtonClickedEvent();
            _botao.onClick.AddListener(() => ToggleMenu());
        }

        public void ToggleMenu()
        {
            _menu.SetActive(!_menu.gameObject.activeInHierarchy);
            EventSystem.current.SetSelectedGameObject(_botaoMenu);
        }
    }
}