using UnityEngine;
using System.Collections;

abstract public class AbstractPassiveObject : AbstractObjects {

    // Event calls when collision
    void OnTriggerEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Player>() != null) {
            Play();
        }
    }
}
