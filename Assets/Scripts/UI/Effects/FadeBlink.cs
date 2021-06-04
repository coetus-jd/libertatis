using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeBlink : MonoBehaviour
{
    //Blink
    public enum BlinkUIState { None, Running }

    [Header("Blink")]
    public float m_SmoothTime = 8.0f;

    public float m_EnableTime = 0.75f;
    private BlinkUIState m_State = BlinkUIState.None;

    //Fade
    [Header("Fade")]
    public float m_Time = 0.5f;

    private float m_StartTime;
    public AnimationCurve m_Curve;

    //Comun
    [Header("Comun")]
    private CanvasGroup m_CanvasGroup;

    private void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        Invoke("Enable", m_EnableTime);
    }

    private IEnumerator Start()
    {
        m_CanvasGroup.alpha = 1.0f;
        m_StartTime = Time.time;

        yield return new WaitForSeconds(m_Time);
        m_State = BlinkUIState.Running;
    }

    private void Update()
    {
        if (m_State != BlinkUIState.Running)
        {
            m_CanvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, m_Curve.Evaluate((Time.time - m_StartTime) / m_Time));
            return;
        }

        m_CanvasGroup.alpha = (Mathf.Sin(Time.time * m_SmoothTime) + 1.0f) * 0.5f;
    }
}