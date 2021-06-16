using System.Collections;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PirateController : MonoBehaviour
    {
        public bool shouldWalk;
        
        /// <summary>
        /// Animações do pirata
        /// </summary>
        [SerializeField]
        private Animator animator;

        /// <summary>
        /// Velocidade com que o pirata irá se mover
        /// </summary>
        private float velocity = 0.9f;

        void Update()
        {
            if (shouldWalk)
                walk();
        }

        public IEnumerator pointing()
        {
            animator.SetBool("pointing", true);
            yield return new WaitForSeconds(3f);
            animator.SetBool("pointing", false);
        }

        public void rotate() => animator.SetBool("rotation", true);

        public void walk()
        {
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetBool("walk", true);

            transform.Translate(((Vector3.right * velocity) * 1f) * Time.deltaTime);

            StartCoroutine(stopWalking());
        }

        /// <summary>
        /// Essa funçãos será chamada após o final da rotation
        /// </summary>
        public void rotationReverse()
        {
            animator.SetBool("rotation", false);
            animator.SetBool("rotationReverse", true);
        }

        public void stopBoolParameter(string parameterName) => animator.SetBool(parameterName, false);

        public void destroyPirate() => Destroy(gameObject);

        public void stopFinalRotation()
        {
            animator.SetBool("rotation", false);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        /// <summary>
        /// Artificialmente paramos o andar do pirata
        /// </summary>
        /// <returns></returns>
        private IEnumerator stopWalking()
        {
            yield return new WaitForSeconds(4f);

            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetBool("walk", false);
            shouldWalk = false;
        }
    }
}