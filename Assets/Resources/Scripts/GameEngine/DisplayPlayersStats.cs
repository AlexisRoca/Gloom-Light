using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DisplayPlayersStats : MonoBehaviour {

    public Text winnerText;

    public Text killText;
    public Text alifeText;
    public Text interractionText;
    public Text lightOnText;
    public Text uselessPressText;

    struct Score
    {
        public float max;
        public int id;
        public Material mat;
    }

    Score kill;
    Score alife;
    Score interraction;
    Score lightOn;
    Score uselessPress;

    Player[] m_player;
    public string [] ColorId;



    // Use this for initialization
    void Start () {
        m_player = PersistentData.m_playersStats;

        getStats();


        winnerText.text = ColorId[alife.id] + "  Win!";
        winnerText.color = alife.mat.color;

        killText.text = "With " + (int)kill.max + " kill";
        killText.color = kill.mat.color;

        alifeText.text = "With " + (int)alife.max + " seconds survive";
        alifeText.color = alife.mat.color;

        if(interraction.max > 0)
        {
            interractionText.text = "With " + (int)interraction.max + " interractions";
            interractionText.color = interraction.mat.color;
        }

        if(lightOn.max > 0)
        {
            lightOnText.text = "With " + (int) lightOn.max + " torch turns on";
            lightOnText.color = lightOn.mat.color;
        }

        if(uselessPress.max > 0)
        {
            uselessPressText.text = "With " + (int) uselessPress.max + " useless press";
            uselessPressText.color = uselessPress.mat.color;
        }

    }

    void Update()
    {
        for (int i = 0; i < m_player.Length; i++)
        {
            if (Input.GetButtonDown("InteractButton_p" + (i + 1).ToString()))
                SceneManager.LoadScene("Start Scene");

            if (Input.GetButtonDown("XButton_p" + (i + 1).ToString()))
                Application.Quit();
        }
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
                kill.mat = m_player[i].m_colorMat;
            }

            if (m_player[i].m_timeAlife > alife.max)
            {
                alife.max = m_player[i].m_timeAlife;
                alife.id = i;
                alife.mat = m_player[i].m_colorMat;
            }

            if (m_player[i].m_nbInterraction > interraction.max)
            {
                interraction.max = m_player[i].m_nbInterraction;
                interraction.id = i;
                interraction.mat = m_player[i].m_colorMat;
            }

            if (m_player[i].m_nbOnLight > lightOn.max)
            {
                lightOn.max = m_player[i].m_nbOnLight;
                lightOn.id = i;
                lightOn.mat = m_player[i].m_colorMat;
            }

            if (m_player[i].m_nbPressUseless > uselessPress.max)
            {
                uselessPress.max = m_player[i].m_nbPressUseless;
                uselessPress.id = i;
                uselessPress.mat = m_player[i].m_colorMat;
            }
        }
    }
}
