using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    
    private string moreGamesUrl = "https://play.google.com/store/apps/dev?id=6243161491331191597";

    public void openRate()
    {
        Debug.Log("Url is opened");
        //Application.OpenURL(rateUrl);
    }

    public void openMoreGames()
    {
        Debug.Log("Url is opened");
        Application.OpenURL(moreGamesUrl);
    }
}
