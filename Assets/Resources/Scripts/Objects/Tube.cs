using UnityEngine;
using System.Collections;

public class Tube : AbstractActiveObject
{
    // Attributes
    public GameObject trigger0;
    public GameObject trigger1;
    public Material material;

    // Attributes
    bool m_isActive = false;

    float m_timeDuration = 1.5f;
    float m_timer = 0.0f;

    float r = 0.5f;
    float g = 0.5f;
    float b = 0.5f;

    // Use this for initialization
    void Start()
    {
        // Animator
        m_audioSource = this.gameObject.GetComponent<AudioSource>();

        // Object disable
        m_isActive = false;
    }

    void Update()
    {
        if (m_isActive)
        {
            m_timer += Time.deltaTime;

            r += (Random.value - 0.5f) * 0.1f;
            g += (Random.value - 0.5f) * 0.1f;
            b += (Random.value - 0.5f) * 0.1f;
            material.SetColor("_EmissionColor", new Color(r, g, b));

            if (m_timer > m_timeDuration)
                Play(null);
        }
    }

    // Play animation and sound
    override protected void Play(Collider collider)
    {
        if(collider != null)
        {
            GameObject player = collider.gameObject;

            float dist1 = Vector3.Distance(trigger0.transform.position, player.transform.position);
            float dist2 = Vector3.Distance(trigger1.transform.position, player.transform.position);

            if(dist1<dist2)
            {
                player.transform.position = new Vector3(trigger1.transform.position.x, player.transform.position.y, trigger1.transform.position.z);
            }
            else
            {
                player.transform.position = new Vector3(trigger0.transform.position.x, player.transform.position.y, trigger0.transform.position.z);
            }
        }

        if (m_isActive)
        {
            m_audioSource.Stop();
            m_audioSource.enabled = false;

            m_timer = 0.0f;
            material.SetColor("_EmissionColor", new Color(0.0f,0.0f,0.0f));
            m_isActive = false;
        }
        else
        {
            m_audioSource.enabled = true;
            m_audioSource.Play();
            
            m_isActive = true;
        }
    }
}
