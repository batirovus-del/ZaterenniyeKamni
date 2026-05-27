using UnityEngine;
using System.Collections;
using YG;

namespace EasyMobile.Internal
{
    using StoringSystem = YG2;
    using System;

    /// <summary>
    /// EM's internal storage system for persistent data, used to add
    /// a level of abstraction between the consuming code and the actual
    /// underlaying storing system, which is Unity's PlayerPrefs as of now
    /// but we can change that if needed.
    /// </summary>
    internal static class StorageUtil
    {
        /// <summary>
        /// Removes all keys and values from the persistent data storage. Use with caution.
        /// </summary>
        public static void DeleteAll()
        {
            SavesYG.DeleteAll();
        }

        /// <summary>
        /// Removes key and its corresponding value from the persistent data storage.
        /// </summary>
        /// <param name="key">Key.</param>
        public static void DeleteKey(string key)
        {
            SavesYG.DeleteKey(key);
        }

        /// <summary>
        /// Returns the value corresponding to key in the persistent data storage if it exists.
        /// If it doesn't exist, it will return defaultValue.
        /// </summary>
        /// <returns>The float.</returns>
        /// <param name="key">Key.</param>
        public static float GetFloat(string key, float defaultValue)
        {
            return SavesYG.GetFloat(key, defaultValue);
        }

        /// <summary>
        /// Returns the value corresponding to key in the persistent data storage if it exists.
        /// If it doesn't exist, it will return defaultValue.
        /// </summary>
        /// <returns>The int.</returns>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        public static int GetInt(string key, int defaultValue)
        {
            return SavesYG.GetInt(key, defaultValue);
        }

        /// <summary>
        /// Returns the value corresponding to key in the persistent data storage if it exists.
        /// If it doesn't exist, it will return defaultValue.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        public static string GetString(string key, string defaultValue)
        {
            return SavesYG.GetString(key, defaultValue);
        }

        /// <summary>
        /// Returns true if key exists in the persistent data storage.
        /// </summary>
        /// <returns><c>true</c> if key exists; otherwise, <c>false</c>.</returns>
        /// <param name="key">Key.</param>
        public static bool HasKey(string key)
        {
            return SavesYG.HasKey(key);
        }

        /// <summary>
        /// Writes all modified keys to disk.
        /// As of current implementation this invokes <c>PlayerPrefs.Save()</c> internally.
        /// </summary>
        public static void Save()
        {
            YG2.SaveProgress();
        }

        /// <summary>
        /// Sets the value associated with key in the persistent data storage.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void SetFloat(string key, float value)
        {
            SavesYG.SetFloat(key, value);
        }

        /// <summary>
        /// Sets the value associated with key in the persistent data storage.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void SetInt(string key, int value)
        {
            SavesYG.SetInt(key, value);
        }

        /// <summary>
        /// Sets the value associated with key in the persistent data storage.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void SetString(string key, string value)
        {
            SavesYG.SetString(key, value);
        }

        /// <summary>
        /// Stores a <see cref="DateTime"/> value as string to the persistent storage.
        /// </summary>
        /// <param name="time">Time.</param>
        /// <param name="ppkey">Key to store the value.</param>
        public static void SetTime(string ppkey, DateTime time)
        {
            SavesYG.SetString(ppkey, time.ToBinary().ToString());
        }

        /// <summary>
        /// Gets the stored string in the persistent storage, converts it to a <see cref="DateTime"/> and returns.
        /// If no value was stored previously, the given default time is returned.
        /// </summary>
        /// <returns>The time.</returns>
        /// <param name="ppkey">Key to retrieve the value.</param>
        public static DateTime GetTime(string ppkey, DateTime defaultTime)
        {
            string storedTime = SavesYG.GetString(ppkey, string.Empty);

            if (!string.IsNullOrEmpty(storedTime))
                return DateTime.FromBinary(Convert.ToInt64(storedTime));
            else
                return defaultTime;
        }
    }
}