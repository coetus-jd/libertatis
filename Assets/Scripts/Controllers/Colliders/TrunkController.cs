using UnityEngine;
using PirateCave.Enums;
using UnityEngine.UI;
using System.Collections;

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
        /// Pontuação ganha por cada tesouro e chamando o tesouro.
        /// </summary>
        [Header("Treasures")]
        [SerializeField]
        private GameObject Calice;
        [SerializeField]
        private GameObject Dobrao;
        [SerializeField]
        private GameObject Anel;
        [SerializeField]
        private GameObject DimaV;
        [SerializeField]
        private GameObject DimaA;
        [SerializeField]
        private GameObject Coroa;

        private GameObject tChoose;

        private int treasureScore;
        private bool openTreasure = false;

        /// <summary>
        /// Conexão com o outro script
        /// </summary>
        private PhaseController pcScript;


        /// <summary>
        /// Boolean que verifica se o jogador está dentro do collider do manuscrito
        /// </summary>
        private bool playerIsIn;

        void Awake()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();
        }

        private void Start()
        {
            pcScript = GameObject.Find("PhaseController").GetComponent<PhaseController>();
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
                    openTreasure = true;
                }
                    
                
            }
        }

        private void getTreasure()
        {   
                
            float randomNumber = Random.Range(1f, 100f);
            
                if (randomNumber <= 50)
                {
                    treasureScore = 10;  //Calice
                    tChoose = Calice;
                }
                else if (randomNumber <= 75)
                {
                    treasureScore = 20;  //Dobrão
                    tChoose = Dobrao;
                }
                else if (randomNumber <= 90)
                {
                    treasureScore = 50;  //Anel
                    tChoose = Anel;
                }
                else if (randomNumber <= 95)
                {
                    treasureScore = 100;  //Diamante vermelho
                    tChoose = DimaV;
                }
                else if (randomNumber <= 99)
                {
                    treasureScore = 200;  //Diamante Azul
                    tChoose = DimaA;
                }
                else
                {
                    treasureScore = 500;  //Coroa
                    tChoose = Coroa;
                }
            

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

        private void summonTreasure()
        {
            Instantiate(tChoose, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        }

        /// <summary>
        /// Esse método vai ser chamado automaticamente ao terminar a animação de abrir do baú
        /// </summary>
        private void openTrunk()
        {
            AudioSource.PlayClipAtPoint(catchAudio, gameObject.transform.position);
            phaseController?.addPoints(20);
            pcScript.points += treasureScore;
            Destroy(this.gameObject);


        }
    }
}