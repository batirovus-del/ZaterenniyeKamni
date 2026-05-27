using System;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int HighScore;
        public string json;
        public static Dictionary<string, object> local_dictionary;

        public static SavesYG Instance;

        public SavesYG()
        {
            ResetSave();
        }

        public void ResetSave()
        {
            json = string.Empty;
            HighScore = 0;
        }

        public void Save()
        {
            YG2.SaveProgress();
        }

        public void LoadSave()
        {
            local_dictionary = new Dictionary<string, object>();
        }

        public void SetNewHighscore()
        {
            HighScore++;
            YG2.SetLeaderboard("Leaderboard", HighScore);
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static void SetInt(string key, int writeValue, bool important = true)
        {
            PlayerPrefs.SetInt(key, writeValue);
            PlayerPrefs.Save();
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        public static void SetBool(string key, bool writeValue, bool important = true)
        {
            PlayerPrefs.SetInt(key, writeValue ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static float GetFloat(string key, float defaultValue = 0.0F)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static void SetFloat(string key, float writeValue, bool important = true)
        {
            PlayerPrefs.SetFloat(key, writeValue);
            PlayerPrefs.Save();
        }

        public static string GetString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static void SetString(string key, string writeValue, bool important = true)
        {
            PlayerPrefs.SetString(key, writeValue);
            PlayerPrefs.Save();
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
