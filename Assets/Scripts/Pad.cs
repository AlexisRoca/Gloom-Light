using UnityEngine;
using System.Collections;

public class Pad : Controller {

    public int joystickNumber = 1;
    string joystickString;

    void Update()
    {
        joystickString = joystickNumber.ToString();
    }

    public override Vector3 getDisplacement()
    {
        joystickString = joystickNumber.ToString();

        movementVector.x = Input.GetAxis("LeftJoystickX_p" + joystickString) * movementSpeed;
        movementVector.z = Input.GetAxis("LeftJoystickY_p" + joystickString) * movementSpeed;

        return movementVector;
    }

    public override float getAngleTorchlight()
    {
        joystickString = joystickNumber.ToString();

        movementVector.x = Input.GetAxis("LeftJoystickX_p" + joystickString);
        movementVector.z = Input.GetAxis("LeftJoystickY_p" + joystickString);

        aimVector.x = Input.GetAxis("RightJoystickX_p" + joystickString);
        aimVector.z = Input.GetAxis("RightJoystickY_p" + joystickString);

        return Vector3.Dot(movementVector, aimVector);
    }

    public override bool getInteractInput()
    {
        joystickString = joystickNumber.ToString();

        return Input.GetButtonDown("InteractButton_p" + joystickString);
    }

    public override bool getLightInput()
    {
        joystickString = joystickNumber.ToString();

        return Input.GetButtonDown("LightButton_p" + joystickString);
    }
}
