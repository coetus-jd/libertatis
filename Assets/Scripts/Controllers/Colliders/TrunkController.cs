using UnityEngine;
using PirateCave.Enums;
using UnityEngine.UI;
using System.Collections;

namespace PirateCave.Controllers.Colliders
{
    public class TrunkController : MonoBehaviour
    {

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

        private bool openTreasure = false;


        /// <summary>
        /// Boolean que verifica se o jogador está dentro do collider do manuscrito
        /// </summary>
        private bool playerIsIn;

        
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
                tChoose = Calice;
                }
                else if (randomNumber <= 75)
                {
                tChoose = Dobrao;
                }
                else if (randomNumber <= 90)
                {
                tChoose = Anel;
                }
                else if (randomNumber <= 95)
                {
                tChoose = DimaV;
                }
                else if (randomNumber <= 99)
                {
                tChoose = DimaA;
                }
                else
                {
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
    }
}