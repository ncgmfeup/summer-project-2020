using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSpecs : MonoBehaviour
{
    public abstract string FireButtonName();
    public abstract string JumpButtonName();
    public abstract string HorizontalAxisName();
}
