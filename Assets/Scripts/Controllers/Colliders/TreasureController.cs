using PirateCave.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateCave.Controllers.Colliders
{
    public class TreasureController : MonoBehaviour
    {
        private PhaseController phaseController;


        [SerializeField]
        private int Choosen;

        /// <summary>
        /// Boolean que verifica se o jogador está dentro do collider do bau
        /// </summary>
        private bool playerIsIn;

        /// <summary>
        /// Armazenar os pontos
        /// </summary>
        private int treasureScore;

        /// <summary>
        /// Movimento da animação e velocidade
        /// </summary>
        private float aniVelocity = 5f;

        /// <summary>
        /// Áudio que será tocado quando o jogador pegar o baú
        /// </summary>
        [SerializeField]
        private AudioClip catchAudio;

        [Header("Body")]
        [SerializeField]
        private Rigidbody2D rb;

        private float limitS = 1f;
        private float limitF;

        private void Awake()
        {

            limitS += this.transform.position.y;
            limitF = this.transform.position.y;


        }

        private void Start()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                                .GetComponent<PhaseController>();
        }

        void Update()
        {
            Anima();

            pointsTreasure();

            if(playerIsIn)
            {
                destroyTreasure();
            }
            
        }

        private void pointsTreasure()
        {

            if (Choosen == 1)
            {
                //Calice
                treasureScore = 10;
            }
            else if (Choosen == 2)
            {
                //Dobrão
                treasureScore = 20;
            }
            else if (Choosen == 3)
            {
                //Anel
                treasureScore = 30;
            }
            else if (Choosen == 4)
            {
                //DimaV
                treasureScore = 40;
            }
            else if (Choosen == 5)
            {
                //DimaA
                treasureScore = 50;
            }
            else
            {
                //Coroa
                treasureScore = 100;
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

        void Anima()
        {


            if (transform.position.y > limitS)
            {
                rb.velocity = new Vector2(0, -aniVelocity);


            }
            else if (transform.position.y <= limitF)
            {
                rb.velocity = new Vector2(0, aniVelocity);
            }
            
        }

        void destroyTreasure()
        {
            AudioSource.PlayClipAtPoint(catchAudio, gameObject.transform.position);
            phaseController.points += treasureScore;
            Destroy(this.gameObject);
        }
    }

}