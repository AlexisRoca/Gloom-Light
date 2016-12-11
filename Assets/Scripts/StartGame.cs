using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    private Player[] m_players;
    private Player[] m_activePlayers;
    public Player m_prefabPlayer;

    public Light[] m_torchLights;

    private bool[] m_boolPlayers;
    private int m_nbGamers;
    

    // Use this for initialization
    void Start () {
        initPlayers();
        m_nbGamers = 0;

        for(int i = 0; i< m_torchLights.Length; i++)
        {
            m_torchLights[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Exit Game Condition
        for (int i = 0; i < m_players.Length; i++)
        {
            if(Input.GetButtonDown("InteractButton_p" + (i+1).ToString()))
            {
                m_torchLights[i].enabled = !m_torchLights[i].enabled;
                m_boolPlayers[i] = true;
            }

            //if (m_players[i].m_controller.getInteractInput() && !m_boolPlayers[i])
            //{
            //    m_torchLights[m_nbGamers].enabled = true;
            //    m_nbGamers++;
            //}
            if (m_players[i].m_controller.getExitInput())
            {
                startGame();
            }
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

        SceneManager.LoadScene("Scene Martin");
    }

    void initPlayers()
    {
        int gamepadNb = 4;
        m_players = new Player[gamepadNb];
        m_boolPlayers = new bool[gamepadNb];

        for (int i = 0; i < gamepadNb; i++)
        {
            m_boolPlayers[i] = false;

            Player player = Instantiate(m_prefabPlayer) as Player;//"Player" + i.ToString()).AddComponent<Player>();
            m_players[i] = player;

            Pad pad = new Pad();
            pad.joystickNumber = i + 1;
            player.m_controller = pad;
        }
    }
}
