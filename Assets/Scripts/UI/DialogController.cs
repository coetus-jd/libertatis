using System.Collections;
using PirateCave.Resources;
using TMPro;
using UnityEngine;

namespace PirateCave.UI
{
    public class DialogController : MonoBehaviour
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

        private bool isLastIndex
        {
            get 
            {
                return nextDialogTextIndex == Resource.Language["pt-BR"].Dialog1.texts.Count - 1;
            }
        }

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
                finishDialog();
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
            currentDialog.GetComponentInChildren<TextMeshProUGUI>()
                .text = Resource.Language["pt-BR"].Dialog1.texts[nextDialogTextIndex];
        }

        private void finishDialog()
        {
            Time.timeScale = 1f;
        }
    }
}