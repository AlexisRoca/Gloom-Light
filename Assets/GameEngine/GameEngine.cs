using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour
{
    public Player m_player;
    public Pad m_pad;

    private void Awake()
    {
        // Get all elements from the scene
    }

    // Use this for initialization
    void Start ()
    {
        m_pad    = new Pad();
        m_player = new Player(m_pad);
    }
	
	// Update is called once per frame
	void Update ()
    {
    }
}