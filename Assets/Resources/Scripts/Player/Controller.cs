using UnityEngine;
using System.Collections;

abstract public class Controller : MonoBehaviour {

    protected Vector3 movementVector;
    protected Vector2 aimVector;
    protected float movementSpeed = 10.0f;

    abstract public Vector3 getDisplacement();
    abstract public Vector2 getAngleTorchlight();
    abstract public bool getInteractInput();
    abstract public bool getLightInput();
}
