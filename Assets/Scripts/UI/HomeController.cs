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