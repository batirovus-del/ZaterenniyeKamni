#if ENABLE_ANTI_CHEAT
using CodeStage.AntiCheat.ObscuredTypes;
#endif
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YG;

public class PlayerDataManager : MonoSingleton<PlayerDataManager>
{
	public int lastLevelStreakFailCount;

	public int adsCount;

	public static int sequenceWinCount = 0;

	public static readonly int DefaultBoosterCount = 0;

	public SystemLanguage SystemLanguage = SystemLanguage.English;

	public Dictionary<int, List<int>> dicBoosterItemList = new Dictionary<int, List<int>>();

	public Dictionary<int, int> dicPlayedLevelScore = new Dictionary<int, int>();

	public Dictionary<int, int> dicPlayedLevelStarPoint = new Dictionary<int, int>();

	public int lastPlayedLevel = -1;

	public bool IsFirstAppInstall = true;

	public DateTime lastLoginDateTime = DateTime.Now;

	[NonSerialized]
	public Booster.BoosterType[] enableBoosterIndexes = new Booster.BoosterType[5]
	{
		Booster.BoosterType.Hammer,
		Booster.BoosterType.CandyPack,
		Booster.BoosterType.HBomb,
		Booster.BoosterType.VBomb,
		Booster.BoosterType.Shuffle
	};

	public int LastPlayedUnixTime;

	public bool IsFirstPlayer = true;

	public bool IsFirstSession = true;

	public bool IsIPad;

	public bool cheatSuperPower;

	public bool CanIGetLogSortingOrder;

	public static int firstGameLaunchTick;

    public DateTime lastRecvDailyBonusDateTime = DateTime.Now;
    public DateTime lastAdRecvDailyBonusDateTime = DateTime.Now;


    private static string deviceID = string.Empty;

	public int addedStarCount;

	public bool isNewClear;

	private readonly StringBuilder strBuilder = new StringBuilder();

	public int PayCount;

	public float PayTotalDollar;

	public int LastPayUnixTimeStamp;

	public bool EnabledDoubleShopCoin;

	private static readonly string KEY_DEVICE_ID = "Device";

	private readonly string KEY_HEADER_BOOSTER = "Booster_";

	private readonly string KEY_LEVEL = "LocalGid";

	private readonly string KEY_OPTION_SOUND_BGM = "SoundBGM";

	private readonly string KEY_OPTION_SOUND_EFFECT = "SoundEffect";

	private readonly string KEY_HEADER_LEVEL_SCORE = "LvS";

	private readonly string KEY_HEADER_LEVEL_STAR = "LvP";

	private readonly string KEY_HEADER_ALL_CLEAR_LEVEL_TAG = "AllClearedLevel";

	private readonly string KEY_COIN = "CCC";

	public bool AllLevelCleared;

	public int CurrentLevelNo = 1;

	public bool IsOnSoundBGM = true;

	public bool IsOnSoundEffect = true;

	[NonSerialized]
#if ENABLE_ANTI_CHEAT
	public ObscuredInt[] BoosterCount = new ObscuredInt[5];
    private ObscuredInt coin;
#else
    public int[] BoosterCount = new int[5];
    private int coin;
#endif


    public bool IsNewMember
	{
		get;
		private set;
	}

	public int Coin
	{
		get
		{
			return coin;
		}
		set
		{
			coin = value;
		}
	}

	public void Start()
	{
		CurrentLevelNo = 33;
		firstGameLaunchTick = SavesYG.GetInt("FirstGameLaunchTick_2", Utils.ConvertToTimestamp(DateTime.Now));
        SavesYG.SetInt("FirstGameLaunchTick_2", firstGameLaunchTick);
		LoadCoinData();
		LoadBoosterData();
		LoadOptionSound();
		IsFirstPlayer = ((SavesYG.GetInt("FirstPlay", 1) == 1) ? true : false);
		MarkFirstPlay();
		LoadPayInfo();
		LoadLocalScoreAndStarPoint();
		LoadLastLoginDateTime();
#if ENABLE_ANTI_CHEAT
		lastLevelStreakFailCount = ObscuredPrefs.GetInt("lastLevelStreakFailCount", 0);
#else
        lastLevelStreakFailCount = SavesYG.GetInt("lastLevelStreakFailCount", 0);
#endif
        if (IsOnSoundBGM)
		{
			SoundManager.SetVolumeMusic(1f);
		}
		else
		{
			SoundManager.SetVolumeMusic(0f);
		}
		if (!IsOnSoundEffect)
		{
			SoundManager.StopSFX();
			SoundManager.Instance.offTheSFX = !IsOnSoundEffect;
		}
		if (MonoSingleton<SceneControlManager>.Instance.CurrentSceneType != SceneType.MapTool)
		{
			MonoSingleton<ServerDataTable>.Instance.LoadTableFromLocalFile();
		}
	}

	public void IncreaseCoin(int incCoin)
	{
		Coin += incCoin;
		SaveCoinData();
	}

	public void DecreaseCoin(int decCoin)
	{
		Coin -= decCoin;
		SaveCoinData();
	}

	public void SetCoin(int _coin)
	{
		Coin = _coin;
		SaveCoinData();
	}

	public void LoadLastDailyBonusDate()
	{
#if ENABLE_ANTI_CHEAT
		string @string = ObscuredPrefs.GetString("LastDailyBonus", string.Empty);
#else
        string @string = SavesYG.GetString("LastDailyBonus", string.Empty);
        string @string2 = SavesYG.GetString("AdLastDailyBonus", string.Empty);
#endif
        if (@string == string.Empty)
        {
            lastRecvDailyBonusDateTime = DateTime.Now.AddDays(-1.0);
        }
        else
        {
            lastRecvDailyBonusDateTime = Convert.ToDateTime(@string);
        }


        if (@string2 == string.Empty)
        {
            lastAdRecvDailyBonusDateTime = DateTime.Now.AddDays(-1.0);
        }
        else
        {
            lastAdRecvDailyBonusDateTime = Convert.ToDateTime(@string);
        }
    }

    public void SetDailyBonusReceived()
    {
#if ENABLE_ANTI_CHEAT
		ObscuredPrefs.SetString("LastDailyBonus", DateTime.Now.ToString());
#else
        SavesYG.SetString("LastDailyBonus", DateTime.Now.ToString());
#endif
        lastRecvDailyBonusDateTime = DateTime.Now;
    }

    public void SetAdDailyBonusReceived()
    {
#if ENABLE_ANTI_CHEAT
		ObscuredPrefs.SetString("LastDailyBonus", DateTime.Now.ToString());
#else
        SavesYG.SetString("AdLastDailyBonus", DateTime.Now.ToString());
#endif
        lastAdRecvDailyBonusDateTime = DateTime.Now;
    }

    public void LoadLastLoginDateTime()
	{
		string @string = SavesYG.GetString("LastLoginDateTime", string.Empty);
		if (@string == string.Empty)
		{
			IsFirstAppInstall = true;
			lastLoginDateTime = DateTime.Now;
		}
		else
		{
			IsFirstAppInstall = false;
			IsFirstSession = false;
			lastLoginDateTime = Convert.ToDateTime(@string);
		}
        SavesYG.SetString("LastLoginDateTime", DateTime.Now.ToString());
		YG2.SaveProgress();
		//PlayerPrefs.Save();
	}

	public void MarkFirstPlay()
	{
		if (IsFirstPlayer)
		{
            SavesYG.SetInt("FirstPlay", 0);
		}
	}

	public static string GetDeviceID()
	{
		if (string.IsNullOrEmpty(deviceID))
		{
			deviceID = SavesYG.GetString(KEY_DEVICE_ID, string.Empty);
		}
		if (string.IsNullOrEmpty(deviceID))
		{
			deviceID = SystemInfo.deviceUniqueIdentifier;
			if (AppEventManager.m_TempBox.isUseDeviceIDForADID == 0 || AppEventManager.m_TempBox.isUseDeviceIDForADID == 1 || AppEventManager.m_TempBox.isUseDeviceIDForADID == -1)
			{
			}
            SavesYG.SetString(KEY_DEVICE_ID, deviceID);
		}
		return deviceID;
	}

	public void SetIsNewMember(int newMember)
	{
		IsNewMember = ((newMember != 0) ? true : false);
	}

	public void UpdateLevelScore(int gid, int score, int starPoint)
	{
		int num = 0;
		bool flag = false;
		if (gid <= CurrentLevelNo)
		{
			if (gid == CurrentLevelNo)
			{
				flag = true;
				CurrentLevelNo++;
				if (CurrentLevelNo > ServerDataTable.GetLimitLevel())
				{
					AllLevelCleared = true;
				}
				CurrentLevelNo = Mathf.Min(ServerDataTable.GetLimitLevel(), CurrentLevelNo);
			}
			addedStarCount = num;
		}
		SaveLocalScoreAndStarPoint();
	}

	public static string GetSessionEndDateTimeString()
	{
		return SavesYG.GetString("SessionEndDateTime", string.Empty);
	}

	public static void SetSessionEndDateTimeString()
	{
        SavesYG.SetString("SessionEndDateTime", DateTime.Now.ToString());
	}

	public static string GetSessionStartDateTimeString()
	{
		return SavesYG.GetString("SessionStartDateTime", string.Empty);
	}

	public static void SetSessionStartDateTimeString()
	{
        SavesYG.SetString("SessionStartDateTime", DateTime.Now.ToString());
	}

	public void CheatGetMoreBooster()
	{
		for (int i = 0; i < BoosterCount.Length; i++)
		{
			++BoosterCount[i];
		}
		CPanelGameUI.Instance.UpdateTextBoosterCount();
	}

	public void RewardServerItem(ServerItemIndex itemIndex, int count, AppEventManager.ItemEarnedBy earnedBy, int gid = -1, bool holdOnUpdateCoin = false)
	{
		switch (itemIndex)
		{
		case ServerItemIndex.BoosterHammer:
		case ServerItemIndex.BoosterCandyPack:
		case ServerItemIndex.BoosterHBomb:
		case ServerItemIndex.BoosterVBomb:
			IncreaseBoosterData(itemIndex, count, earnedBy);
			break;
		case ServerItemIndex.Coin:
			IncreaseCoin(count);
			UIManager.holdOnUpdateCoin = holdOnUpdateCoin;
			MonoSingleton<AppEventManager>.Instance.SendAppEventCoinEarned(gid, Coin - count, count, Coin, earnedBy);
			break;
		}
	}

	public static string GetRewardCountValue(ServerItemIndex itemIndex, int count)
	{
		return "x " + count;
	}

	private void UpdateBoosterData(Booster.BoosterType boosterType, int count)
	{
		BoosterCount[(int)boosterType] = Mathf.Max(0, count);
		SaveBoosterData();
		if (MonoSingleton<SceneControlManager>.Instance.CurrentSceneType == SceneType.Game)
		{
			CPanelGameUI.Instance.UpdateTextBoosterCount();
		}
	}

	public void IncreaseBoosterData(Booster.BoosterType boosterType, int incValue, AppEventManager.ItemEarnedBy earnedBy)
	{
		MonoSingleton<AppEventManager>.Instance.SendAppEventInGameItemEarned(boosterType, earnedBy, incValue, 0, BoosterCount[(int)boosterType]);
		UpdateBoosterData(boosterType, (int)BoosterCount[(int)boosterType] + incValue);
	}

	public void IncreaseBoosterData(ServerItemIndex itemIndex, int incValue, AppEventManager.ItemEarnedBy earnedBy)
	{
		IncreaseBoosterData(ServerDataTable.GetBoosterTypeFromServerItemIndex(itemIndex), incValue, earnedBy);
	}

	public int GetBoosterUiIndexesFromBoosterType(Booster.BoosterType boosterType, bool needException = true)
	{
		int num = -1;
		for (int i = 0; i < enableBoosterIndexes.Length; i++)
		{
			if (enableBoosterIndexes[i] == boosterType)
			{
				num = i;
			}
		}
		if (num == -1 && needException)
		{
			num = 0;
		}
		return num;
	}

	public bool IsPayUser()
	{
		return PayCount > 0;
	}

	public void SavePayInfo()
	{
        SavesYG.SetInt("P_Count", PayCount);
        SavesYG.SetFloat("P_Total", PayTotalDollar);
        SavesYG.SetInt("P_Date", LastPayUnixTimeStamp);
        SavesYG.SetInt("P_DoubleShop", EnabledDoubleShopCoin ? 1 : 0);
	}

	public void LoadPayInfo()
	{
		PayCount = SavesYG.GetInt("P_Count", 0);
		PayTotalDollar = SavesYG.GetFloat("P_Total", 0f);
		LastPayUnixTimeStamp = SavesYG.GetInt("P_Date", 0);
		EnabledDoubleShopCoin = (SavesYG.GetInt("P_DoubleShop", 0) != 0);
	}

	private void OnApplicationQuit()
	{
	}

	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
		}
	}

	public string GetJsonSaveDataForBackup()
	{
		try
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary[KEY_DEVICE_ID] = SavesYG.GetString(KEY_DEVICE_ID, string.Empty);
			dictionary[KEY_LEVEL] = CurrentLevelNo;
			for (int i = 0; i < BoosterCount.Length; i++)
			{
				dictionary[KEY_HEADER_BOOSTER + i] = BoosterCount[i];
			}
			return JsonConvert.SerializeObject(dictionary);
		}
		catch (Exception)
		{
		}
		return string.Empty;
	}

	private void SaveCoinData()
	{
#if ENABLE_ANTI_CHEAT
		ObscuredPrefs.SetInt(KEY_COIN, Coin);
		ObscuredPrefs.Save();
#else
        SavesYG.SetInt(KEY_COIN, Coin);
		YG2.SaveProgress();
        //PlayerPrefs.Save();
#endif
    }

	private void LoadCoinData()
	{
#if ENABLE_ANTI_CHEAT
		Coin = ObscuredPrefs.GetInt(KEY_COIN, 0);
#else
		Coin = SavesYG.GetInt(KEY_COIN, 0);
#endif
    }

    public void SaveBoosterData()
	{
#if ENABLE_ANTI_CHEAT
		for (int i = 0; i < BoosterCount.Length; i++)
		{
			ObscuredPrefs.SetInt(KEY_HEADER_BOOSTER + i, BoosterCount[i]);
		}
		ObscuredPrefs.Save();
#else
        for (int i = 0; i < BoosterCount.Length; i++)
        {
            SavesYG.SetInt(KEY_HEADER_BOOSTER + i, BoosterCount[i]);
        }
		YG2.SaveProgress();
        //PlayerPrefs.Save();
#endif
    }

	private void LoadBoosterData()
	{
		bool flag = false;
#if ENABLE_ANTI_CHEAT
		for (int i = 0; i < BoosterCount.Length; i++)
		{
			BoosterCount[i] = ObscuredPrefs.GetInt(KEY_HEADER_BOOSTER + i, 1);
		}
#else
        for (int i = 0; i < BoosterCount.Length; i++)
        {
            BoosterCount[i] = SavesYG.GetInt(KEY_HEADER_BOOSTER + i, 1);
        }
#endif
    }

	public void SaveOptionSound()
	{
        SavesYG.SetInt(KEY_OPTION_SOUND_BGM, IsOnSoundBGM ? 1 : 0);
        SavesYG.SetInt(KEY_OPTION_SOUND_EFFECT, IsOnSoundEffect ? 1 : 0);
		SoundManager.SetVolumeMusic((!IsOnSoundBGM) ? 0f : 1f);
		SoundManager.Instance.offTheSFX = !IsOnSoundEffect;
	}

	private void LoadOptionSound()
	{
		int @int = SavesYG.GetInt(KEY_OPTION_SOUND_BGM, 1);
		int int2 = SavesYG.GetInt(KEY_OPTION_SOUND_EFFECT, 1);
		IsOnSoundBGM = (@int == 1);
		IsOnSoundEffect = (int2 == 1);
	}

	private void LoadLocalScoreAndStarPoint()
	{
		dicPlayedLevelScore.Clear();
		dicPlayedLevelStarPoint.Clear();
#if ENABLE_ANTI_CHEAT
		CurrentLevelNo = ObscuredPrefs.GetInt(KEY_LEVEL, 1);
		//CurrentLevelNo = 642;
		AllLevelCleared = ObscuredPrefs.GetBool(KEY_HEADER_ALL_CLEAR_LEVEL_TAG, defaultValue: false);
		string @string = ObscuredPrefs.GetString(KEY_HEADER_LEVEL_SCORE, "[0]");
		string string2 = ObscuredPrefs.GetString(KEY_HEADER_LEVEL_STAR, "[0]");
#else
        CurrentLevelNo = SavesYG.GetInt(KEY_LEVEL, 1);
        AllLevelCleared = SavesYG.GetBool(KEY_HEADER_ALL_CLEAR_LEVEL_TAG, defaultValue: false);
        string @string = SavesYG.GetString(KEY_HEADER_LEVEL_SCORE, "[0]");
        string string2 = SavesYG.GetString(KEY_HEADER_LEVEL_STAR, "[0]");
#endif
        int[] array = JsonConvert.DeserializeObject<int[]>(@string);
		int[] array2 = JsonConvert.DeserializeObject<int[]>(string2);
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				dicPlayedLevelScore.Add(i + 1, array[i]);
			}
		}
		if (array2 != null)
		{
			for (int j = 0; j < array2.Length; j++)
			{
				dicPlayedLevelStarPoint.Add(j + 1, array2[j]);
			}
		}
	}

	private void SaveLocalScoreAndStarPoint()
	{
		if (CurrentLevelNo == 0)
		{
			return;
		}
		int[] array = new int[CurrentLevelNo - 1];
		int[] array2 = new int[CurrentLevelNo - 1];
		for (int i = 0; i < CurrentLevelNo - 1; i++)
		{
			if (dicPlayedLevelScore.ContainsKey(i + 1))
			{
				array[i] = dicPlayedLevelScore[i + 1];
			}
			if (dicPlayedLevelStarPoint.ContainsKey(i + 1))
			{
				array2[i] = dicPlayedLevelStarPoint[i + 1];
			}
		}
		string value = JsonConvert.SerializeObject(array);
		string value2 = JsonConvert.SerializeObject(array2);
#if ENABLE_ANTI_CHEAT
		if (!string.IsNullOrEmpty(value))
		{
			ObscuredPrefs.SetString(KEY_HEADER_LEVEL_SCORE, value);
		}
		if (!string.IsNullOrEmpty(value2))
		{
			ObscuredPrefs.SetString(KEY_HEADER_LEVEL_STAR, value2);
		}
		SaveCurrentLevel();
		ObscuredPrefs.Save();
#else
        if (!string.IsNullOrEmpty(value))
        {
            SavesYG.SetString(KEY_HEADER_LEVEL_SCORE, value);
        }
        if (!string.IsNullOrEmpty(value2))
        {
            SavesYG.SetString(KEY_HEADER_LEVEL_STAR, value2);
        }
        SaveCurrentLevel();
		//PlayerPrefs.Save();
		YG2.SaveProgress();
#endif
    }

	public void SaveCurrentLevel()
	{
#if ENABLE_ANTI_CHEAT
		ObscuredPrefs.SetInt(KEY_LEVEL, CurrentLevelNo);
		ObscuredPrefs.SetBool(KEY_HEADER_ALL_CLEAR_LEVEL_TAG, AllLevelCleared);
#else
        SavesYG.SetInt(KEY_LEVEL, CurrentLevelNo);
        SavesYG.SetBool(KEY_HEADER_ALL_CLEAR_LEVEL_TAG, AllLevelCleared);
#endif
    }
}
