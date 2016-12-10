using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour
{
    private AbstractObjects [] m_roomsObjects;
    private Player [] m_players;

    private Light [] m_roomLights;
    private Light [] m_windowsLight;

    private void Awake()
    {
        loadScene();
        initPlayers();
    }

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    void loadScene()
    {
        // Get all elements from the scene
        m_roomsObjects = GameObject.FindObjectsOfType<AbstractObjects>();

        initLights();
    }

    void initLights()
    {
        GameObject[] roomLightsGO = GameObject.FindGameObjectsWithTag("RoomLight");
        m_roomLights = new Light[roomLightsGO.Length];

        for(int i = 0; i < roomLightsGO.Length; i++)
            m_roomLights.SetValue(roomLightsGO[i].GetComponent<Light>(),i);
    }

    void initPlayers()
    {
        int gamepadNb = Input.GetJoystickNames().Length;
        m_players = new Player[gamepadNb];

        for(int i=0; i<gamepadNb; i++)
        {
            //m_players[i].
        }
    }    
}