using UnityEngine;
using System.Collections;

public class TorchKill : MonoBehaviour {
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(collider.gameObject);
            Debug.Log(collider.gameObject.name + " is DEAD !");
        }
    }
}
