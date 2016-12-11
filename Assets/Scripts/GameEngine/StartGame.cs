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


    // Use this for initialization
    void Start ()
    {
        m_boolPlayers = new bool[4];
        m_torchLights = GameObject.FindGameObjectsWithTag("Torch");

        for (int i = 0; i<4; i++)
        {
            m_boolPlayers[i] = false;
            m_torchLights[i].GetComponentInChildren<Light>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        bool canStart = false;

        // Exit Game Condition
        for (int i = 0; i < 4; i++)
        {
            if(Input.GetButtonDown("InteractButton_p" + (i+1).ToString()))
            {
                m_torchLights[i].GetComponentInChildren<Light>().enabled = !m_torchLights[i].GetComponentInChildren<Light>().enabled;
                m_torchLights[i].GetComponent<Animator>().Play("Press");

                m_boolPlayers[i] = !m_boolPlayers[i];
            }

            if (Input.GetButtonDown("ExitButton_p" + (i+1).ToString()))
                startGame();

            if (m_boolPlayers[i]) canStart = true;
        }

        startText.color = (canStart) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.0f);
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
