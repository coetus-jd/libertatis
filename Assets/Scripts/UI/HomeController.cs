using UnityEngine;
using UnityEngine.UI;

namespace PirateCave.Controllers
{
    public class HomeController : MonoBehaviour
    {
        public Button _botao;
        public GameObject _menu;

        private void Start()
        {
            _botao.onClick = new Button.ButtonClickedEvent();
            _botao.onClick.AddListener(() => ToggleMenu());
        }

        public void ToggleMenu()
        {
            _menu.SetActive(!_menu.gameObject.activeInHierarchy);
        }
    }
}