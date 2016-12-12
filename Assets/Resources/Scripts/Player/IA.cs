using UnityEngine;
using System.Collections;
using System;

public class IA : Controller
{
    int m_direction = 0;

    bool m_interact = false;
    bool m_light = false;

    float time_displacement = 3.0f;
    float time_interact = 0.5f;
    float time_Offlight = 5.0f;
    float time_Onlight = 0.5f;

    float timerD = 0.0f;
    float timerI = 0.0f;
    float timerL = 0.0f;

    void Update()
    {
        timerD += Time.deltaTime;
        timerL += Time.deltaTime;

        if (timerD > time_displacement)
        {
            m_direction = (int)UnityEngine.Random.Range(0.0f, 5.0f);
            timerD = 0.0f;
        }

        if(m_interact)
        {
            timerI += Time.deltaTime;
            if (timerD > time_interact)
            {
                m_interact = false;
                timerI = 0.0f;
            }   
        }

        if(m_light)
        {
            if (timerL > time_Onlight)
            {
                m_light = false;

                time_Offlight = UnityEngine.Random.Range(5.0f, 10.0f);
                timerL = 0.0f;
            }
        }
        else
        {
            if (timerL > time_Offlight)
            {
                m_light = true;
                timerL = 0.0f;
            }
        }
    }

    public override Vector3 getDisplacement()
    {
        Vector3 result = Vector3.zero;

        switch (m_direction)
        {
            case 0 :
                result = Vector3.forward;
                break;
            case 1:
                result = Vector3.right;
                break;
            case 2:
                result = -Vector3.forward;
                break;
            case 3:
                result = -Vector3.right;
                break;
            default:
                break;
        }

        return result;
    }

    public override Vector2 getAngleTorchlight()
    {
        return Vector2.zero;
    }

    public override bool getInteractInput()
    {
        return m_interact;
    }

    public override bool getLightInput()
    {
        return m_light;
    }


    void OnCollisionEnter(Collision collision)
    {
        m_interact = true;
    }
}
