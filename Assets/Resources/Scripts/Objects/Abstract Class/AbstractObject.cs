using UnityEngine;
using System.Collections;

abstract public class AbstractObject : MonoBehaviour
{
    protected Animator m_animator;
    protected AudioSource m_audioSource;

    // Play animation and sound
    abstract protected void Play(Collider collider);
}
