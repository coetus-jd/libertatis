using System.Collections;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PirateController : MonoBehaviour
    {
        /// <summary>
        /// Animações do pirata
        /// </summary>
        [SerializeField]
        private Animator animator;

        public IEnumerator pointing()
        {
            animator.SetBool("pointing", true);
            yield return new WaitForSeconds(3f);
            animator.SetBool("pointing", false);
        }

        public void rotate() => animator.SetBool("rotation", true);

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
    }
}