using UnityEngine;
using System.Collections;

public class Fridge : AbstractActiveObject {

    // Attributes
    bool isOpen = false;


    // Use this for initialization
    void Start () {
        // Object disable
        isOpen = false;

        m_audioSource.enabled = false;
        m_audioSource.loop = true;
    }

    // Play animation and sound
    override protected void Play() {
        if (isOpen) {
            m_animator.Play("Close Anim");
            m_audioSource.enabled = false;

            isOpen = false;
        } else {
            m_animator.Play("Open Anim");
            m_audioSource.enabled = true;

            isOpen = true;
        }
    }
}
