using UnityEngine;
using PirateCave.Enums;
using TMPro;
using PirateCave.Resources;

namespace PirateCave.Controllers.Colliders
{
    public class ManuscriptController : MonoBehaviour
    {
        private PhaseController phaseController;

        private float maximumY;

        private float minimumY;

        private float interpolation;

        /// <summary>
        /// Áudio que será tocado quando o jogador "abrir" o pargaminho
        /// </summary>
        [SerializeField]
        private AudioClip openAudio;

        /// <summary>
        /// Boolean que verifica se o jogador está dentro do collider do manuscrito
        /// </summary>
        private bool playerIsIn;

        /// <summary>
        /// Elemento da UI que irá exibir o texto do manuscrito
        /// </summary>
        [SerializeField]
        private GameObject manuscriptPanel;

        void Awake()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();
        }

        void Start()
        {
            minimumY = gameObject.transform.position.y;
            maximumY = gameObject.transform.position.y + 0.200f;
        }

        void Update()
        {
            if (playerIsIn)
                openManuscript();

            animate();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.Player))
                playerIsIn = true;
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.Player))
                playerIsIn = false;
        }

        private void openManuscript()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                AudioSource.PlayClipAtPoint(openAudio, gameObject.transform.position);

                manuscriptPanel.SetActive(true);
                manuscriptPanel.GetComponentInChildren<TextMeshProUGUI>()
                    .text = Resource.Language["pt-BR"].Manuscript.texts[0];

                Destroy(gameObject);
            }
        }

        private void animate()
        {
            transform.position = new Vector3(
                gameObject.transform.position.x,
                Mathf.Lerp(minimumY, maximumY, interpolation),
                gameObject.transform.position.z
            );

            interpolation += 1.7f * Time.deltaTime;

            if (interpolation > 1.0f)
            {
                float temp = maximumY;
                maximumY = minimumY;
                minimumY = temp;
                interpolation = 0.0f;
            }
        }
    }
}