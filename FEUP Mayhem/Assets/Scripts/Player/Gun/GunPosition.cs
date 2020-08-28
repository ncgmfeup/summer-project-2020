using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPosition : MonoBehaviour
{
    [SerializeField] Vector2 gunRelatvePos;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = gunRelatvePos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
