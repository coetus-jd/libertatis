using System.Linq;
using PirateCave.Enums;
using UnityEngine;

namespace PirateCave.Controllers.Colliders
{
    public class HookController : MonoBehaviour
    {
        /// <summary>
        ///  Mão do personagem
        /// </summary>
        [SerializeField]
        private GameObject startPosition;

        /// <summary>
        /// Gancho em si
        /// </summary>
        // private GameObject endPosition;

        /// <summary>
        /// Distância entre os fragmentos das correntes
        /// </summary>
        private float distanceBetweenChains = 0.4f;

        /// <summary>
        /// Prefab dos fragmentos da corrente
        /// </summary>
        [SerializeField]
        private GameObject chainFragmentPrefab;

        // SerializeField]
        private GameObject player;

        /// <summary>
        /// Guarda a referência para a última parte da corrente instânciada
        /// </summary>
        private GameObject lastChainInstantiated;
        
        /// <summary>
        /// O "meio" do gancho
        /// </summary>
        [SerializeField]
        private GameObject middle;

        private bool playerSwing;

        private bool chainMounted;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag(Tags.Player);
            lastChainInstantiated = middle;
        }

        void Update()
        {

            if (chainMounted)
                return;

            if (
                Vector2.Distance(startPosition.transform.position, lastChainInstantiated.transform.position) > distanceBetweenChains
                && !chainMounted
                && playerSwing
            )
            {
                InstantiateChain();
            }
            else if (
                Vector2.Distance(startPosition.transform.position, lastChainInstantiated.transform.position) <= distanceBetweenChains
                && !chainMounted
                && playerSwing
            )
            {
                chainMounted = true;
                lastChainInstantiated.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            }
        }

        public void destroyChain()
        {
            var objectChains = middle.transform;

            foreach (Transform child in objectChains)
                GameObject.Destroy(child.gameObject);

            chainMounted = false;
            playerSwing = false;
            lastChainInstantiated = middle;
            Destroy(middle.GetComponent<HingeJoint2D>());
            middle.AddComponent<HingeJoint2D>();
        }

        private void InstantiateChain()
        {
            Vector2 positionToCreate = startPosition.transform.position - lastChainInstantiated.transform.position;
            positionToCreate.Normalize();
            positionToCreate *= distanceBetweenChains;
            positionToCreate += (Vector2)lastChainInstantiated.transform.position;

            GameObject insertedChain = Instantiate(chainFragmentPrefab, positionToCreate, Quaternion.identity);

            insertedChain.transform.SetParent(middle.transform);

            lastChainInstantiated.GetComponent<HingeJoint2D>().connectedBody = insertedChain.GetComponent<Rigidbody2D>();
            lastChainInstantiated = insertedChain;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag(Tags.PlayerHook))
                return;

            playerSwing = true;
            
            col.gameObject
                .GetComponentInParent<PlayerController>()
                .activateSwinging(middle);
        }
    }
}