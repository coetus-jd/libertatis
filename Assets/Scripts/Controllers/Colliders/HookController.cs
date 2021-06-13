using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Colliders
{
    public class HookController : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.PlayerHook))
                return;
            
            col.gameObject
                .GetComponentInParent<PlayerController>()
                .activateSwinging();
        }
    }
}