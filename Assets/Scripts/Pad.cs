using UnityEngine;
using System.Collections;

public class Pad : Controller
{
    public int joystickNumber = 1;
    string joystickString;

    public override Vector3 getDisplacement()
    {
        joystickString = joystickNumber.ToString();

        movementVector.x = Input.GetAxis("LeftJoystickX_p" + joystickString) * movementSpeed;
        movementVector.z = Input.GetAxis("LeftJoystickY_p" + joystickString) * movementSpeed;

        return movementVector;
    }

    public override Vector2 getAngleTorchlight()
    {
        joystickString = joystickNumber.ToString();

        aimVector.x = Input.GetAxis("RightJoystickX_p" + joystickString);
        aimVector.y = Input.GetAxis("RightJoystickY_p" + joystickString);

        return aimVector;
    }

    public override bool getInteractInput()
    {
        joystickString = joystickNumber.ToString();
        return Input.GetButtonDown("InteractButton_p" + joystickString);
    }

    public override bool getPauseInput()
    {
        joystickString = joystickNumber.ToString();

        return Input.GetButtonDown("StartButton_p" + joystickString);
    }

    public override bool getExitInput()
    {
        joystickString = joystickNumber.ToString();

        return Input.GetButtonDown("ExitButton_p" + joystickString);
    }

    public override bool getLightInput()
    {
        joystickString = joystickNumber.ToString();

        return (Input.GetAxis("LightButton_p" + joystickString) != 0);
    }

    private float getNorm(Vector3 vec3)
    {
        return Mathf.Sqrt(Mathf.Pow(vec3[0], 2) +
                          Mathf.Pow(vec3[1], 2) +
                          Mathf.Pow(vec3[2], 2));
    }
}