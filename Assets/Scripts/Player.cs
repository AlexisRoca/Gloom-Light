﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    protected CharacterController m_characterController;
    public Controller m_controller;
    private bool m_interact;
    private bool m_lightOn = false;
    private Quaternion m_prevLightOrientation;

    public float lightOnCooldown = 3.0f;
    public float lightOnDuration = 1.0f;
    private float lightOnTime;

    // Start & Update functions
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        this.GetComponentInChildren<Light>().enabled = false;
    }


    public void updatePlayer()
    {
        // Get angle of right and left stick
        Vector3 displacementVector = m_controller.getDisplacement();
        Vector2 aimVector = m_controller.getAngleTorchlight();

        // Get all input button
        m_interact = m_controller.getInteractInput();
        m_lightOn  = m_controller.getLightInput();

        // Move character
        m_characterController.Move(displacementVector * Time.deltaTime);

        // Aim
        if(aimVector.x != 0.0f || aimVector.y != 0.0f)
        {
            float lightAngle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90.0f;
            this.transform.localEulerAngles = new Vector3(0.0f, lightAngle, 0.0f);
        }
        else if(displacementVector.x != 0.0f || displacementVector.z != 0.0f)
        {
            float characterAngle = Mathf.Atan2(-displacementVector.z, displacementVector.x) * Mathf.Rad2Deg + 90.0f;
            this.transform.localEulerAngles = new Vector3(0.0f, characterAngle, 0.0f);

            //float lightAngle = Mathf.Atan2(-displacementVector.z, displacementVector.x) * Mathf.Rad2Deg + 90.0f;
            //this.GetComponentInChildren<Light>().transform.localEulerAngles = new Vector3(0.0f, lightAngle, 0.0f);

            m_prevLightOrientation = this.GetComponentInChildren<Light>().transform.rotation;
        }
        else
            this.GetComponentInChildren<Light>().transform.rotation = m_prevLightOrientation;

        // Turn on the light
        //if(m_controller.getLightInput() && !m_lightOn && ((Time.time-lightOnTime+lightOnDuration) > lightOnCooldown))
        //{
        //    this.GetComponentInChildren<Light>().enabled = true;
        //    m_lightOn = true;
        //    lightOnTime = Time.time;
        //}
        //else if(m_lightOn && ((Time.time-lightOnTime) > lightOnDuration))
        //{
        //    this.GetComponentInChildren<Light>().enabled = false;
        //    m_lightOn = false;
        //}

        // To comment
        if (m_controller.getLightInput())
        {
            this.GetComponentInChildren<Light>().enabled = true;
            this.GetComponentInChildren<Light>().GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        else
        {
            this.GetComponentInChildren<Light>().enabled = false;
            this.GetComponentInChildren<Light>().GetComponentInChildren<MeshRenderer>().enabled = false;
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