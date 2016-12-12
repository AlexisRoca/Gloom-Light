using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    enum Substate
    {
        WaitForStart,
        Game,
        WaitForEnd,
        Pause
    };

    public Player m_prefabPlayer;
    public Canvas PauseCanvas;
    public bool debug;

    private AbstractObject[] m_roomsObjects;
    private Player[] m_players;
    private GameObject[] m_deadSpotLight;
    private GameObject[] m_spawners;
    private LightManager m_lightManager;
    private int m_nbSurvivors = 0;

    private Substate m_substate;

    public float m_waitForStartDuration = 5.0f;
    public float m_waitForEndDuration = 3.0f;
    private float m_sceneOnTime;

    public void Start()
    {
        m_lightManager = new LightManager(Time.time);
        m_lightManager.initLights();

        loadScene();
        initPlayers();

        enablePlayerInteractions(false);

        m_substate = Substate.WaitForStart;
        m_sceneOnTime = Time.time;

        m_nbSurvivors = m_players.Length;
    }

    // Update is called once per frame
    void Update()
    {
        m_substate = checkChangeSubstate();
        executeSubstate();
    }

    void playerIsDead(Transform transform, Material colorMat)
    {
        // Instanciate new SpotLight
        m_deadSpotLight[m_players.Length - m_nbSurvivors] = new GameObject();
        GameObject spotLightObj = m_deadSpotLight[m_players.Length - m_nbSurvivors];

        Instantiate(spotLightObj);
        spotLightObj.transform.position = new Vector3(transform.position[0], 5.0f, transform.position[2]);
        spotLightObj.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        Light pointLight = spotLightObj.AddComponent<Light>();
        pointLight.type = LightType.Spot;
        pointLight.spotAngle = 100.0f;
        pointLight.intensity = 20.0f;
        pointLight.color = colorMat.color;
    }

    void loadScene()
    {
        // Get all elements from the scene
        m_roomsObjects = GameObject.FindObjectsOfType(typeof(AbstractObject)) as AbstractObject[];
        m_spawners = GameObject.FindGameObjectsWithTag("Spawn");
    }

    void initPlayers()
    {
        if(debug)
        {
            m_players = new Player[2];
            m_deadSpotLight = new GameObject[1];

            for(int i = 0; i < 2; i++)
            {
                Player player = Instantiate(m_prefabPlayer) as Player;//"Player" + i.ToString()).AddComponent<Player>();
                player.transform.position = m_spawners[i].transform.position;

                GameObject go = GameObject.Find("GUI_Player" + (i + 1).ToString());
                for(int j = 0; j < go.transform.childCount; j++)
                    if(go.transform.GetChild(j).transform.name == "Battery")
                        player.m_torchlight.GetComponent<Torchlight>().m_batteryUI = go.transform.GetChild(j).gameObject;


                Pad pad = new Pad();
                //IA pad = new IA();

                pad.joystickNumber = i + 1;
                player.m_controller = pad;
                player.m_enableInteractions = false;

                player.setColor((Material)Resources.Load("Materials/Player/Player" + (i + 1).ToString()));

                m_players[i] = player;
            }
        }

        else
        {
            int gamepadNb = PersistentData.m_nbActivePlayer;
            m_players = new Player[gamepadNb];
            m_deadSpotLight = new GameObject[gamepadNb-1];

            int activePlayerIndex = 0;

            for(int i = 0; i < 4; i++)
            {
                bool activePlayer = PersistentData.m_activePlayers[i];

                if(!activePlayer)
                    continue;

                Player player = Instantiate(m_prefabPlayer) as Player;
                player.transform.position = m_spawners[i].transform.position;

                GameObject go = GameObject.Find("GUI_Player" + (i + 1).ToString());
                for(int j = 0; j < go.transform.childCount; j++)
                    if(go.transform.GetChild(j).transform.name == "Battery")
                        player.m_torchlight.GetComponent<Torchlight>().m_batteryUI = go.transform.GetChild(j).gameObject;


                Pad pad = new Pad();
                pad.joystickNumber = i + 1;
                player.m_controller = pad;

                player.setColor((Material)Resources.Load("Materials/Player/Player" + (i + 1).ToString()));
                player.m_enableInteractions = false;

                m_players[activePlayerIndex] = player;
                activePlayerIndex++;
            }
        }
    }

    void enablePlayerInteractions(bool enabled)
    {
        for(int i = 0; i < m_players.Length; i++)
            m_players[i].m_enableInteractions = enabled;
    }

    void endGame()
    {
        PersistentData.m_playersStats = m_players;
        SceneManager.LoadScene("Final Scene");
    }

    Substate checkChangeSubstate()
    {
        switch(m_substate)
        {
            case Substate.WaitForStart:
                if((Time.time - m_sceneOnTime) > m_waitForStartDuration)
                {
                    enablePlayerInteractions(true);
                    return Substate.Game;
                }
                break;

            case Substate.Game:
                if(!debug)
                    if(m_nbSurvivors == 1)
                    {
                        m_sceneOnTime = Time.time;
                        return Substate.WaitForEnd;
                    }

                if(Input.GetButtonDown("Start"))
                {
                    PauseCanvas.enabled = true;
                    return Substate.Pause;
                }
                break;

            case Substate.WaitForEnd:
                if((Time.time - m_sceneOnTime) > m_waitForEndDuration)
                    endGame();
                break;

            case Substate.Pause:
                if(Input.GetButtonDown("Start"))
                {
                    PauseCanvas.enabled = false;
                    return Substate.Game;
                }

                if(Input.GetButtonDown("Exit"))
                    Application.Quit();
                break;
        }

        return m_substate;
    }

    void executeSubstate()
    {
        switch(m_substate)
        {
            case Substate.WaitForStart:
                m_lightManager.update(Time.time);
                for(int i = 0; i < m_players.Length; i++)
                    if(m_players[i] != null)
                        m_players[i].updatePlayer();
                break;

            case Substate.Game:
                m_lightManager.update(Time.time);
                for(int i = 0; i < m_players.Length; i++)
                {
                    if((m_players[i] != null) && !m_players[i].m_isDead)
                    {
                        if(m_players[i].m_waitForDying)
                        {
                            playerIsDead(m_players[i].transform, m_players[i].m_colorMat);
                            m_players[i].deadNow();
                            m_players[i].m_waitForDying = false;
                            m_nbSurvivors--;

                            continue;
                        }

                        m_players[i].updatePlayer();
                    }
                }
                break;

            case Substate.WaitForEnd:
                m_lightManager.update(Time.time);

                for(int i = 0; i < m_players.Length; i++)
                    if(m_players[i] != null)
                        m_players[i].updatePlayer();
                break;

            case Substate.Pause:
                break;
        }
    }
}