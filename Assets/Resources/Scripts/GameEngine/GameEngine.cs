using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    public Player m_prefabPlayer;
    public Canvas PauseCanvas;
    public bool debug;

    private AbstractObject[] m_roomsObjects;
    private Torchlight[] m_torchesPlayer;
    private Player[] m_players;
    private LightManager m_lightManager;
    private bool inPause = false;
    private int m_nbSurvivors = 0;

    enum Substate
    {
        WaitForStart,
        Game,
        WaitForEnd,
        Pause
    };

    private Substate m_substate;

    public float m_waitForStartDuration = 5.0f;
    public float m_waitForEndDuration = 5.0f;
    private float m_sceneOnTime;

    public void Start()
    {
        m_lightManager = new LightManager(Time.time);
        m_lightManager.initLights();

        initPlayers();
        loadScene();

        enablePlayerInteractions(false);

        m_substate = Substate.WaitForStart;
        m_sceneOnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        m_substate = checkChangeSubstate();
        executeSubstate();
    }

    void playerIsDead(Vector3 pos, Material colorMat)
    {
        // Instanciate new SpotLight
        GameObject spotLightObj = new GameObject();
        spotLightObj.transform.position = new Vector3(pos[0], 5.0f, pos[2]);
        Light spotLight = spotLightObj.AddComponent<Light>();
        spotLight.type = LightType.Spot;
        spotLight.spotAngle = 50.0f;
        spotLight.intensity = 8.0f;
        spotLight.color = colorMat.color;
    }

    void loadScene()
    {
        // Get all elements from the scene
        m_roomsObjects = GameObject.FindObjectsOfType(typeof(AbstractObject)) as AbstractObject[];
    }

    void initPlayers()
    {
        if(debug)
        {
            m_players = new Player[2];
            for(int i = 0; i < 2; i++)
            {
                Player player = Instantiate(m_prefabPlayer) as Player;//"Player" + i.ToString()).AddComponent<Player>();
                player.transform.position = new Vector3(0.0f, 0.5f, 2.0f * i);

                GameObject go = GameObject.Find("GUI_Player" + (i + 1).ToString());
                for(int j = 0; j < go.transform.childCount; j++)
                    if(go.transform.GetChild(j).transform.name == "Battery")
                        player.m_torchlight.GetComponent<Torchlight>().m_batteryUI = go.transform.GetChild(j).gameObject;


                Pad pad = new Pad();
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

            int activePlayerIndex = 0;

            for(int i = 0; i < 4; i++)
            {
                bool activePlayer = PersistentData.m_activePlayers[i];

                if(!activePlayer)
                    continue;

                Player player = Instantiate(m_prefabPlayer) as Player;
                player.transform.position = new Vector3(0.0f, 0.5f, 2.0f * i);

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
                        return Substate.WaitForEnd;

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
                int nbSurvivors = 0;
                for(int i = 0; i < m_players.Length; i++)
                {
                    if(m_players[i] != null)
                    {
                        if(m_players[i].m_readyForDead)
                        {
                            playerIsDead(m_players[i].transform.position, m_players[i].m_colorMat);
                            // Destroy(m_players[i].gameObject); // TO DO
                        }

                        m_players[i].updatePlayer();
                        nbSurvivors += 1;
                    }
                }
                break;

            case Substate.Game:
                m_lightManager.update(Time.time);
                m_nbSurvivors = 0;
                for(int i = 0; i < m_players.Length; i++)
                {
                    if(m_players[i] != null)
                    {
                        if(m_players[i].m_readyForDead)
                        {
                            playerIsDead(m_players[i].transform.position, m_players[i].m_colorMat);
                            m_players[i].deadNow();
                            // Destroy(m_players[i].gameObject); // TO DO

                            continue;
                        }

                        m_players[i].updatePlayer();
                        m_nbSurvivors += 1;
                    }
                }
                break;

            case Substate.WaitForEnd:
                m_lightManager.update(Time.time);

                m_nbSurvivors = 0;
                for(int i = 0; i < m_players.Length; i++)
                {
                    if(m_players[i] != null)
                    {
                        if(m_players[i].m_readyForDead)
                        {
                            playerIsDead(m_players[i].transform.position, m_players[i].m_colorMat);
                            Destroy(m_players[i].gameObject); // TO DO
                        }

                        m_players[i].updatePlayer();
                        m_nbSurvivors += 1;
                    }
                }
                break;

            case Substate.Pause:
                break;
        }
    }
}