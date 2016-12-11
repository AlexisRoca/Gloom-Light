using UnityEngine;
using System.Collections;

public class Torchlight : MonoBehaviour
{
    enum Substate
    {
        WaitForStart,
        Ready,
        On,
        Cooldown
    };

    private Substate m_substate;

    public GameObject m_batteryUI;
    public Light m_light;
    private Renderer m_cone;

    public float m_waitForStartDuration = 5.0f;
    public float m_lightOnCooldown = 3.0f;
    public float m_lightOnDuration = 1.0f;
    private float m_lightOnTime;

    void Start ()
    {
        m_substate = Substate.WaitForStart;
        m_light = this.GetComponentInChildren<Light>();
        m_cone = this.GetComponentInChildren<Light>().GetComponentInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_substate = checkChangeSubstate();
    }

    public bool setOn()
    {
        if(m_substate != Substate.Ready)
            return false;

        m_light.enabled = true;
        m_lightOnTime = Time.time;
        return true;
    }

    Substate checkChangeSubstate()
    {
        switch(m_substate)
        {
            case Substate.WaitForStart:
            if(Time.time > m_waitForStartDuration)
                return Substate.Ready;
            break;

            case Substate.Ready:
            if(m_light.enabled)
                return Substate.On;
            break;

            case Substate.On:
            if((Time.time - m_lightOnTime) > m_lightOnDuration)
            {
                m_light.enabled = false;
                m_batteryUI.GetComponent<Animator>().Play("BatteryCharging");
                return Substate.Cooldown;
            }
            break;

            case Substate.Cooldown:
            if((Time.time - m_lightOnTime + m_lightOnDuration) > m_lightOnCooldown)
            {
                //Draw lightning
                //...

                return Substate.Ready;
            }
            break;
        }

        return m_substate;
    }
}
