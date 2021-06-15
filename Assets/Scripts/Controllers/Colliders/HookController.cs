using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Colliders
{
    public class HookController : MonoBehaviour
    {
        [Header("Effect")]
        [SerializeField]
        private GameObject player;

        private DistanceJoint2D ropeEffect;

        void Start()
        {
            ropeEffect = player.GetComponent<DistanceJoint2D>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.PlayerHook))
                return;

            ropeEffect.enabled = true;
            ropeEffect.connectedAnchor = (Vector2) transform.position;
            
            col.gameObject
                .GetComponentInParent<PlayerController>()
                .activateSwinging();
        }
    }
}