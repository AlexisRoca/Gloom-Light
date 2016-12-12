using UnityEngine;
using System.Collections;

public class TorchKill : MonoBehaviour {

    // public Collider m_OurCollider;
    public Player m_player;

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && m_player.getLightOn())
        {
            // Try if there is no obstable between both players
            RaycastHit hit;
            Vector3 origin = m_player.transform.position;
            Vector3 direction = (collider.transform.position - m_player.transform.position).normalized;

            if (Physics.Raycast(origin, direction, out hit))
            {
                if (hit.collider.gameObject == collider.gameObject)
                {
                    m_player.m_nbKill += 1;
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
