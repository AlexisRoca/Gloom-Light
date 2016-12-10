using UnityEngine;
using System.Collections;

public class Lamp : AbstractActiveObject
{

    // Attributes
    bool m_isOn = false;
    Light m_light;

    // Use this for initialization
    void Start()
    {
        m_light = this.gameObject.GetComponentInChildren<Light>();

        // Object disable
        m_isOn = false;
        m_light.enabled = false;
    }

    // Play animation and sound
    override protected void Play()
    {
        if (m_isOn)
        {
            m_light.enabled = false;

            m_isOn = false;
        }
        else
        {
            m_light.enabled = true;

            m_isOn = true;
        }
    }
}
