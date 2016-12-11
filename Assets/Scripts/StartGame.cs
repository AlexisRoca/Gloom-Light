using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string nextScene;
    public Light[] m_torchLights;
    private bool[] m_boolPlayers;


    // Use this for initialization
    void Start ()
    {
        m_boolPlayers = new bool[4];
        for (int i = 0; i<4; i++)
        {
            m_boolPlayers[i] = false;
            m_torchLights[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Exit Game Condition
        for (int i = 0; i < 4; i++)
        {
            if(Input.GetButtonDown("InteractButton_p" + (i+1).ToString()))
            {
                m_torchLights[i].enabled = !m_torchLights[i].enabled;
                m_boolPlayers[i] = !m_boolPlayers[i];
            }

            if (Input.GetButtonDown("ExitButton_p" + (i+1).ToString()))
                startGame();
        }      
    }

    void startGame()
    {
        PersistentData.m_activePlayers = new bool[4];
        PersistentData.m_nbActivePlayer = 0;

        for (int i = 0; i < m_boolPlayers.Length; i++)
        {
            PersistentData.m_activePlayers[i] = m_boolPlayers[i];

            if(m_boolPlayers[i])
                PersistentData.m_nbActivePlayer++;
        }

        SceneManager.LoadScene(nextScene);
    }
}
