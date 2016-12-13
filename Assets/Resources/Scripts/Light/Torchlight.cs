using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Torchlight : MonoBehaviour
{
    enum Substate
    {
        Ready,
        On,
        Cooldown
    };

    private Substate m_substate;

    private Light m_light;
    private Renderer m_cone;
    private Image m_batteryLightning;
    [HideInInspector] public GameObject m_batteryUI;

    public float m_lightCooldown = 3.0f;
    public float m_lightOnDuration = 1.0f;
    private float m_lightOnTime;

    void Start ()
    {
        m_substate = Substate.Ready;
        m_light = this.GetComponentInChildren<Light>();
        m_cone = this.GetComponentInChildren<Light>().GetComponentInChildren<Renderer>();

        for(int j = 0; j < m_batteryUI.transform.childCount; j++)
            if(m_batteryUI.transform.GetChild(j).transform.name == "LightningIcon")
                m_batteryLightning = m_batteryUI.transform.GetChild(j).GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_substate = checkChangeSubstate();

        if(m_substate == Substate.Cooldown)
        {
            float alpha = Mathf.Lerp(0.0f,1.0f,(Time.time-m_lightOnTime-m_lightOnDuration) /m_lightCooldown);
            m_batteryUI.GetComponent<Image>().color = new Color(1.0f,1.0f,1.0f,alpha);
        }
    }

    public bool setOn()
    {
        if(m_substate != Substate.Ready)
            return false;

        m_light.enabled = true;
        m_cone.enabled = true;
        m_lightOnTime = Time.time;

        m_batteryLightning.enabled = false;
        m_batteryUI.GetComponent<Image>().enabled = false;
        return true;
    }

    Substate checkChangeSubstate()
    {
        switch(m_substate)
        {
            case Substate.Ready:
            if(m_light.enabled)
                return Substate.On;
            break;

            case Substate.On:
            if((Time.time - m_lightOnTime) > m_lightOnDuration)
            {
                m_light.enabled = false;
                m_cone.enabled = false;

                m_batteryUI.GetComponent<Image>().enabled = true;
                return Substate.Cooldown;
            }
            break;

            case Substate.Cooldown:
            if((Time.time - m_lightOnTime - m_lightOnDuration) > m_lightCooldown)
            {
                //Draw lightning
                m_batteryLightning.enabled = true;
                return Substate.Ready;
            }
            break;
        }

        return m_substate;
    }
}
