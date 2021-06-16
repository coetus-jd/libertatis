using System.Collections;
using PirateCave.Controllers;
using PirateCave.Resources;
using TMPro;
using UnityEngine;

namespace PirateCave.UI
{
    public class CorsairDialogController : MonoBehaviour
    {
        /// <summary>
        /// A primeira "pessoa" do dialog
        /// </summary>
        [SerializeField]
        private GameObject dialogPanel1;

        /// <summary>
        /// A segunda "pessoa" do dialog
        /// </summary>
        [SerializeField]
        private GameObject dialogPanel2;

        /// <summary>
        /// Representa o atual dialog que está sendo exibido
        /// </summary>
        private GameObject currentDialog;

        /// <summary>
        /// Indica se quem deve começar a conversa é a segunda "pessoa"
        /// </summary>
        [SerializeField]
        private bool secondPersonStarts;

        /// <summary>
        /// Indica o índice do próximo texto
        /// </summary>
        private int nextDialogTextIndex;

        private string language = "en-US";

        private bool isLastIndex
        {
            get 
            {
                return (
                    language == "pt-BR"
                    ? nextDialogTextIndex == Resource.Language[language].FinalA.texts.Count - 1
                    : nextDialogTextIndex == Resource.Language[language].FinalB.texts.Count - 1
                );
            }
        }

        [SerializeField]
        private CorsairController corsair;

        void Start()
        {
            if (secondPersonStarts)
                currentDialog = dialogPanel2;
            else
                currentDialog = dialogPanel1;
            
            startDialog();
        }

        public void nextDialog()
        {
            currentDialog.SetActive(false);

            if (isLastIndex)
            {
                finishDialog();
                corsair.shouldWalk = true;
            }
            else
            {
                nextDialogTextIndex++;
                openDialog();
            }        
        }

        private void startDialog()
        {
            // Time.timeScale = 0f;
            openDialog(true);
        }

        private void openDialog(bool ignoreLogic = false)
        {
            if (!ignoreLogic)
                currentDialog = currentDialog == dialogPanel1 ? dialogPanel2 : dialogPanel1;
            
            currentDialog.SetActive(true);

            string text = "";

            if (language == "pt-BR")
                text = Resource.Language[language].FinalA.texts[nextDialogTextIndex];
            else
                text = Resource.Language[language].FinalB.texts[nextDialogTextIndex];

            currentDialog.GetComponentInChildren<TextMeshProUGUI>()
                .text = text;
        }

        private void finishDialog()
        {
            Time.timeScale = 1f;
        }
    }
}