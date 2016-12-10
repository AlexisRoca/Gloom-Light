using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    protected CharacterController characterController;
    public Controller m_controller;
    private bool m_interact;

    // Start & Update functions
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        characterController.Move(m_controller.getDisplacement() * Time.deltaTime);
        m_interact = m_controller.getInteractInput();
    }

    // Class functions
    public Player(Controller controller)
    {
        m_controller = controller;
        Debug.Log("Creating Player");
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collider name : " + collider.gameObject.name);

        if (collider.gameObject.tag == "ConeLight")
        {
            //Destroy(this.gameObject);
            Debug.Log("MOOOOOOOORT _ collider.gameObject.name : " + collider.gameObject.name);
            Debug.Log("collider.gameObject.tag : " + collider.gameObject.tag);
        }
    }

    public bool getInteract()
    { return m_interact; }
    public void setInteract(bool interact)
    { interact = m_interact; }
}
