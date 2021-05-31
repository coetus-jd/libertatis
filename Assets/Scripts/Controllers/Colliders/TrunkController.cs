using UnityEngine;
using PirateCave.Enums;

namespace PirateCave.Controllers.Colliders
{
    public class TrunkController : MonoBehaviour
    {
        private PhaseController phaseController;

        /// <summary>
        /// Áudio que será tocado quando o jogador pegar o baú
        /// </summary>
        [SerializeField]
        private AudioClip catchAudio;

        /// <summary>
        /// Boolean que verifica se o jogador está dentro do collider do manuscrito
        /// </summary>
        private bool playerIsIn;

        void Awake()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();
        }

        void Update()
        {
            if (playerIsIn)
                openTrunk();
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

        private void openTrunk()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                AudioSource.PlayClipAtPoint(catchAudio, gameObject.transform.position);

                phaseController?.addPoints(20);
                Destroy(gameObject);
            }
        }
    }
}