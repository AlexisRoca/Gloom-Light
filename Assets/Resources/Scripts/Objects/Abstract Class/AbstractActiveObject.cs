using UnityEngine;
using System.Collections;

abstract public class AbstractActiveObject : AbstractObjects {

    // Event calls when in trigger and interact
    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.GetComponent<Player>() != null) {

            Player player = collider.gameObject.GetComponent<Player>();

            if (player.getInteract()) {
                player.m_nbInterraction += 1;
                Play(collider);
            }
        }
    }
}
