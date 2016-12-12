using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Commands : MonoBehaviour {

    //Player[] m_player;

    // Use this for initialization
    void Start () {
        //m_player = PersistentData.m_playersStats;
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 4; i++)
            if (Input.GetButtonDown("XButton_p" + (i + 1).ToString()))
                SceneManager.LoadScene("Start Scene");
    }
}
