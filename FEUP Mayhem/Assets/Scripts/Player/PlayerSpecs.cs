using UnityEngine;

public class PlayerSpecs : MonoBehaviour
{
    private string fireButton = "Fire1";
    private string jumpButton = "Jump1";
    private string horizontalAxis = "Horizontal1";

    public void SetFireButtonName(string fire)
    {
        fireButton = fire;
    }

    public void JumpButtonName(string jump)
    {
        jumpButton = jump;
    }

    public void HorizontalAxisName(string horizontal)
    {
        horizontalAxis = horizontal;
    }

    public string FireButtonName()
    {
        return fireButton;
    }

    public string JumpButtonName()
    {
        return jumpButton;
    }

    public string HorizontalAxisName()
    {
        return horizontalAxis;
    }
}
