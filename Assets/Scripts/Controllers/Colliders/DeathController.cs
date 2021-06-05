using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Colliders
{
    public class DeathController : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(col);

            if (!col.gameObject.CompareTag(Tags.Player))
                return;
            
            col.gameObject.GetComponent<PlayerController>().die();
        }
    }
}