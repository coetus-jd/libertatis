using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Colliders
{
    public class HookController : MonoBehaviour
    {
        [SerializeField]
        private float throwVelocity;

        [Header("Effect")]
        [SerializeField]
        private GameObject player;

        [SerializeField]
        private Rigidbody2D rigidBody;

        private HingeJoint2D ropeEffect;

        void Start()
        {
            ropeEffect = GetComponent<HingeJoint2D>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.PlayerHook))
                return;
            Debug.Log("Entrou");

            ropeEffect.connectedBody = player.GetComponent<Rigidbody2D>();
            
            col.gameObject
                .GetComponentInParent<PlayerController>()
                .activateSwinging();
        }
    }
}