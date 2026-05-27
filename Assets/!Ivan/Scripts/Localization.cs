using System.Collections.Generic;
using UnityEngine;

public static class Localization
{
    public static Dictionary<string, string> Translations;

    public static void InitTranslations()
    {
        string lang = GetLanguage();

        if (lang == "en")
        {
            Translations = new()
            {
                { "Level", "Level" },
                { "NEXT", "NEXT" },
                { "PLAY", "PLAY" },
                { "SETTINGS", "SETTINGS" },
                { "CLAIM", "CLAIM" },
                { "LOADING...", "LOADING..." },
                { "CONTINUE", "CONTINUE" },
            };
        }
        else if (lang == "tr")
        {
            Translations = new()
            {
                { "Level", "Seviye" },
                { "NEXT", "SONRAKİ" },
                { "PLAY", "OYNA" },
                { "SETTINGS", "Ayarlar" },
                { "Sound", "Ses" },
                { "Music", "Müzik" },
                { "Push", "İTMEK" },
            };
        }
        else
        {
            Translations = new()
            {
                { "Level", "Уровень" },
                { "NEXT", "ДАЛЬШЕ" },
                { "PLAY", "ИГРАТЬ" },
                { "SETTINGS", "НАСТРОЙКИ" },
                { "CLAIM", "ЗАБРАТЬ" },
                { "LOADING...", "ЗАГРУЗКА..." },
                { "CONTINUE", "ПРОДОЛЖИТЬ" },
            };
        }
    }

    private static string GetLanguage()
    {
        string lang = PlayerPrefs.GetString("lang_YG2", string.Empty);
        if (!string.IsNullOrEmpty(lang))
            return lang;

        switch (Application.systemLanguage)
        {
            case SystemLanguage.English: return "en";
            case SystemLanguage.Turkish: return "tr";
            default: return "ru";
        }
    }
}
