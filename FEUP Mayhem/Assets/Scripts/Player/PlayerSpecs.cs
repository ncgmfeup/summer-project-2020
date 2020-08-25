using UnityEngine;

public class PlayerSpecs : MonoBehaviour
{
    private string fireButton = "Fire1";
    private string jumpButton = "Jump1";
    private string jumpDownButton = "JumpDown1";
    private string horizontalAxis = "Horizontal1";

    public void SetFireButtonName(string fire)
    {
        fireButton = fire;
    }

    public void SetJumpButtonName(string jump)
    {
        jumpButton = jump;
    }

    public void SetHorizontalAxisName(string horizontal)
    {
        horizontalAxis = horizontal;
    }

    public void SetJumpDownButtonName(string jumpDown)
    {
        jumpDownButton = jumpDown;
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

    public string JumpDownName()
    {
        return jumpDownButton;
    }
}
