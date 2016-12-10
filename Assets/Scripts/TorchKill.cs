using UnityEngine;
using System.Collections;

public class TorchKill : MonoBehaviour {

    //public Collider OurCollider;

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("collider name : " + collider.gameObject.name);

        if (collider.gameObject.tag == "Player")
        {
            Destroy(collider.gameObject);
            //Debug.Log("MOOOOOOOORT _ collider.gameObject.name : " + collider.gameObject.name);
            //Debug.Log("collider.gameObject.tag : " + collider.gameObject.tag);
        }
    }
}
