using UnityEngine;
using System.Collections;

public class Fridge : AbstractActiveObject {

    // Attributes
    bool m_isOpen = false;

    // Sound 
    private float m_frequency = 120.0f;
    private bool m_upPeak = false;
    private float m_gain = 0.05f;

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
    override protected void Play() {
        if (m_isOpen) {
            m_animator.Play("Close Anim");
            
            m_isOpen = false;
            m_gain = 0.0f;
        } else {
            m_animator.Play("Open Anim");

            m_isOpen = true;
            m_gain = 0.05f;
        }
    }

    // Play procedural sound
    void OnAudioFilterRead(float[] data, int channels)
    {
        // Update increment in case frequency has changed
        m_increment = m_frequency * 2 * Mathf.PI / m_sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            m_phase = m_phase + m_increment;
            
            // Copy audio data
            //data[i] = (float)(m_gain * Mathf.Sin(m_phase));     // Sinus
            data[i] = (m_upPeak) ? (float)(m_gain) : 0.0f;      // Square

            
            // Stereo sound
            if (channels == 2) data[i + 1] = data[i];

            // Frequency
            if (m_phase > 2 * Mathf.PI) {
                m_phase = 0;
                m_upPeak = !m_upPeak;
            }
        }
    }
}
