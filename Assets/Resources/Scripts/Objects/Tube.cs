using UnityEngine;
using System.Collections;

public class Tube : AbstractActiveObject
{
    // Attributes
    public GameObject trigger0;
    public GameObject trigger1;

    // Play animation and sound
    override protected void Play(Collider collider)
    {
        GameObject player = collider.gameObject;

        float dist1 = Vector3.Distance(trigger0.transform.position, collider.gameObject.transform.position);
        float dist2 = Vector3.Distance(trigger1.transform.position, collider.gameObject.transform.position);

        if(dist1<dist2)
        {
            player.transform.position = new Vector3(trigger1.transform.position.x, player.transform.position.y, trigger1.transform.position.z);
        }
        else
        {
            player.transform.position = new Vector3(trigger0.transform.position.x, player.transform.position.y, trigger0.transform.position.z);
        }
    }
}
