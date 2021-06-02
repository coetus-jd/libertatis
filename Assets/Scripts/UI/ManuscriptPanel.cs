using PirateCave.Resources;
using TMPro;
using UnityEngine;

namespace PirateCave.UI
{
    public class ManuscriptPanel : MonoBehaviour
    {
        /// <summary>
        /// Guarda o index do próximo manuscrito a ser exibido
        /// </summary>
        private int nextManuscriptIndex;

        /// <summary>
        /// Componente de texto que irá exibir o manuscrito
        /// </summary>
        private TextMeshProUGUI manuscritText;

        void Awake()
        {
            manuscritText = GetComponentInChildren<TextMeshProUGUI>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                gameObject.SetActive(false);
        }

        public void showManuscript()
        {
            gameObject.SetActive(true);
            manuscritText.text = Resource.Language["pt-BR"].Manuscript.texts[nextManuscriptIndex];

            if (nextManuscriptIndex != Resource.Language["pt-BR"].Manuscript.texts.Count - 1)
                nextManuscriptIndex++;
        }
    }
}