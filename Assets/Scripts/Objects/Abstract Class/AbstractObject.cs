using UnityEngine;
using System.Collections;

abstract public class AbstractObjects : MonoBehaviour
{

    public Animator m_animator;
    public AudioSource m_audioSource;

    // Play animation and sound
    abstract protected void Play();

}
