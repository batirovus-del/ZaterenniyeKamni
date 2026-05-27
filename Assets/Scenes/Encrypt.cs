using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cookapps;
using System;
using System.IO;

public class Encrypt : MonoBehaviour
{
    public string urlIn, urlOut;
    // Start is called before the first frame update
    void Start()
    {
        LoadTextInternal(urlIn, value =>
        {
            File.WriteAllText(urlOut, DataEncryption.EncryptString(value)); ;
        });
    }
    
    private void LoadTextInternal(string dataFile, Action<string> action)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataFile);
        if (textAsset != null)
            action(textAsset.text);
        else
            action("");
    }
}
