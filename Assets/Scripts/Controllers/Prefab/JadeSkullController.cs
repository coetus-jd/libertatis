using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Prefab
{
    public class JadeSkullController : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.Player))
               return;

            Destroy(gameObject);
        }
    }
}