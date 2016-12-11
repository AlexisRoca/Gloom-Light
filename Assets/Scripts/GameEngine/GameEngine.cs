using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour
{
    private AbstractObjects [] m_roomsObjects;
    private Player [] m_players;
    private LightManager m_lightManager;

    public Canvas PauseCanvas;

    public Player m_prefabPlayer;

    public bool inPause = false;

    public void Start()
    {
        loadScene();

        m_lightManager = new LightManager(Time.time);
        m_lightManager.initLights();

        initPlayers();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(! inPause)
        {
            m_lightManager.update(Time.time);
            for (int i = 0; i < m_players.Length; i++)
                m_players[i].updatePlayer();

            PauseCanvas.enabled = false;
        }
        else
        {
            PauseCanvas.enabled = true;

            // Exit Game Condition
            for (int i = 0; i < m_players.Length; i++)
                if (m_players[i].m_controller.getExitInput())
                    Application.Quit();
        }

        // Pause gestion
        for (int i = 0; i < m_players.Length; i++)
            if (m_players[i].m_controller.getPauseInput())
                inPause = !inPause;
    }


    void loadScene()
    {
        // Get all elements from the scene
        m_roomsObjects = GameObject.FindObjectsOfType<AbstractObjects>();
    }

    void initPlayers()
    {
        int gamepadNb = PersistentData.m_nbActivePlayer;
        m_players = new Player[PersistentData.m_nbActivePlayer];
        Debug.Log("NB JOUEURS : " + gamepadNb);

        int activePlayerIndex = 0; 

        for (int i = 0; i < 4; i++)
        {
            bool activePlayer = PersistentData.m_activePlayers[i];

            if (!activePlayer)
                continue;


            Player player = Instantiate(m_prefabPlayer) as Player;//"Player" + i.ToString()).AddComponent<Player>();
            m_players[activePlayerIndex] = player;
            m_players[activePlayerIndex].transform.position += m_players[activePlayerIndex].transform.forward * activePlayerIndex * 2;

            Pad pad = new Pad();
            pad.joystickNumber = i + 1;
            player.m_controller = pad;

            activePlayerIndex++;
        }
    }
}