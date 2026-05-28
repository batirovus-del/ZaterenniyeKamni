//using UnityEditor.Hardware;
using UnityEngine;
using YG;

public class UILevelBallButtonEvent : MonoBehaviour
{
	public void OnPressLevelBallButton()
	{
        if (!YandexGame.nowAdsShow && YandexGame.timerShowAd >= YandexGame.Instance.infoYG.fullscreenAdInterval)
        {
            YandexGame.FullscreenShow(null, onPressLevelBallButton);
        }
        else
        {
            onPressLevelBallButton();
        }
        
	}

    public void onPressLevelBallButton()
    {
        int result = 1;
        if (int.TryParse(base.gameObject.name, out result) && result <= MonoSingleton<PlayerDataManager>.Instance.CurrentLevelNo)
        {
            MapData.main = new MapData(result);
            MonoSingleton<PlayerDataManager>.Instance.lastPlayedLevel = result;
            SoundSFX.Play(SFXIndex.ButtonClick);
            MonoSingleton<SceneControlManager>.Instance.LoadScene(SceneType.Game, SceneChangeEffect.Color);
            GameMain.CompleteGameStart();
        }
    }
}
