using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour
{
    private AbstractObjects [] m_roomsObjects;
    private Player [] m_players;
    private LightManager m_lightManager;

    public Player m_prefabPlayer;


    private void Awake()
    {
        loadScene();

        m_lightManager = new LightManager(Time.time);
        m_lightManager.initLights();

        initPlayers();
    }

    // Use this for initialization
    void Start ()
    {
  
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_lightManager.update(Time.time);
    }


    void loadScene()
    {
        // Get all elements from the scene
        m_roomsObjects = GameObject.FindObjectsOfType<AbstractObjects>();
    }

    void initPlayers()
    {
        int gamepadNb = Input.GetJoystickNames().Length;
        m_players = new Player[gamepadNb];

        for(int i=0; i<gamepadNb; i++)
        {
            Player player = m_players[i];
            player = Instantiate(m_prefabPlayer) as Player;//"Player" + i.ToString()).AddComponent<Player>();

            Pad pad = new Pad();
            pad.joystickNumber = i+1;
            player.m_controller = pad;
        }
    }    
}