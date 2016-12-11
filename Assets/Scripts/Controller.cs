using UnityEngine;
using System.Collections;

abstract public class Controller {

    protected Vector3 movementVector;
    protected Vector2 aimVector;
    protected float movementSpeed = 8;

    abstract public Vector3 getDisplacement();
    abstract public Vector2 getAngleTorchlight();
    abstract public bool getInteractInput();
    abstract public bool getLightInput();
    abstract public bool getPauseInput();
    abstract public bool getExitInput();
}
