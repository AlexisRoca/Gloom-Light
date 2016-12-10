using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private Vector3 m_pos;
    private Controller m_controller;

    public Player(Controller controller)
    { m_controller = controller; }

    public Vector3 getPosition()
    { return m_pos; }

    public void setPosition(Vector3 pos)
    { m_pos = pos; }
}
