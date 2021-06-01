using UnityEngine;
using UnityEngine.UI;

public class SequenceImageUI : MonoBehaviour
{
    public Sprite[] m_Sprites;
    public float m_Time = 0.05f;
    private Image m_Image;
    private int m_Index = 0;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    private void Loop()
    {
        m_Index = ++m_Index % m_Sprites.Length;
        m_Image.sprite = m_Sprites[m_Index];
    }

    private void OnEnable()
    {
        InvokeRepeating("Loop", m_Time, m_Time);
    }

    private void OnDisable()
    {
        CancelInvoke("Loop");
    }
}
