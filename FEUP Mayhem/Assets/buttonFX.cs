using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFX : MonoBehaviour
{
    static buttonFX instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
            Destroy(gameObject);
        else{
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
