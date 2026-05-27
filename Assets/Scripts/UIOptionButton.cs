using UnityEngine;
using YG;

public class UIOptionButton : MonoBehaviour
{
	public enum OptionMenuType
	{
		Lobby,
		Game
	}

	public OptionMenuType optionMenuType;

	public void OnPressMainButton()
	{
        if (!YG2.nowAdsShow)
        {
            YG2.InterstitialAdvShow();
        }
        else
        {
            onPressMainButton();
        }
        
	}

    public void onPressMainButton()
    {
        if (GameMain.main != null && GameMain.main.CanIWait())
        {
            MonoSingleton<UIManager>.Instance.CancelBooster();
            PopupSetting popupSetting = MonoSingleton<PopupManager>.Instance.Open(PopupType.PopupSetting) as PopupSetting;
            popupSetting.SetPopup(optionMenuType);
        }
        else
        {
            PopupSetting popupSetting2 = MonoSingleton<PopupManager>.Instance.Open(PopupType.PopupSetting) as PopupSetting;
            popupSetting2.SetPopup(optionMenuType);
        }
    }
}
