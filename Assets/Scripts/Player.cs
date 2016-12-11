using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    protected CharacterController characterController;
    public Controller m_controller;
    private bool m_interact;
    private bool m_lightOn = false;

    public float lightOnCooldown = 3.0f;
    public float lightOnDuration = 1.0f;
    private float lightOnTime;

    // Start & Update functions
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        this.GetComponentInChildren<Light>().enabled = false;
    }


    public void updatePlayer()
    {
        characterController.Move(m_controller.getDisplacement() * Time.deltaTime);

        m_interact = m_controller.getInteractInput();

        if(m_controller.getLightInput() && !m_lightOn && ((Time.time-lightOnTime+lightOnDuration) > lightOnCooldown))
        {
            this.GetComponentInChildren<Light>().enabled = true;
            m_lightOn = true;
            lightOnTime = Time.time;
        }

        else if(m_lightOn && ((Time.time-lightOnTime) > lightOnDuration))
        {
            this.GetComponentInChildren<Light>().enabled = false;
            m_lightOn = false;
        }

        Vector2 aimVector = m_controller.getAngleTorchlight();

        if (aimVector.x != 0.0f || aimVector.y != 0.0f)
        {
            float lightAngle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90.0f;
            this.GetComponentInChildren<Light>().transform.localEulerAngles = new Vector3(0.0f, lightAngle, 0.0f);
        }




    }

    // Class functions
    public Player(Controller controller)
    {
        m_controller = controller;
    }

    public bool getInteract()
    { return m_interact; }
    public void setInteract(bool interact)
    { interact = m_interact; }

    public bool getLightOn()
    { return m_lightOn; }
    public void setLightOn(bool lightOn)
    { lightOn = m_lightOn; }
}