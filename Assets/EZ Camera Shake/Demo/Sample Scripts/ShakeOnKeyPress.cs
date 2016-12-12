using UnityEngine;
using EZCameraShake;

public class ShakeOnKeyPress : MonoBehaviour
{
    public float Magnitude = 2.5f;
    public float Roughness = 13.5f;
    public float FadeInTime = 0.5f;
    public float FadeOutTime = 1.0f;

	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.LeftShift))
            CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0.5f, FadeOutTime);
	}
}
