using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    protected CharacterController characterController;
    private Controller m_controller;
    private bool m_interact;

    // Start & Update functions
    void Start()
    {
        m_controller = new Pad();
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

    public bool getInteract()
    { return m_interact; }
    public void setInteract(bool interact)
    { interact = m_interact; }
}
