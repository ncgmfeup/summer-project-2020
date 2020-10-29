using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Perk_Health : MonoBehaviour
{
    [SerializeField]
    float HP = 0.1f;


    public void SubtractHealth(float number)
    {
        HP -= number;
        Debug.Log(HP);
        if (HP <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
