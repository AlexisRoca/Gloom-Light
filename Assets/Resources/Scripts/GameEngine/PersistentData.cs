using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

    public static bool [] m_activePlayers;
    public static int m_nbActivePlayer;
    public static Player[] m_playersStats;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
