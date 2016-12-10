using UnityEngine;
using System.Collections;

public class Fridge : AbstractActiveObject {

    // Attributes
    new Light light;
    bool isOpen = false;

    // Use this for initialization
    void Start () {
        // Get the SpotLight Component
        light = this.gameObject.GetComponent<Light>();

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
