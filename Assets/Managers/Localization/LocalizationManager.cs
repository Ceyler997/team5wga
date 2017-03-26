using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager: MonoBehaviour {//TODO: move to static whole class

    #region Public Fields

    [Tooltip("With extenstion")]
    public string localizationFileName = "enLoc.json";
    #endregion

    #region Private Fields
    private static Dictionary<string, string> localizedText;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        DontDestroyOnLoad(this);

        LoadLocilizedText(localizationFileName);
    }
    #endregion

    #region Public Methods

    //HEAVY, DON'T USE TOO MUCH
    public void LoadLocilizedText(string fileName) {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int dataCount = 0; dataCount < loadedData.items.Length; ++dataCount) {
                localizedText.Add(loadedData.items [dataCount].key,
                    loadedData.items [dataCount].value);
            }

            Debug.Log("Localized data loaded from " + fileName + ", " + localizedText.Count.ToString() + " entries");
        } else {
            Debug.LogError("No such localization!");
        }
    }

    public static string getTextByKey(string key) {
        string result;

        if(localizedText == null) {
            Debug.LogError("Localization not loaded!");
            return "NOT LOADED";
        }

        if (localizedText.TryGetValue(key, out result) == false) {
            Debug.LogError("No localization data for " + key + " key");
            return "NO DATA";
        }

        return result;
    }
    #endregion
}
