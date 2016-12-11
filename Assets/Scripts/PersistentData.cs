using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

    public static bool [] m_activePlayers;
    public static int m_nbActivePlayer;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
