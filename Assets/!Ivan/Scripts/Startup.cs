using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Startup : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (YG2.isSDKEnabled == false)
            yield return null;

        Localization.InitTranslations();
        SceneManager.LoadSceneAsync(1);
    }
}
