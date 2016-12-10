using UnityEngine;
using System.Collections;

public class TorchKill : MonoBehaviour {

    public Collider m_OurCollider;
    public Player m_player;

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && m_OurCollider != collider && m_player.getLightOn())
            Destroy(collider.gameObject);
    }
}
