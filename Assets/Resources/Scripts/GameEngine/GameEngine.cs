using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    public Player m_prefabPlayer;
    public Canvas PauseCanvas;
    public bool debug;

    private AbstractObjects[] m_roomsObjects;
    private Player[] m_players;
    private LightManager m_lightManager;
    private bool inPause = false;

    public void Start()
    {
        loadScene();

        m_lightManager = new LightManager(Time.time);
        m_lightManager.initLights();

        initPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Start"))
        {
            inPause = !inPause;
            PauseCanvas.enabled = !PauseCanvas.enabled;
        }


        if(!inPause)
        {
            m_lightManager.update(Time.time);

            int nbSurvivors = 0;
            for(int i = 0; i < m_players.Length; i++)
            {
                if (m_players[i] != null)
                {
                    if(m_players[i].m_readyForDead)
                        Destroy(m_players[i].gameObject);

                    m_players[i].updatePlayer();
                    nbSurvivors += 1;
                }
            }
            
            if(!debug)
                if(nbSurvivors == 1)
                    endGame();
        }
        else
        {
            // Exit Game Condition
            for(int i = 0; i < m_players.Length; i++)
            {
                if (m_players[i].m_controller.getExitInput())
                    Application.Quit();
            }
        }
    }

    void loadScene()
    {
        // Get all elements from the scene
        m_roomsObjects = GameObject.FindObjectsOfType<AbstractObjects>();
    }

    void initPlayers()
    {
        if(debug)
        {
            m_players = new Player[2];
            for (int i = 0; i < 2; i++)
            {
                Player player = Instantiate(m_prefabPlayer) as Player;//"Player" + i.ToString()).AddComponent<Player>();
                player.transform.position = new Vector3(0.0f, 0.5f, 2.0f * i);

                GameObject go = GameObject.Find("GUI_Player" + (i+1).ToString());
                for (int j = 0; j < go.transform.childCount; j++)
                    if(go.transform.GetChild(j).transform.name == "Battery")
                        player.m_torchlight.GetComponent<Torchlight>().m_batteryUI = go.transform.GetChild(j).gameObject;


                Pad pad = new Pad();
                pad.joystickNumber = i + 1;
                player.m_controller = pad;

                player.setColor((Material) Resources.Load("Materials/Player/Player" + (i+1).ToString()));

                m_players[i] = player;
            }
        }

        else
        {
            int gamepadNb = PersistentData.m_nbActivePlayer;
            m_players = new Player[gamepadNb];

            int activePlayerIndex = 0; 

            for (int i = 0; i < 4; i++)
            {
                bool activePlayer = PersistentData.m_activePlayers[i];

                if (!activePlayer)
                    continue;

                Player player = Instantiate(m_prefabPlayer) as Player;
                player.transform.position = new Vector3(0.0f, 0.5f, 2.0f*i);

                GameObject go = GameObject.Find("GUI_Player" + (i+1).ToString());
                for(int j=0; j < go.transform.childCount; j++)
                    if(go.transform.GetChild(j).transform.name == "Battery")
                        player.m_torchlight.GetComponent<Torchlight>().m_batteryUI = go.transform.GetChild(j).gameObject;


                Pad pad = new Pad();
                pad.joystickNumber = i + 1;
                player.m_controller = pad;
            
                player.setColor((Material) Resources.Load("Materials/Player/Player" + (i+1).ToString()));

                m_players[activePlayerIndex] = player;
                activePlayerIndex++;
            }
        }
    }


    void endGame()
    {
        PersistentData.m_playersStats = m_players;
        SceneManager.LoadScene("Final Scene");
    }
}