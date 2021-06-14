using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateCave.Controllers.Colliders
{
    public class TreasureController : MonoBehaviour
    {

        /// <summary>
        /// Movimento da animação e velocidade
        /// </summary>
        private float aniVelocity = 5f;

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

        }

        void Update()
        {

            Anima();

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





            destroyTreasure();
        }

        void destroyTreasure()
        {
            StartCoroutine(hurtCooldown(1f));

            IEnumerator hurtCooldown(float t)
            {

                yield return new WaitForSeconds(t);

                Destroy(this.gameObject);

            }
        }
    }

}