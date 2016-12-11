using UnityEngine;
using System.Collections;

public class TV : AbstractActiveObject
{
    // Attributes
    bool m_isActive = false;
    Light m_light;
    
    float r = 0.5f;
    float g = 0.5f;
    float b = 0.5f;

    // Use this for initialization
    void Start()
    {
        // Animator
        m_light = this.gameObject.GetComponentInChildren<Light>();

        // Object disable
        m_light.enabled = false;
        m_isActive = false;
    }

    void Update()
    {
        if (m_isActive)
        {
            m_light.intensity += (Random.value - 0.5f) * 0.2f;

            r += (Random.value - 0.5f) * 0.1f;
            g += (Random.value - 0.5f) * 0.1f;
            b += (Random.value - 0.5f) * 0.1f;
            m_light.color = new Color(r, g, b);
        }
    }

    // Play animation and sound
    override protected void Play(Collider collider)
    {
        if (m_isActive)
        {
            m_light.enabled = false;
            m_isActive = false;
        }
        else
        {
            m_light.enabled = true;
            m_isActive = true;
        }
    }
}
