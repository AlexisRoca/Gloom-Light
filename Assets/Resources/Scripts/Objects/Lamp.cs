using UnityEngine;
using System.Collections;

public class Lamp : AbstractActiveObject
{

    // Attributes
    bool m_isOn = false;
    Light m_light;
    //Renderer m_materialLights;

    // Use this for initialization
    void Start()
    {
        m_light = this.gameObject.GetComponentInChildren<Light>();
        //m_materialLights = GetComponentInChildren<Renderer>();
        // Object disable
        m_isOn = false;
        m_light.enabled = false;
    }

    // Play animation and sound
    override protected void Play(Collider collider)
    {
        if (m_isOn)
        {
            m_light.enabled = false;
            //m_materialLights.material.SetColor("_EmissionColor", Color.white);
            m_isOn = false;
        }
        else
        {
            m_light.enabled = true;
            //m_materialLights.material.SetColor("_EmissionColor", Color.black);
            m_isOn = true;
        }
    }
}
