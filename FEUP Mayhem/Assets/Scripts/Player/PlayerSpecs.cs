using UnityEngine;

public class PlayerSpecs : MonoBehaviour
{
    [SerializeField]
    private string fireButton = "Fire1";
    [SerializeField]
    private string jumpButton = "Jump1";
    [SerializeField]
    private string jumpDownButton = "JumpDown1";
    [SerializeField]
    private string horizontalAxis = "Horizontal1";
    [SerializeField]
    private string dynamiteButton = "Dynamite1";
    [SerializeField]
    private string perkButton = "Perk1";

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

    public string DynamiteButtonName()
    {
        return dynamiteButton;
    }

    public string PerkButtonName()
    {
        return perkButton;
    }
}
