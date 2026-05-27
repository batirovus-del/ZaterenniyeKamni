using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class PopupLiteTutorial : Popup
{
	private string[] tutorialDesc = new string[16]
	{
        "Try to match 3 same Gems!",
        "Match the Gems within the Jail!",
        "Match the Gems around the Blocks.",
        "Match the Gems on the Tile.",
        "Match the Gems on the Gold to spread it.",
        "Bring the Crowns down to the bottom of the board.",
        "Find the Holy Grail behind the Tiles!",
        "Match the Gems & Find the Silvers!",

        "Попробуйте соединить 3 одинаковых драгоценных камня!",
        "Сопоставьте драгоценные камни в тюрьме!",
        "Сопоставьте драгоценные камни вокруг блоков.",
        "Сопоставьте драгоценные камни на плитке.",
        "Сопоставьте драгоценные камни с золотом, чтобы распространить его.",
        "Принесите Короны к нижней части доски.",
        "Найдите Святой Грааль за плиткой!",
        "Сопоставьте драгоценные камни и найдите серебро!"
    };

	private string[] tutorialTitle = new string[0];

	public GameObject[] ObjTutorailGroups;

	public Text TextTutorialTitle;

	public Text TextTutorialDesc;

	public void SetData(int tutorialIndex)
	{
		ObjTutorailGroups[tutorialIndex].SetActive(value: true);

        if (LocalizationManager.CurrentLanguage == "English")
		    TextTutorialDesc.text = tutorialDesc[tutorialIndex];
        else if (LocalizationManager.CurrentLanguage == "Russian")
            TextTutorialDesc.text = tutorialDesc[tutorialIndex+8];
    }
}
