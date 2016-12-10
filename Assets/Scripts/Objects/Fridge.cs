using UnityEngine;
using System.Collections;

public class Fridge : AbstractActiveObject {

    // Attributes
    bool isOpen = false;


    // Use this for initialization
    void Start () {
        // Object disable
        isOpen = false;
    }

    // Play animation and sound
    override protected void Play() {
        if (isOpen) {
            m_animator.Play("Close Anim");
            
            isOpen = false;
        } else {
            m_animator.Play("Open Anim");
            
            isOpen = true;
        }
    }
}
