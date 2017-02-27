using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager: MonoBehaviour {

    #region Public Fields

    [Tooltip("With extenstion")]
    public string localizationFileName = "enLoc.json";
    #endregion

    #region Private Fields
    private Dictionary<string, string> localizedText;
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

            Debug.Log("Locilized data loaded from " + fileName + ", " + localizedText.Count.ToString() + " entries");
        } else {
            Debug.LogError("No such localization!");
        }
    }
    #endregion
}
