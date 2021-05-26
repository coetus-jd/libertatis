using UnityEngine;
using PirateCave.Enums;

namespace PirateCave.Controllers.Colliders
{
    public class TrunkController : MonoBehaviour
    {
        private PhaseController phaseController;
        
        void Awake()
        {
            phaseController = GameObject.FindGameObjectWithTag(Tags.PhaseController)
                .GetComponent<PhaseController>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.Player))
                return;

            phaseController?.addPoints(20);
            Destroy(gameObject);
        }
    }
}