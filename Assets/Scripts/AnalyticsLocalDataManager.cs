using UnityEngine;
using YG;

public class AnalyticsLocalDataManager : MonoSingleton<AnalyticsLocalDataManager>
{
	private readonly string PrefsKey = "Analytics";

	public AnalyticsLocalData Data = new AnalyticsLocalData();

	private bool dirtyValue;

	public override void Awake()
	{
		base.Awake();
		string @string = SavesYG.GetString(PrefsKey, string.Empty);
		if (!string.IsNullOrEmpty(@string))
		{
			Data = JsonUtility.FromJson<AnalyticsLocalData>(@string);
		}
	}

	public void Save()
	{
		dirtyValue = true;
	}

	private void LateUpdate()
	{
		if (dirtyValue)
		{
			dirtyValue = false;
            SavesYG.SetString(PrefsKey, JsonUtility.ToJson(Data));
		}
	}
}
