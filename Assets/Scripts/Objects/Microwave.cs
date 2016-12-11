using UnityEngine;
using System.Collections;

public class Microwave : AbstractActiveObject
{

    // Attributes
    bool m_isActive = false;
    Light m_light;

    float m_timeDuration = 5.0f;
    float m_timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        // Animator
        m_audioSource = this.gameObject.GetComponent<AudioSource>();
        m_light = this.gameObject.GetComponentInChildren<Light>();

        // Object disable
        m_light.enabled = false;
        m_isActive = false;
    }

    void Update()
    {
        if(m_isActive)
        {
            m_light.intensity += (Random.value - 0.5f) * 0.2f;

            m_timer += Time.deltaTime;

            if (m_timer > m_timeDuration)
                Play();
        }
    }

    // Play animation and sound
    override protected void Play()
    {
        if (m_isActive)
        {
            m_audioSource.Stop();
            m_audioSource.enabled = false;

            m_timer = 0.0f;
            m_light.enabled = false;
            m_isActive = false;
        }
        else
        {
            m_audioSource.enabled = true;
            m_audioSource.Play();

            m_light.enabled = true;
            m_isActive = true;
        }
    }
}
