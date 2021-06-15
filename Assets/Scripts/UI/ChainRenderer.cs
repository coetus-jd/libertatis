using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PirateCave.UI
{
    public class ChainRenderer : MonoBehaviour
    {
        /// <summary>
        /// Aonde a corrente começará a ser renderizada, no caso a mão do personagem
        /// </summary>
        [SerializeField]
        private GameObject startRenderer;

        /// <summary>
        /// Ponto inicial e final da linha
        /// </summary>
        [SerializeField]
        private List<Transform> pointsToDraw;

        private UnityEngine.LineRenderer lineRenderer;

        private void Start()
        {
            lineRenderer = GetComponent<UnityEngine.LineRenderer>();
            lineRenderer.startWidth = 0.1f;

            pointsToDraw = new List<Transform>();
            pointsToDraw.Add(startRenderer.transform);
        }

        void Update()
        {
            lineRenderer.positionCount = pointsToDraw.Count;
            lineRenderer.SetPositions(pointsToDraw.Select(p => p.position).ToArray());
        }

        /// <summary>
        /// Muda aonde será o final da linha
        /// </summary>
        /// <param name="endPosition"></param>
        public void changeEndPosition(Transform endPosition)
        {
            if (pointsToDraw.Count == 2)
                pointsToDraw[1] = endPosition;
            else
                pointsToDraw.Add(endPosition);
        }

        /// <summary>
        /// Mostra ou não a linha
        /// </summary>
        public void toggleLineRenderer()
        {
            lineRenderer.enabled = !lineRenderer.enabled;
        }
    }
}