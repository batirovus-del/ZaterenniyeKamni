using I2.Loc;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupInGameItemStore : Popup
{
	public List<ItemsInGameStore> listItems = new List<ItemsInGameStore>();

	public ItemsInGameStore ObjBaseItem;

	public Text TextTitle;

	public Text TextDesc;

	public override void OnEnable()
	{
		base.OnEnable();
	}

	private void Caching()
	{
	}

	public void SetPopup(Booster.BoosterType boosterType, bool isTutorial = false)
	{
		Caching();
		if (!isTutorial)
		{
			MonoSingleton<UIManager>.Instance.SetCoinCurrencyMenuLayer(isPopupOverLayer: true);
		}
		switch (boosterType)
		{
		case Booster.BoosterType.Hammer:
				if (LocalizationManager.CurrentLanguage == "English")
				{
                    TextTitle.text = "Magic Hammer";
                    TextDesc.text = "Magic Hammer can break a gem or an obstacle.";
                }
				else if (LocalizationManager.CurrentLanguage == "Russian")
				{
                    TextTitle.text = "Волшебный молот";
                    TextDesc.text = "Волшебный молот может разбить драгоценный камень или препятствие.";
                }

                    break;
		case Booster.BoosterType.CandyPack:
                if (LocalizationManager.CurrentLanguage == "English")
                {
                    TextTitle.text = "Gem Bomb";
                    TextDesc.text = "You can remove all of the gems and obstacles.";
                }
                else if (LocalizationManager.CurrentLanguage == "Russian")
                {
                    TextTitle.text = "Самоцветная бомба";
                    TextDesc.text = "Вы можете удалить все драгоценные камни и препятствия.";
                }
                
			break;
		case Booster.BoosterType.HBomb:
                if (LocalizationManager.CurrentLanguage == "English")
                {
                    TextTitle.text = "Horizontal Bomb";
                    TextDesc.text = "You can removes any gems that are either horizontally.";
                }
                else if (LocalizationManager.CurrentLanguage == "Russian")
                {
                    TextTitle.text = "Горизонтальная бомба";
                    TextDesc.text = "Вы можете удалить любые драгоценные камни, расположенные горизонтально.";
                }
                
			break;
		case Booster.BoosterType.VBomb:
                if (LocalizationManager.CurrentLanguage == "English")
                {
                    TextTitle.text = "Vertical Bomb";
                    TextDesc.text = "You can removes any gems that are either vertically.";
                }
                else if (LocalizationManager.CurrentLanguage == "Russian")
                {
                    TextTitle.text = "Вертикальная бомба";
                    TextDesc.text = "Вы можете удалить любые драгоценные камни, которые расположены вертикально.";
                }
                
			break;
		}
		listItems.Clear();
		int boosterItemIndex = MonoSingleton<ServerDataTable>.Instance.GetBoosterItemIndex(boosterType);
		if (!MonoSingleton<PlayerDataManager>.Instance.dicBoosterItemList.ContainsKey(boosterItemIndex))
		{
			return;
		}
		for (int i = 0; i < MonoSingleton<PlayerDataManager>.Instance.dicBoosterItemList[boosterItemIndex].Count; i++)
		{
			if (MonoSingleton<ServerDataTable>.Instance.m_dicTableItemShop.ContainsKey(MonoSingleton<PlayerDataManager>.Instance.dicBoosterItemList[boosterItemIndex][i]))
			{
				GameObject gameObject;
				if (i > 0)
				{
					gameObject = Object.Instantiate(ObjBaseItem.gameObject);
					gameObject.transform.SetParent(ObjBaseItem.transform.parent, worldPositionStays: false);
				}
				else
				{
					gameObject = ObjBaseItem.gameObject;
				}
				gameObject.GetComponent<ItemsInGameStore>().SetItem(MonoSingleton<ServerDataTable>.Instance.m_dicTableItemShop[MonoSingleton<PlayerDataManager>.Instance.dicBoosterItemList[boosterItemIndex][i]], boosterType, i);
				listItems.Add(gameObject.GetComponent<ItemsInGameStore>());
			}
		}
	}

	public override void OnEventClose()
	{
		base.OnEventClose();
		MonoSingleton<UIManager>.Instance.HideCoinCurrentMenuLayer();
	}
}
