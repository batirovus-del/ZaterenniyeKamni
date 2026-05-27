using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance;

    public bool secondFortune = false;

    [SerializeField]
    private string leaderboardName = "Leaderboard";

    public void Awake()
    {
        if (LeaderBoardManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PauseSound()
    {
        if (MonoSingleton<PlayerDataManager>.Instance.IsOnSoundBGM)
        {
            SoundManager.SetVolumeMusic(0f);
        }
        if (!MonoSingleton<PlayerDataManager>.Instance.IsOnSoundEffect)
        {
            SoundManager.SetVolumeSFX(0f);
        }
        AudioListener.volume = 0f;
        Time.timeScale = 0f;
    }

    public void ResumeSound()
    {
        if (MonoSingleton<PlayerDataManager>.Instance.IsOnSoundBGM)
        {
            SoundManager.SetVolumeMusic(1f);
        }
        if (!MonoSingleton<PlayerDataManager>.Instance.IsOnSoundEffect)
        {
            SoundManager.SetVolumeSFX(1f);
        }
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
    }

    public void NewScore(int score)
    {
        YG2.SetLeaderboard(leaderboardName, score);
    }
}
