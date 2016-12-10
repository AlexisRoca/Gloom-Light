using UnityEngine;
using System.Collections;

public class Fridge : AbstractActiveObject {

    // Attributes
    public Light light;
    bool isOpen = false;


    // Use this for initialization
    void Start () {
        // Object disable
        light.enabled = false;
        isOpen = false;
    }

    // Play animation and sound
    override protected void Play() {
        if (isOpen) {
            m_animator.Play("Close");

            light.enabled = false;
            isOpen = false;
        } else {
            m_animator.Play("Open");

            light.enabled = true;
            isOpen = true;
        }
    }
}
