using UnityEngine;
using PirateCave.Enums;
using UnityEngine.UI;

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

        [SerializeField]
        private Animator animator;

        /// <summary>
        /// Pontuação ganha por cada tesouro e atualização de pontos na tela
        /// </summary>
        private float treasureScore;
        public Text txtScore;
        bool openTreasure = false;

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
            {
                // openTrunk();
                if (Input.GetKeyDown(KeyCode.X) && openTreasure == false)
                {
                    animator.SetBool("open", true);
                    getTreasure();
                    PlayerPrefs.SetFloat("treasureScore", treasureScore);
                    openTreasure = true;
                }
                    
                
            }
        }

        private void getTreasure()
        {
            float randomNumber = Random.Range(1f, 50f);
            

                if (randomNumber <= 20)
                {
                    treasureScore = treasureScore + 1;  //Calice
                }
                else if (randomNumber <= 30)
                {
                    treasureScore = treasureScore + 2;  //Dobrão
                }
                else if (randomNumber <= 38)
                {
                    treasureScore = treasureScore + 3;  //Anel
                }
                else if (randomNumber <= 44)
                {
                    treasureScore = treasureScore + 4;  //Diamante vermelho
                }
                else if (randomNumber <= 48)
                {
                    treasureScore = treasureScore + 5;  //Diamante Azul
                }
                else
                {
                    treasureScore = treasureScore + 6;  //Coroa
                }

                txtScore.text = "" + treasureScore;
            

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

        /// <summary>
        /// Esse método vai ser chamado automaticamente ao terminar a animação de abrir do baú
        /// </summary>
        private void openTrunk()
        {
            AudioSource.PlayClipAtPoint(catchAudio, gameObject.transform.position);
            phaseController?.addPoints(20);
            Destroy(gameObject);
        }
    }
}