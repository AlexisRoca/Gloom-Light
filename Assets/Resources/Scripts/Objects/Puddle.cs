using UnityEngine;
using System.Collections;

public class Puddle : AbstractPassiveObject {

    // Use this for initialization
    void Start()
    {
        // Animator
        m_audioSource = this.gameObject.GetComponent<AudioSource>();
        m_audioSource.Stop();
    }


    // Play animation and sound
    override protected void Play(Collider collider)
    {
        m_audioSource.Play();
    }
}
