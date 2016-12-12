using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [HideInInspector] public Controller m_controller;
    public GameObject m_torchlight;

    public bool m_readyForDead = false;
    public Material m_colorMat;
    public bool m_enableInteractions;

    public int m_nbOnLight = 0;
    public int m_nbPressUseless = 0;
    public int m_nbInterraction = 0;
    public int m_nbKill = 0;
    public float m_timeAlife = 0.0f;

    public GameObject m_torchlight;
    public AudioSource m_torchSound;
    private Quaternion m_prevLightOrientation;
    protected CharacterController m_characterController;

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
        if(m_enableInteractions)
        {
        if(m_controller.getLightInput())
        {
                if(!m_torchlight.GetComponent<Torchlight>().setOn())
                m_nbPressUseless += 1;
            else
            {
                m_torchSound.Play();
                m_torchlight.GetComponent<Light>().enabled = true;
        }
    }
    }

    public void updatePlayerWithoutLight()
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
        else if(displacementVector.x != 0.0f || displacementVector.z != 0.0f)
        {
            float lightAngle = Mathf.Atan2(-displacementVector.z, displacementVector.x) * Mathf.Rad2Deg + 90.0f;
            this.transform.localEulerAngles = new Vector3(0.0f, lightAngle, 0.0f);

            m_prevLightOrientation = m_torchlight.transform.rotation;
        }
        else
        {
            m_torchlight.transform.rotation = m_prevLightOrientation;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Torch" && collider.GetComponentInParent<Light>().GetComponentInParent<Player>().getLightOn())
        {
            // Try if there is no obstable between both players
            RaycastHit hit;
            GameObject player = collider.transform.parent.parent.parent.gameObject;

            Vector3 origin = this.transform.position;
            Vector3 direction = (player.transform.position - this.transform.position).normalized;

            if (Physics.Raycast(origin, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
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
        m_colorMat = newMaterial;
        this.GetComponentInChildren<Light>().GetComponentInChildren<MeshRenderer>().material = m_colorMat;
    }

    public bool getInteract()
    { return m_enableInteractions && m_controller.getInteractInput(); }
    public bool getLightOn()
    { return m_torchlight.GetComponent<Light>().enabled; }
}