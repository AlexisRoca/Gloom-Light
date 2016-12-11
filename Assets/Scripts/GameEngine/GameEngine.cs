using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    private AbstractObjects[] m_roomsObjects;
    private Player[] m_players;
    private LightManager m_lightManager;

    public Canvas PauseCanvas;

    public Player m_prefabPlayer;

    public bool inPause = false;

    public Material m_material1;
    public Material m_material2;
    public Material m_material3;
    public Material m_material4;

    public bool debug;

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

            int nbSurvivors = 0;
            for (int i = 0; i < m_players.Length; i++)
            {
                if(m_players[i] != null)
                {
                    m_players[i].updatePlayer();
                    nbSurvivors += 1;
                }
            }

            if(nbSurvivors == 1)
            {
                endGame();
            }
  
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
        if(debug)
        {
            for (int i = 0; i < 2; i++)
            {
                Player player = Instantiate(m_prefabPlayer) as Player;//"Player" + i.ToString()).AddComponent<Player>();
                player.transform.position = new Vector3(0.0f, 0.5f, 2.0f * i);

                GameObject go = GameObject.Find("GUI_Player" + (i + 1).ToString());
                for (int j = 0; j < go.transform.childCount - 1; j++)
                    if (go.transform.GetChild(j).transform.name == "Battery")
                        player.m_batteryUI = go.transform.GetChild(j).gameObject;


                Pad pad = new Pad();
                pad.joystickNumber = i + 1;
                player.m_controller = pad;
            }
        }

        else
        {
            int gamepadNb = PersistentData.m_nbActivePlayer;
            m_players = new Player[gamepadNb];
            Debug.Log("NB JOUEURS : " + gamepadNb);

            int activePlayerIndex = 0; 

            for (int i = 0; i < 4; i++)
            {
                bool activePlayer = PersistentData.m_activePlayers[i];

                if (!activePlayer)
                    continue;

                Player player = Instantiate(m_prefabPlayer) as Player;//"Player" + i.ToString()).AddComponent<Player>();
                player.transform.position = new Vector3(0.0f, 0.5f, 2.0f*i);

                GameObject go = GameObject.Find("GUI_Player" + (i+1).ToString());
                for(int j=0; j < go.transform.childCount - 1; j++)
                    if(go.transform.GetChild(j).transform.name == "Battery")
                        player.m_batteryUI = go.transform.GetChild(j).gameObject;
              

                Pad pad = new Pad();
                pad.joystickNumber = i + 1;
                player.m_controller = pad;
            
                switch(i)
                {
                    case 0:
                        player.setColor(m_material1);
                        break;
                    case 1:
                        player.setColor(m_material2);
                        break;
                    case 2:
                        player.setColor(m_material3);
                        break;
                    case 3:
                        player.setColor(m_material4);
                        break;
                    default:
                        break;
                }

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