using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class EasyMobileInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
        Debug.Log("Easymobile is Inited");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
