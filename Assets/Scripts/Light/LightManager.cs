using UnityEngine;
using System.Collections;

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

    private float m_startTime;
    private float m_fadeInStart = 1.0f;
    private float m_roomLightsOnDuration = 5;

    public LightManager(float gameTime)
    {
        m_startTime = gameTime;
        m_substate = Substate.Start;
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
            if(gameTime > m_startTime + m_roomLightsOnDuration)
                return Substate.Off;
            break;

            case Substate.Lightning:
            break;

            case Substate.Off:
            break;
        }

        return m_substate;
    }

    void updateSubstate(float gameTime)
    {
        switch(m_substate)
        {
            case Substate.Start:
               float intensity = Mathf.Lerp(m_fadeInStart,0.0f,(gameTime-m_startTime) / m_roomLightsOnDuration);

               for(int i=0; i<m_roomLights.Length; i++)
                    m_roomLights[i].intensity = intensity;
            break;

            case Substate.Lightning:
            break;

            case Substate.Off:
            break;
        }
    }
}