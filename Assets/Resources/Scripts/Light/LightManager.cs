using UnityEngine;
using System.Collections;
using EZCameraShake;

public class LightManager
{
    enum Substate
    {
        Start,
        Lightning,
        Off
    }

    private Substate m_substate;

    private Light [] m_roomLights;
    private Light [] m_windowsLights;

    private float m_startStateTime;
    private float m_roomLightsOnDuration = 5.0f;
    private float m_roomLightsIntensity = 4.0f;

    private int m_lightningNumber;
    private int m_currentLightningNumber;
    private float m_timeSinceLastLightning;
    private float m_lightningCooldown = 10.0f;
    private float m_lightningDuration = 0.3f;
    private float m_lightningIntensity = 3.5f;
    private bool m_lightningOn;

    private float Magnitude = 2.5f;
    private float Roughness = 13.5f;
    private float FadeInTime = 0.5f;
    private float FadeOutTime = 1.0f;

    private AudioSource LightningSound;

    public LightManager(float gameTime)
    {
        m_startStateTime = gameTime;
        m_substate = Substate.Start;

        LightningSound = GameObject.Find("FlashLights").GetComponentInChildren<AudioSource>();
    }

    public void initLights()
    {
        // Room lights
        GameObject[] roomLightsGO = GameObject.FindGameObjectsWithTag("RoomLight");
        m_roomLights = new Light[roomLightsGO.Length];

        for(int i = 0; i < roomLightsGO.Length; i++)
            m_roomLights.SetValue(roomLightsGO[i].GetComponent<Light>(),i);


        // Windows lights
        GameObject[] windowsLightsGO = GameObject.FindGameObjectsWithTag("WindowLight");
        m_windowsLights = new Light[windowsLightsGO.Length];

        for(int i = 0; i < windowsLightsGO.Length; i++)
            m_windowsLights.SetValue(windowsLightsGO[i].GetComponent<Light>(),i);
    }

    public void update(float gameTime)
    {
        m_substate = checkChangeState(gameTime);

        updateSubstate(gameTime);
    }

    Substate checkChangeState(float gameTime)
    {
        switch(m_substate)
        {
            case Substate.Start:
            if((gameTime-m_startStateTime) > m_roomLightsOnDuration)
            {
                m_startStateTime = gameTime;
                return Substate.Off;
            }
            break;

            case Substate.Lightning:
            if((m_currentLightningNumber == m_lightningNumber) && !m_lightningOn)
                return Substate.Off;
            break;

            case Substate.Off:
            if((gameTime-m_timeSinceLastLightning) > m_lightningCooldown)
            {
                m_lightningNumber = Random.Range(1,4);
                m_currentLightningNumber = 0;
                m_lightningOn = false;

                return Substate.Lightning;
            }
            break;
        }

        return m_substate;
    }

    void updateSubstate(float gameTime)
    {
        switch(m_substate)
        {
            case Substate.Start:
               for(int i=0; i<m_roomLights.Length; i++)
                    m_roomLights[i].intensity = Mathf.Lerp(m_roomLightsIntensity,0.0f,(gameTime - m_startStateTime) / m_roomLightsOnDuration); ;
            break;

            case Substate.Lightning:
            if(m_lightningOn)
            {
                if((gameTime-m_timeSinceLastLightning)>m_lightningDuration)
                {
                    m_lightningOn = false;
                    for(int i = 0; i < m_windowsLights.Length; i++)
                        m_windowsLights[i].intensity = 0.0f;
                }
            }

            else
            {
                float randomNumber = Random.value;
                if(randomNumber > 0.9f)
                {
                    m_lightningOn = true;
                    m_timeSinceLastLightning = gameTime;
                    m_currentLightningNumber++;

                    LightningSound.Play();
                    CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, FadeInTime, FadeOutTime);
                    
                    for (int i = 0; i < m_windowsLights.Length; i++)
                        m_windowsLights[i].intensity = m_lightningIntensity;
                }
            }
            break;

            case Substate.Off:
            break;
        }
    }
}