using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string nextScene;
    public Text startText;

    private GameObject[] m_torchLights;
    private bool[] m_boolPlayers;

    private int m_nbActivePlayer;

    // Use this for initialization
    void Start ()
    {
        m_boolPlayers = new bool[4];
        m_torchLights = new GameObject[4];

        for (int i = 0; i<4; i++)
        {
            m_boolPlayers[i] = false;
            m_torchLights[i] = GameObject.Find("Torch_" + (i+1).ToString());
            m_torchLights[i].GetComponentInChildren<Light>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < 4; i++)
        {
            if(Input.GetButtonDown("InteractButton_p" + (i+1).ToString()))
            {
                m_torchLights[i].GetComponentInChildren<Light>().enabled = !m_torchLights[i].GetComponentInChildren<Light>().enabled;
                m_torchLights[i].GetComponent<Animator>().Play("Press");

                m_boolPlayers[i] = !m_boolPlayers[i];

                if(m_boolPlayers[i])
                    m_nbActivePlayer++;
                else
                    m_nbActivePlayer--;
            }

            if (Input.GetButtonDown("XButton_p" + (i + 1).ToString()))
            {
                SceneManager.LoadScene("Commands Scene");
            }
        }

        if(m_nbActivePlayer > 1)
        {
            if(startText.color != new Color(1.0f,1.0f,1.0f,1.0f))
                startText.color = new Color(1.0f,1.0f,1.0f,1.0f);

            if(Input.GetButtonDown("Start"))
                startGame();
        }

        else
        {
            if(startText.color != new Color(1.0f,1.0f,1.0f,0.0f))
                startText.color = new Color(1.0f,1.0f,1.0f,0.0f);
        }
    }

    void startGame()
    {
        PersistentData.m_nbActivePlayer = m_nbActivePlayer;

        PersistentData.m_activePlayers = new bool[4];
        m_boolPlayers.CopyTo(PersistentData.m_activePlayers,0);

        SceneManager.LoadScene(nextScene);
    }
}