using UnityEngine;
using System.Collections;

public class Fridge : AbstractActiveObject {

    // Attributes
    bool m_isOpen = false;

    // Sound 
    private float m_frequency = 120.0f;
    private float m_gain = 0.5f;

    private float m_increment;
    private float m_phase;
    private float m_sampling_frequency = 48000.0f;

    // Use this for initialization
    void Start () {
        // Animator
        m_animator = this.gameObject.GetComponent<Animator>();

        // Object disable
        m_isOpen = false;
    }

    // Play animation and sound
    override protected void Play(Collider collider) {
        if (m_isOpen) {
            m_animator.Play("Close Anim");
            
            m_isOpen = false;
        } else {
            m_animator.Play("Open Anim");

            m_isOpen = true;
        }
    }

    // Play procedural sound
    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!m_isOpen)
            return;

        // Update increment in case frequency has changed
        m_increment = m_frequency * 2 * Mathf.PI / m_sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            m_phase = m_phase + m_increment;

            // Copy audio data
            data[i] = (float)(m_gain * Mathf.Sin(m_phase));     // Sinus

            
            // Stereo sound
            if (channels == 2) data[i + 1] = data[i];

            // Frequency
            if (m_phase > 2 * Mathf.PI) m_phase = 0;
        }
    }
}
