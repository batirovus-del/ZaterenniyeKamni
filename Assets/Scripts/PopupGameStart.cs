using I2.Loc;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;
//using Firebase.Analytics;

public class PopupGameStart : Popup, IAppEventAdWatchingFunnel
{
	public enum PoupGameStartType
	{
		GameStart,
		GameRetry,
		GameResult
	}

	public RectTransform MainView;

	public Text TextLevel;

	[Header("Target")]
	public GameObject targetRoot;

	public Text TextGoal;

	public Text TextScore;

	[Header("Star")]
	public GameObject starRoot;

	public GameObject[] ObjsStar;

	public GameObject[] objStarIdleEffect;

	public GameObject[] objGetStarEffect;

	public GameObject[] objStarIdleEffectForAdd;

	public GameObject[] objGetStarEffectForAdd;

	public GameObject PrefabStarIdleEffect;

	public GameObject PrefabGetStarEffect;

	[Space(10f)]
	[Header("Buttons")]
	public GameObject buttonPlay;

	public GameObject groupNext;

	[Space(10f)]
	public PoupGameStartType m_Type;

	private int nStartLevel;

	private AudioSource GameClearLoopSound;

	public static AppEventManager.AdAccessedBy accessedBy = AppEventManager.AdAccessedBy.Levelball_from_Lobby;

	private AppEventManager.AdCompletedStepReached adCompletedStepReached;

	private AppEventManager.AdCompletedStepReached adCompletedStepReachedForGuestAds;

	private int beforeStarIndex;

    public void SendAppEventAdWatchingFunnel(int beforeLife, AppEventManager.AdCompletedStepReached step)
	{
		adCompletedStepReached = step;
		MonoSingleton<AppEventManager>.Instance.SendAppEventAdWatchingFunnel(step, (int)(DateTime.Now - AppEventManager.m_TempBox.PurchaseFunnelStepElapsedTime).TotalSeconds, beforeLife, MonoSingleton<PlayerDataManager>.Instance.Coin, AdRewardType.PlayWithItem, 1, AppEventManager.AdAccessedBy.Levelball_from_Lobby);
	}

	public void SetAdCompletedStepReached(AppEventManager.AdCompletedStepReached step)
	{
		adCompletedStepReached = step;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Y))
		{
			StartCoroutine(waitGetStarEffect(0.5f, 3));
		}
	}

	public override void Start()
	{
		base.Start();
	}

	public override void OnEnable()
	{
		SetAdCompletedStepReachedForGuestBonus(AppEventManager.AdCompletedStepReached.None);
		base.transform.localScale = Vector3.one;
		base.OnEnable();
	}

	private void OnDestroy()
	{
		if ((bool)GameClearLoopSound)
		{
			GameClearLoopSound.Stop();
			GameClearLoopSound = null;
		}
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		RemoveStarEffect();
		if ((bool)GameClearLoopSound)
		{
			GameClearLoopSound.Stop();
			GameClearLoopSound = null;
		}
	}

	public void SetPopupLevelStart(int level, GoalTarget target, int starCount, PoupGameStartType type, bool isHardLevel)
	{
		TextScore.text = string.Empty;
		m_Type = type;
		nStartLevel = level;
		if ((bool)TextLevel)
		{
            if (LocalizationManager.CurrentLanguage == "English")
                TextLevel.text = "Level " + level;
            else if (LocalizationManager.CurrentLanguage == "Russian")
                TextLevel.text = "Уровень " + level;

        }
		if (!isHardLevel)
		{
			switch (target)
			{
			case GoalTarget.NotAssign:
			case GoalTarget.Score:
				MonoSingleton<ServerDataTable>.Instance.SetLangValue(TextGoal, (!MapData.IsCollectMakeSpecial(level)) ? "Popup_LevelInfo_CollectMode" : "Popup_LevelInfo_CollectMakeSpecial");
				break;
			case GoalTarget.BringDown:
				MonoSingleton<ServerDataTable>.Instance.SetLangValue(TextGoal, "Popup_LevelInfo_BringDownMode");
				break;
			case GoalTarget.SweetRoad:
				MonoSingleton<ServerDataTable>.Instance.SetLangValue(TextGoal, "Popup_LevelInfo_SweetRoadMode");
				break;
			case GoalTarget.RescueVS:
				MonoSingleton<ServerDataTable>.Instance.SetLangValue(TextGoal, "Popup_LevelInfo_WitchDefeatMode");
				break;
			case GoalTarget.RescueMouse:
			case GoalTarget.RescueGingerMan:
				MonoSingleton<ServerDataTable>.Instance.SetLangValue(TextGoal, "Popup_LevelInfo_GingerRescueMode");
				break;
			case GoalTarget.Jelly:
				MonoSingleton<ServerDataTable>.Instance.SetLangValue(TextGoal, "Popup_LevelInfo_JellyMode");
				break;
			case GoalTarget.Digging:
				MonoSingleton<ServerDataTable>.Instance.SetLangValue(TextGoal, "Popup_LevelInfo_DiggingMode");
				break;
			}
		}
		TurnOffAllObject();
		buttonPlay.SetActive(value: true);
		starRoot.SetActive(value: false);
	}

	public void SetPopupLevelSuccess(int score, int level, GoalTarget target, int starCount, bool isLogined, PoupGameStartType type)
	{
		AppEventManager.m_TempBox.stageClearedPopupClosedAction = AppEventManager.StageClearPopupClosedAction.Close;
		SetAdCompletedStepReached(AppEventManager.AdCompletedStepReached.None);
		MonoSingleton<UIManager>.Instance.HideCoinCurrentMenuLayer();
		m_Type = type;
		nStartLevel = level;
        Debug.Log(level);

        /*switch (level)
        {
            case 1:
                FirebaseAnalytics.LogEvent("level_1");
                break;
            case 2:
                FirebaseAnalytics.LogEvent("level_2");
                break;
            case 3:
                FirebaseAnalytics.LogEvent("level_3");
                break;
            case 4:
                FirebaseAnalytics.LogEvent("level_4");
                break;
            case 5:
                FirebaseAnalytics.LogEvent("level_5");
                break;
            case 6:
                FirebaseAnalytics.LogEvent("level_6");
                break;
            case 7:
                FirebaseAnalytics.LogEvent("level_7");
                break;
            case 8:
                FirebaseAnalytics.LogEvent("level_8");
                break;
            case 9:
                FirebaseAnalytics.LogEvent("level_9");
                break;
            case 10:
                FirebaseAnalytics.LogEvent("level_10");
                break;
            case 11:
                FirebaseAnalytics.LogEvent("level_11");
                break;
            case 12:
                FirebaseAnalytics.LogEvent("level_12");
                break;
            case 13:
                FirebaseAnalytics.LogEvent("level_13");
                break;
            case 14:
                FirebaseAnalytics.LogEvent("level_14");
                break;
            case 15:
                FirebaseAnalytics.LogEvent("level_15");
                break;
            case 16:
                FirebaseAnalytics.LogEvent("level_16");
                break;
            case 17:
                FirebaseAnalytics.LogEvent("level_17");
                break;
            case 18:
                FirebaseAnalytics.LogEvent("level_18");
                break;
            case 19:
                FirebaseAnalytics.LogEvent("level_19");
                break;
            case 20:
                FirebaseAnalytics.LogEvent("level_20");
                break;
            case 21:
                FirebaseAnalytics.LogEvent("level_21");
                break;
            case 22:
                FirebaseAnalytics.LogEvent("level_22");
                break;
            case 23:
                FirebaseAnalytics.LogEvent("level_23");
                break;
            case 24:
                FirebaseAnalytics.LogEvent("level_24");
                break;
            case 25:
                FirebaseAnalytics.LogEvent("level_25");
                break;
            case 26:
                FirebaseAnalytics.LogEvent("level_26");
                break;
            case 27:
                FirebaseAnalytics.LogEvent("level_27");
                break;
            case 28:
                FirebaseAnalytics.LogEvent("level_28");
                break;
            case 29:
                FirebaseAnalytics.LogEvent("level_29");
                break;
            case 30:
                FirebaseAnalytics.LogEvent("level_30");
                break;
            case 31:
                FirebaseAnalytics.LogEvent("level_31");
                break;
            case 32:
                FirebaseAnalytics.LogEvent("level_32");
                break;
            case 33:
                FirebaseAnalytics.LogEvent("level_33");
                break;
            case 34:
                FirebaseAnalytics.LogEvent("level_34");
                break;
            case 35:
                FirebaseAnalytics.LogEvent("level_35");
                break;
            case 36:
                FirebaseAnalytics.LogEvent("level_36");
                break;
            case 37:
                FirebaseAnalytics.LogEvent("level_37");
                break;
            case 38:
                FirebaseAnalytics.LogEvent("level_38");
                break;
            case 39:
                FirebaseAnalytics.LogEvent("level_39");
                break;
            case 40:
                FirebaseAnalytics.LogEvent("level_40");
                break;
            case 41:
                FirebaseAnalytics.LogEvent("level_41");
                break;
            case 42:
                FirebaseAnalytics.LogEvent("level_42");
                break;
            case 43:
                FirebaseAnalytics.LogEvent("level_43");
                break;
            case 44:
                FirebaseAnalytics.LogEvent("level_44");
                break;
            case 45:
                FirebaseAnalytics.LogEvent("level_45");
                break;
            case 46:
                FirebaseAnalytics.LogEvent("level_46");
                break;
            case 47:
                FirebaseAnalytics.LogEvent("level_47");
                break;
            case 48:
                FirebaseAnalytics.LogEvent("level_48");
                break;
            case 49:
                FirebaseAnalytics.LogEvent("level_49");
                break;
            case 50:
                FirebaseAnalytics.LogEvent("level_50");
                break;
            case 51:
                FirebaseAnalytics.LogEvent("level_51");
                break;
            case 52:
                FirebaseAnalytics.LogEvent("level_52");
                break;
            case 53:
                FirebaseAnalytics.LogEvent("level_53");
                break;
            case 54:
                FirebaseAnalytics.LogEvent("level_54");
                break;
            case 55:
                FirebaseAnalytics.LogEvent("level_55");
                break;
            case 56:
                FirebaseAnalytics.LogEvent("level_56");
                break;
            case 57:
                FirebaseAnalytics.LogEvent("level_57");
                break;
            case 58:
                FirebaseAnalytics.LogEvent("level_58");
                break;
            case 59:
                FirebaseAnalytics.LogEvent("level_59");
                break;
            case 60:
                FirebaseAnalytics.LogEvent("level_60");
                break;
            case 61:
                FirebaseAnalytics.LogEvent("level_61");
                break;
            case 62:
                FirebaseAnalytics.LogEvent("level_62");
                break;
            case 63:
                FirebaseAnalytics.LogEvent("level_63");
                break;
            case 64:
                FirebaseAnalytics.LogEvent("level_64");
                break;
            case 65:
                FirebaseAnalytics.LogEvent("level_65");
                break;
            case 66:
                FirebaseAnalytics.LogEvent("level_66");
                break;
            case 67:
                FirebaseAnalytics.LogEvent("level_67");
                break;
            case 68:
                FirebaseAnalytics.LogEvent("level_68");
                break;
            case 69:
                FirebaseAnalytics.LogEvent("level_69");
                break;
            case 70:
                FirebaseAnalytics.LogEvent("level_70");
                break;
            case 71:
                FirebaseAnalytics.LogEvent("level_71");
                break;
            case 72:
                FirebaseAnalytics.LogEvent("level_72");
                break;
            case 73:
                FirebaseAnalytics.LogEvent("level_73");
                break;
            case 74:
                FirebaseAnalytics.LogEvent("level_74");
                break;
            case 75:
                FirebaseAnalytics.LogEvent("level_75");
                break;
            case 76:
                FirebaseAnalytics.LogEvent("level_76");
                break;
            case 77:
                FirebaseAnalytics.LogEvent("level_77");
                break;
            case 78:
                FirebaseAnalytics.LogEvent("level_78");
                break;
            case 79:
                FirebaseAnalytics.LogEvent("level_79");
                break;
            case 80:
                FirebaseAnalytics.LogEvent("level_80");
                break;
            case 81:
                FirebaseAnalytics.LogEvent("level_81");
                break;
            case 82:
                FirebaseAnalytics.LogEvent("level_82");
                break;
            case 83:
                FirebaseAnalytics.LogEvent("level_83");
                break;
            case 84:
                FirebaseAnalytics.LogEvent("level_84");
                break;
            case 85:
                FirebaseAnalytics.LogEvent("level_85");
                break;
            case 86:
                FirebaseAnalytics.LogEvent("level_86");
                break;
            case 87:
                FirebaseAnalytics.LogEvent("level_87");
                break;
            case 88:
                FirebaseAnalytics.LogEvent("level_88");
                break;
            case 89:
                FirebaseAnalytics.LogEvent("level_89");
                break;
            case 90:
                FirebaseAnalytics.LogEvent("level_90");
                break;
            case 91:
                FirebaseAnalytics.LogEvent("level_91");
                break;
            case 92:
                FirebaseAnalytics.LogEvent("level_92");
                break;
            case 93:
                FirebaseAnalytics.LogEvent("level_93");
                break;
            case 94:
                FirebaseAnalytics.LogEvent("level_94");
                break;
            case 95:
                FirebaseAnalytics.LogEvent("level_95");
                break;
            case 96:
                FirebaseAnalytics.LogEvent("level_96");
                break;
            case 97:
                FirebaseAnalytics.LogEvent("level_97");
                break;
            case 98:
                FirebaseAnalytics.LogEvent("level_98");
                break;
            case 99:
                FirebaseAnalytics.LogEvent("level_99");
                break;
            case 100:
                FirebaseAnalytics.LogEvent("level_100");
                break;
            case 101:
                FirebaseAnalytics.LogEvent("level_101");
                break;
            case 102:
                FirebaseAnalytics.LogEvent("level_102");
                break;
            case 103:
                FirebaseAnalytics.LogEvent("level_103");
                break;
            case 104:
                FirebaseAnalytics.LogEvent("level_104");
                break;
            case 105:
                FirebaseAnalytics.LogEvent("level_105");
                break;
            case 106:
                FirebaseAnalytics.LogEvent("level_106");
                break;
            case 107:
                FirebaseAnalytics.LogEvent("level_107");
                break;
            case 108:
                FirebaseAnalytics.LogEvent("level_108");
                break;
            case 109:
                FirebaseAnalytics.LogEvent("level_109");
                break;
            case 110:
                FirebaseAnalytics.LogEvent("level_110");
                break;
            case 111:
                FirebaseAnalytics.LogEvent("level_111");
                break;
            case 112:
                FirebaseAnalytics.LogEvent("level_112");
                break;
            case 113:
                FirebaseAnalytics.LogEvent("level_113");
                break;
            case 114:
                FirebaseAnalytics.LogEvent("level_114");
                break;
            case 115:
                FirebaseAnalytics.LogEvent("level_115");
                break;
            case 116:
                FirebaseAnalytics.LogEvent("level_116");
                break;
            case 117:
                FirebaseAnalytics.LogEvent("level_117");
                break;
            case 118:
                FirebaseAnalytics.LogEvent("level_118");
                break;
            case 119:
                FirebaseAnalytics.LogEvent("level_119");
                break;
            case 120:
                FirebaseAnalytics.LogEvent("level_120");
                break;
            case 121:
                FirebaseAnalytics.LogEvent("level_121");
                break;
            case 122:
                FirebaseAnalytics.LogEvent("level_122");
                break;
            case 123:
                FirebaseAnalytics.LogEvent("level_123");
                break;
            case 124:
                FirebaseAnalytics.LogEvent("level_124");
                break;
            case 125:
                FirebaseAnalytics.LogEvent("level_125");
                break;
            case 126:
                FirebaseAnalytics.LogEvent("level_126");
                break;
            case 127:
                FirebaseAnalytics.LogEvent("level_127");
                break;
            case 128:
                FirebaseAnalytics.LogEvent("level_128");
                break;
            case 129:
                FirebaseAnalytics.LogEvent("level_129");
                break;
            case 130:
                FirebaseAnalytics.LogEvent("level_130");
                break;
            case 131:
                FirebaseAnalytics.LogEvent("level_131");
                break;
            case 132:
                FirebaseAnalytics.LogEvent("level_132");
                break;
            case 133:
                FirebaseAnalytics.LogEvent("level_133");
                break;
            case 134:
                FirebaseAnalytics.LogEvent("level_134");
                break;
            case 135:
                FirebaseAnalytics.LogEvent("level_135");
                break;
            case 136:
                FirebaseAnalytics.LogEvent("level_136");
                break;
            case 137:
                FirebaseAnalytics.LogEvent("level_137");
                break;
            case 138:
                FirebaseAnalytics.LogEvent("level_138");
                break;
            case 139:
                FirebaseAnalytics.LogEvent("level_139");
                break;
            case 140:
                FirebaseAnalytics.LogEvent("level_140");
                break;
            case 141:
                FirebaseAnalytics.LogEvent("level_141");
                break;
            case 142:
                FirebaseAnalytics.LogEvent("level_142");
                break;
            case 143:
                FirebaseAnalytics.LogEvent("level_143");
                break;
            case 144:
                FirebaseAnalytics.LogEvent("level_144");
                break;
            case 145:
                FirebaseAnalytics.LogEvent("level_145");
                break;
            case 146:
                FirebaseAnalytics.LogEvent("level_146");
                break;
            case 147:
                FirebaseAnalytics.LogEvent("level_147");
                break;
            default:
                break;
        }*/

		if ((bool)TextLevel)
		{
            if (LocalizationManager.CurrentLanguage == "English")
                TextLevel.text = "Level " + level;
            else if (LocalizationManager.CurrentLanguage == "Russian")
                TextLevel.text = "Уровень " + level;            
		}
		TextGoal.text = string.Empty;
		TextScore.text = string.Empty;
		TurnOffAllObject();
		starRoot.SetActive(value: true);
		groupNext.SetActive(value: true);
		StartCoroutine(GameClearScoreAnimation(score, isHardLevel: false));
		StartCoroutine(waitGetStarEffect(0.5f, starCount));
	}

	private IEnumerator waitPopupOpenCompletely(GameObject waitingObj)
	{
		waitingObj.SetActive(value: false);
		yield return new WaitForSeconds(tweenTimeToOpen);
		waitingObj.SetActive(value: true);
	}

	private IEnumerator GameClearScoreAnimation(int targetScore, bool isHardLevel)
	{
		int currentScore = 0;
		float lerpScoreTimeDuration = 0f;
		GameClearLoopSound = SoundSFX.Play(SFXIndex.GameClearPopupScoreCountLoop, loop: true);
		while (currentScore < targetScore)
		{
			currentScore = (int)Mathf.Lerp(0f, targetScore, lerpScoreTimeDuration);
			lerpScoreTimeDuration += Time.deltaTime * 0.8f;
			TextScore.text = Utils.GetCurrencyNumberString(currentScore);
			yield return null;
		}
		if ((bool)GameClearLoopSound)
		{
			GameClearLoopSound.Stop();
			GameClearLoopSound = null;
			SoundSFX.Play(SFXIndex.GameClearPopupScoreCountEnd);
		}
	}

	private void TurnOffAllObject()
	{
		buttonPlay.SetActive(value: false);
		groupNext.SetActive(value: false);
	}

	private void RemoveGetStarEffectForAdd()
	{
		if (objGetStarEffectForAdd != null && objGetStarEffectForAdd.Length > 0)
		{
			for (int i = 0; i < objGetStarEffectForAdd.Length; i++)
			{
				if ((bool)objGetStarEffectForAdd[i])
				{
					UnityEngine.Object.Destroy(objGetStarEffectForAdd[i]);
				}
			}
		}
		objGetStarEffectForAdd = null;
	}

	private void RemoveIdleStarEffectForAdd()
	{
		if (objStarIdleEffectForAdd != null && objStarIdleEffectForAdd.Length > 0)
		{
			for (int i = 0; i < objStarIdleEffectForAdd.Length; i++)
			{
				if ((bool)objStarIdleEffectForAdd[i])
				{
					UnityEngine.Object.Destroy(objStarIdleEffectForAdd[i]);
				}
			}
		}
		objStarIdleEffectForAdd = null;
	}

	private IEnumerator waitIdleStarEffectForAdd(float delay, int starCount)
	{
		yield return new WaitForSeconds(delay);
		CreateStarIdleEffectForAdd(starCount);
	}

	private void CreateStarIdleEffectForAdd(int starCount)
	{
		RemoveIdleStarEffectForAdd();
		RemoveGetStarEffectForAdd();
		objStarIdleEffectForAdd = new GameObject[starCount];
		objGetStarEffectForAdd = new GameObject[starCount];
		for (int i = 0; i < ObjsStar.Length; i++)
		{
			if (i < starCount && i > beforeStarIndex)
			{
				ObjsStar[i].SetActive(value: true);
				objStarIdleEffectForAdd[i] = UnityEngine.Object.Instantiate(PrefabStarIdleEffect);
				objStarIdleEffectForAdd[i].transform.SetParent(ObjsStar[i].transform, worldPositionStays: false);
				objStarIdleEffectForAdd[i].transform.localPosition = Vector3.zero;
			}
			else if (i == beforeStarIndex)
			{
				ObjsStar[i].SetActive(value: true);
			}
			else
			{
				ObjsStar[i].SetActive(value: false);
			}
		}
	}

	private IEnumerator waitGetStarEffectForAdd(float delay, int starCount)
	{
		yield return new WaitForSeconds(delay);
		RemoveIdleStarEffectForAdd();
		RemoveGetStarEffectForAdd();
		objGetStarEffectForAdd = new GameObject[starCount];
		objStarIdleEffectForAdd = new GameObject[starCount];
		for (int i = 0; i < ObjsStar.Length; i++)
		{
			if (i < starCount && i > beforeStarIndex)
			{
				ObjsStar[i].SetActive(value: true);
				ObjsStar[i].GetComponent<Image>().enabled = false;
				if (objGetStarEffectForAdd != null)
				{
					objGetStarEffectForAdd[i] = UnityEngine.Object.Instantiate(PrefabGetStarEffect);
					objGetStarEffectForAdd[i].transform.SetParent(ObjsStar[i].transform, worldPositionStays: false);
					objGetStarEffectForAdd[i].transform.localPosition = Vector3.zero;
					SoundSFX.Play((SFXIndex)(16 + i), loop: false, 0.25f);
				}
			}
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(0.667f);
		if (objGetStarEffectForAdd != null)
		{
			for (int j = 0; j < ObjsStar.Length; j++)
			{
				if (j < starCount && j > beforeStarIndex)
				{
					UnityEngine.Object.Destroy(objGetStarEffectForAdd[j]);
					ObjsStar[j].GetComponent<Image>().enabled = true;
				}
			}
		}
		StartCoroutine(waitIdleStarEffectForAdd(0.1f, starCount));
	}

	private void RemoveStarEffect()
	{
		for (int i = 0; i < ObjsStar.Length; i++)
		{
			ObjsStar[i].SetActive(value: false);
		}
		if (objStarIdleEffect != null && objStarIdleEffect.Length > 0)
		{
			for (int j = 0; j < objStarIdleEffect.Length; j++)
			{
				if ((bool)objStarIdleEffect[j])
				{
					UnityEngine.Object.Destroy(objStarIdleEffect[j]);
				}
			}
		}
		objStarIdleEffect = null;
		if (objGetStarEffect != null && objGetStarEffect.Length > 0)
		{
			for (int k = 0; k < objGetStarEffect.Length; k++)
			{
				if (objGetStarEffect[k] != null)
				{
					UnityEngine.Object.Destroy(objGetStarEffect[k]);
				}
			}
		}
		objGetStarEffect = null;
		RemoveIdleStarEffectForAdd();
		RemoveGetStarEffectForAdd();
	}

	private IEnumerator waitIdleStarEffect(float delay, int starCount)
	{
		yield return new WaitForSeconds(delay);
		CreateStarIdleEffect(starCount);
	}

	private void CreateStarIdleEffect(int starCount)
	{
		RemoveStarEffect();
		objStarIdleEffect = new GameObject[starCount];
		for (int i = 0; i < ObjsStar.Length; i++)
		{
			if (i < starCount)
			{
				ObjsStar[i].SetActive(value: true);
				objStarIdleEffect[i] = UnityEngine.Object.Instantiate(PrefabStarIdleEffect);
				objStarIdleEffect[i].transform.SetParent(ObjsStar[i].transform, worldPositionStays: false);
				objStarIdleEffect[i].transform.localPosition = Vector3.zero;
			}
			else
			{
				ObjsStar[i].SetActive(value: false);
			}
		}
	}

	private IEnumerator waitGetStarEffect(float delay, int starCount)
	{
		yield return new WaitForSeconds(delay);
		RemoveStarEffect();
		objGetStarEffect = new GameObject[starCount];
		for (int i = 0; i < ObjsStar.Length; i++)
		{
			if (i < starCount)
			{
				ObjsStar[i].SetActive(value: true);
				ObjsStar[i].GetComponent<Image>().enabled = false;
				objGetStarEffect[i] = UnityEngine.Object.Instantiate(PrefabGetStarEffect);
				objGetStarEffect[i].transform.SetParent(ObjsStar[i].transform, worldPositionStays: false);
				objGetStarEffect[i].transform.localPosition = Vector3.zero;
				SoundSFX.Play((SFXIndex)(16 + i), loop: false, 0.25f);
			}
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(0.667f);
		for (int j = 0; j < ObjsStar.Length; j++)
		{
			if (j < starCount)
			{
				UnityEngine.Object.Destroy(objGetStarEffect[j]);
				ObjsStar[j].GetComponent<Image>().enabled = true;
			}
		}
		StartCoroutine(waitIdleStarEffect(0.1f, starCount));
	}

	public override void OnEventOK()
	{
        YandexAdManager.Instance.ShowInterstitial();
        if (!YandexGame.nowAdsShow && YandexGame.timerShowAd >= YandexGame.Instance.infoYG.fullscreenAdInterval)
        {
            YandexGame.FullscreenShow(null, eventOk);
        }
        else
        {
            eventOk();
        }
    }

    public void eventOk()
    {
        //Debug.Log("LEVEL CLEAR!!!!!!!!!!!" + level);
        base.OnEventOK();
        if (m_Type == PoupGameStartType.GameResult)
        {
            if (AppEventManager.m_TempBox.stageClearedPopupClosedAction != AppEventManager.StageClearPopupClosedAction.Invite)
            {
                AppEventManager.m_TempBox.stageClearedPopupClosedAction = AppEventManager.StageClearPopupClosedAction.Next;
            }
            MonoSingleton<AppEventManager>.Instance.SendAppEventPopupLevelCleared();
            // FindObjectOfType<AdManager>().ShowAdmobRewardVideo();
        }
    }

    public void OnPressDailySpin()
    {
        MonoSingleton<PopupManager>.Instance.Open(PopupType.PopupEventDailySpinReward);
    }

    public override void OnEventClose()
	{
        if (!YandexGame.nowAdsShow && YandexGame.timerShowAd >= YandexGame.Instance.infoYG.fullscreenAdInterval)
        {
            YandexGame.FullscreenShow(null, onEventClose);
        }
        else
        {
            onEventClose();
        }
        
    }

    public void onEventClose()
    {
        //YandexAdManager.Instance.ShowInterstitial();
        if (m_Type == PoupGameStartType.GameResult && AppEventManager.m_TempBox.stageClearedPopupClosedAction != 0)
        {
            MonoSingleton<AppEventManager>.Instance.SendAppEventPopupLevelCleared();
        }
        base.OnEventClose();

        //FindObjectOfType<AdManager>().ShowAdmobInterstitial();
        // Advertising.ShowInterstitialAd();
    }

    public void OnEventButtonGamePlay(bool forceStart = false)
	{
		eventClose = null;
		OnEventClose();
		MonoSingleton<PlayerDataManager>.Instance.lastPlayedLevel = nStartLevel;
		MapData.main = new MapData(nStartLevel);
		if (MonoSingleton<SceneControlManager>.Instance.CurrentSceneType == SceneType.Lobby)
		{
			MonoSingleton<SceneControlManager>.Instance.LoadScene(SceneType.Game, SceneChangeEffect.Color);
			GameMain.CompleteGameStart();
			return;
		}
		MonoSingleton<PopupManager>.Instance.CloseAllPopup();
		MonoSingleton<SceneControlManager>.Instance.RemoveCurrentScene();
		MonoSingleton<SceneControlManager>.Instance.LoadScene(SceneType.Game, SceneChangeEffect.Color);
		GameMain.CompleteGameStart();
	}

	public void SetAdCompletedStepReachedForGuestBonus(AppEventManager.AdCompletedStepReached step)
	{
		adCompletedStepReachedForGuestAds = step;
	}

	public void SendAppEventAdWatchingFunnelForGuestBonus(int beforeLife, AppEventManager.AdCompletedStepReached step)
	{
		adCompletedStepReachedForGuestAds = step;
		MonoSingleton<AppEventManager>.Instance.SendAppEventAdWatchingFunnel(step, (int)(DateTime.Now - AppEventManager.m_TempBox.PurchaseFunnelStepElapsedTime).TotalSeconds, beforeLife, MonoSingleton<PlayerDataManager>.Instance.Coin, AdRewardType.GuestBonus, 3, AppEventManager.AdAccessedBy.Guest_GameStartPopup_RankingUI);
	}

	public void SendAppEventAdWatchingFunnelForPreBooster(int beforeLife, AppEventManager.AdCompletedStepReached step)
	{
		adCompletedStepReached = step;
		MonoSingleton<AppEventManager>.Instance.SendAppEventAdWatchingFunnel(step, (int)(DateTime.Now - AppEventManager.m_TempBox.PurchaseFunnelStepElapsedTime).TotalSeconds, beforeLife, MonoSingleton<PlayerDataManager>.Instance.Coin, AdRewardType.PreBooster, 3, AppEventManager.AdAccessedBy.Levelball_from_Lobby);
	}

	public void SendAppEventAdWatchingFunnelForStar(int beforeLife, AppEventManager.AdCompletedStepReached step)
	{
		adCompletedStepReached = step;
		MonoSingleton<AppEventManager>.Instance.SendAppEventAdWatchingFunnel(step, (int)(DateTime.Now - AppEventManager.m_TempBox.PurchaseFunnelStepElapsedTime).TotalSeconds, beforeLife, MonoSingleton<PlayerDataManager>.Instance.Coin, AdRewardType.Star, 3, AppEventManager.AdAccessedBy.Level_Result_Popup);
	}

	public void OnPressNextByAD()
	{

        //Advertising.ShowRewardedAd();
        YandexAdManager.Instance.ShowRewardedAd();
        //FindObjectOfType<AdManager>().ShowAdmobRewardVideo();
        GameMain.rewardMove3ByADStart = true;
				OnEventOK();
				MonoSingleton<AppEventManager>.Instance.SendAppEventAdCompleted(MonoSingleton<PlayerDataManager>.Instance.Coin, MonoSingleton<PlayerDataManager>.Instance.Coin, AdRewardType.StartMove3, 3, AppEventManager.AdAccessedBy.Level_Result_Popup);
		
	}
}
