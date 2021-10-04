using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImmune : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(transform.parent.transform.localRotation.eulerAngles);
    }
}
