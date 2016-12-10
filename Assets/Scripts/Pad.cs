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

        Vector2 verticalAxes = new Vector2(0.0f, 1.0f);

        aimVector.x = Input.GetAxis("RightJoystickX_p" + joystickString);
        aimVector.y = Input.GetAxis("RightJoystickY_p" + joystickString);

        //float dotProduct = Vector3.Dot(aimVector, verticalAxes);
        //Vector3 crossProduct = Vector3.Cross(aimVector, verticalAxes);

        //float cosAlpha = dotProduct / (getNorm(aimVector) * getNorm(verticalAxes));
        //float sinAlpha = crossProduct.y / (getNorm(aimVector) * getNorm(verticalAxes));

        //float angle = Mathf.Atan2(sinAlpha, cosAlpha) * 180/Mathf.PI;
        float angle = Mathf.Acos(Vector3.Dot(aimVector, verticalAxes)) * 180 / Mathf.PI;

        Debug.Log(aimVector);

        return aimVector;
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

    private float getNorm(Vector3 vec3)
    {
        return Mathf.Sqrt(Mathf.Pow(vec3[0], 2) +
                          Mathf.Pow(vec3[1], 2) +
                          Mathf.Pow(vec3[2], 2));
    }
}
