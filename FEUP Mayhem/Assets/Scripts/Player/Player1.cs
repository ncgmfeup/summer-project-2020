using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerSpecs
{
    public override string FireButtonName()
    {
        return "Fire1";
    }

    public override string HorizontalAxisName()
    {
        return "Horizontal1";
    }

    public override string JumpButtonName()
    {
        return "Jump1";
    }
}
