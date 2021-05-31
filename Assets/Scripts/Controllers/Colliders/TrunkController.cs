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

        void Awake()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.Player))
                return;

            if (Input.GetKeyDown(KeyCode.X))
            {
                AudioSource.PlayClipAtPoint(catchAudio, gameObject.transform.position);

                phaseController?.addPoints(20);
                Destroy(gameObject);
            }
        }
    }
}