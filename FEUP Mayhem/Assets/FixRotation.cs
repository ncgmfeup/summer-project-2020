
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion rotation;
    void Awake()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
    void LateUpdate()
    {
        //transform.position = player.position + Vector3.up * 3f;  
    }
}