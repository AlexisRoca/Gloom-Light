using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    protected CharacterController m_characterController;
    [HideInInspector] public Controller m_controller;

    private bool m_interact;
    public bool m_readyForDead = false;
    private Quaternion m_prevLightOrientation;

    public int m_nbOnLight = 0;
    public int m_nbPressUseless = 0;
    public int m_nbInterraction = 0;
    public int m_nbKill = 0;
    public float m_timeAlife = 0.0f;

    public GameObject m_torchlight;

    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            if (this.transform.GetChild(i).transform.name == "Torch")
                m_torchlight = this.transform.GetChild(i).gameObject;
    }

    // Start & Update functions
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    public void updatePlayer()
    {
        // Increase Time Alife
        m_timeAlife += Time.deltaTime;

        // Get angle of right and left stick
        Vector3 displacementVector = m_controller.getDisplacement();
        Vector2 aimVector = m_controller.getAngleTorchlight();

        // Move character
        m_characterController.Move(displacementVector * Time.deltaTime);

        // Aim
        if(aimVector.x != 0.0f || aimVector.y != 0.0f)
        {
            float lightAngle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90.0f;
            this.transform.localEulerAngles = new Vector3(0.0f, lightAngle, 0.0f);

            m_prevLightOrientation = m_torchlight.transform.rotation;
        }
        else if (displacementVector.x != 0.0f || displacementVector.z != 0.0f)
        {
            float lightAngle = Mathf.Atan2(-displacementVector.z, displacementVector.x) * Mathf.Rad2Deg + 90.0f;
            this.transform.localEulerAngles = new Vector3(0.0f, lightAngle, 0.0f);

            m_prevLightOrientation = m_torchlight.transform.rotation;
        }
        else
        {
            m_torchlight.transform.rotation = m_prevLightOrientation;
        }

        // Get interact input
        m_interact = m_controller.getInteractInput();


        if(m_controller.getLightInput())
        {
            if(!m_torchlight.GetComponent<Torchlight>().setOn())
                m_nbPressUseless += 1;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Torch" && this.getLightOn())
        {
            // Try if there is no obstable between both players
            RaycastHit hit;
            Vector3 origin = this.transform.position;
            Vector3 direction = (collider.transform.position - this.transform.position).normalized;

            if (Physics.Raycast(origin, direction, out hit))
            {
                if (hit.collider.gameObject == collider.gameObject)
                {
                    this.m_nbKill += 1;
                    this.m_readyForDead = true;
                }
            }
        }
    }

    // Class functions
    public Player(Controller controller)
    {
        m_controller = controller;
    }

    public void setColor(Material newMaterial)
    {
        this.GetComponentInChildren<Light>().GetComponentInChildren<MeshRenderer>().material = newMaterial;
    }

    public bool getInteract()
    { return m_interact; }
    public bool getLightOn()
    { return m_torchlight.GetComponent<Light>().enabled; }
}