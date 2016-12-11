using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayPlayersStats : MonoBehaviour {

    public Text killText;
    public Text alifeText;
    public Text interractionText;
    public Text lightOnText;
    public Text uselessPressText;

    struct Score
    {
        public float max;
        public int id;
    }

    Score kill;
    Score alife;
    Score interraction;
    Score lightOn;
    Score uselessPress;

    Player[] m_player;

    // Use this for initialization
    void Start () {
        m_player = PersistentData.m_playersStats;

        getStats();

        killText.text = "Player " + kill.id + " with " + (int)kill.max + " kill";
        alifeText.text = "Player " + alife.id + " with " + (int)alife.max + " seconds survive";
        interractionText.text = "Player " + interraction.id + " with " + (int)interraction.max + " interractions";
        lightOnText.text = "Player " + lightOn.id + " with " + (int)lightOn.max + " torch turns on";
        uselessPressText.text = "Player " + uselessPress.id + " with " + (int)uselessPress.max + " useless press";
    }

    void getStats()
    {
        kill.max = 0.0f;
        kill.id = 0;
        alife.max = 0.0f;
        alife.id = 0;
        interraction.max = 0.0f;
        interraction.id = 0;
        lightOn.max = 0.0f;
        lightOn.id = 0;
        uselessPress.max = 0.0f;
        uselessPress.id = 0;

        for (int i = 0; i < m_player.Length; i++)
        {
            if (m_player[i].m_nbKill > kill.max)
            {
                kill.max = m_player[i].m_nbKill;
                kill.id = i;
            }

            if (m_player[i].m_timeAlife > alife.max)
            {
                alife.max = m_player[i].m_timeAlife;
                alife.id = i;
            }

            if (m_player[i].m_nbInterraction > interraction.max)
            {
                interraction.max = m_player[i].m_nbInterraction;
                interraction.id = i;
            }

            if (m_player[i].m_nbOnLight > lightOn.max)
            {
                lightOn.max = m_player[i].m_nbOnLight;
                lightOn.id = i;
            }

            if (m_player[i].m_nbPressUseless > uselessPress.max)
            {
                uselessPress.max = m_player[i].m_nbPressUseless;
                uselessPress.id = i;
            }
        }
    }
}
