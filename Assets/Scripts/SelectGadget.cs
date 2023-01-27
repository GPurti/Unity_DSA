using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGadget : MonoBehaviour
{    
    public GameObject missingObject;

    void Start()
    {
        
    }

    public void destroyMissing()
    {
        Destroy(missingObject);
    }
}
