using UnityEngine;
using System.Collections;

abstract public class Controller {

    protected Vector3 movementVector;
    protected Vector3 aimVector;
    protected float movementSpeed = 8;

    abstract public Vector3 getDisplacement();
    abstract public float getAngleTorchlight();
    abstract public bool getInteractInput();
    abstract public bool getLightInput();
}
