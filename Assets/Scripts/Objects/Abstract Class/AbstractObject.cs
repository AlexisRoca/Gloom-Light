using UnityEngine;
using System.Collections;

abstract public class AbstractObjects : MonoBehaviour
{

    protected Animator m_animator;
    protected AudioSource m_audioSource;

    // Use this for initialization
    void Start() {
        // Animator
        if (this.gameObject.GetComponent<Animator>() != null)
            m_animator = this.gameObject.GetComponent<Animator>();

        // Audio
        if (this.gameObject.GetComponent<AudioSource>() != null)
            m_audioSource = this.gameObject.GetComponent<AudioSource>();
    }


    // Play animation and sound
    abstract protected void Play();

}
