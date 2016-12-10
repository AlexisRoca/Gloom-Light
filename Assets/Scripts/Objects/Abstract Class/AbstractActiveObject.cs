using UnityEngine;
using System.Collections;

abstract public class AbstractActiveObject : AbstractObjects {

    // Event calls when collision
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Player>() != null) {

            Player player = collision.gameObject.GetComponent<Player>();

            //if (player.isInteracted()) {
            //    Play();
            //}
        }
    }
}
