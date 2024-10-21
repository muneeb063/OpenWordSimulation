using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.DemiEditor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// Helper Utility Class to quickly go through mostly used Actions.
/// </summary>
public class HelperUtility : EditorWindow
{
    private string gameDataFilePath;

    private const string filePathSave = "FilePathSave";

    [MenuItem("Utilities/Helper Utility")]
    static void Init()
    {
        HelperUtility window = (HelperUtility)EditorWindow.GetWindow(typeof(HelperUtility));
        window.Show();
    }

    private static void ExecuteAfterPlay()
    {
        Debug.Log("Start to wait");
    }

    private void OnGUI()
    {
        SceneChanger();

        GUILayout.Space(10f);

        SaveDataHelper();

        EditorGUILayout.Space(10f);

        GameDataResetter();
    }


    void SceneChanger()
    {
        EditorGUILayout.LabelField("Scene Changer");

        foreach (var scene in EditorBuildSettings.scenes)
        {
            EditorGUILayout.BeginHorizontal();
            if (EditorSceneManager.GetActiveScene().name == scene.path.FileOrDirectoryName())
                GUI.enabled = false;
            else
            {
                GUI.enabled = true;
            }

            if (GUILayout.Button(scene.path.FileOrDirectoryName()))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    if (EditorUtility.DisplayDialog("Change Scene",
                            "Your current Scene isn't saved. Any unchanged progress will be lost. Do you want to Save?",
                            "YES", "NO"))
                    {
                        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
                    }
                }

                EditorSceneManager.OpenScene(scene.path);

            }

            GUI.enabled = true;

            if (GUILayout.Button("Select"))
            {
                EditorGUIUtility.PingObject(AssetDatabase.LoadMainAssetAtPath(scene.path));
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    void SaveDataHelper()
    {
        GUILayout.Label("Save Data Helper");

        if (PlayerPrefs.HasKey("AchievementsSaveData"))
            GUI.enabled = true;
        else
            GUI.enabled = false;

        if (GUILayout.Button("Clear Player Prefs"))
        {
            if (EditorUtility.DisplayDialog("Clear Player Prefs", "Are you sure you want to delete all saced data?", "Yes", "No"))
            {
                PlayerPrefs.DeleteAll();

                PlayerPrefs.SetString(filePathSave, gameDataFilePath);
                PlayerPrefs.Save();
            }
        }

        GUI.enabled = true;

        if (GUILayout.Button("Open Persistent Data Path"))
        {
            Application.OpenURL(Application.persistentDataPath);
        }

        string filePath = Application.persistentDataPath + "/SaveData.txt";

        if (File.Exists(filePath))
            GUI.enabled = true;
        else
            GUI.enabled = false;

        if (GUILayout.Button("Delete SaveData file"))
        {
            if (EditorUtility.DisplayDialog("Delete Saved Data", "Are you sure you want to delete all saved data?", "Yes", "No"))
            {
                File.Delete(Application.persistentDataPath + "/SaveData.txt");
            }
        }

        GUI.enabled = true;
    }

    void GameDataResetter()
    {
        EditorGUILayout.LabelField("Game Data Resetter");
        EditorGUILayout.LabelField(gameDataFilePath);

        if (gameDataFilePath == "" && PlayerPrefs.HasKey(filePathSave))
            gameDataFilePath = PlayerPrefs.GetString(filePathSave);

        if (GUILayout.Button("Choose File Path"))
        {
            gameDataFilePath = EditorUtility.OpenFolderPanel("Load Game Data Folder", "", "");
            PlayerPrefs.SetString(filePathSave, gameDataFilePath);
            PlayerPrefs.Save();
        }

        if (GUILayout.Button("Reset All Saved Data"))
        {
            string[] objects = Directory.GetFiles(gameDataFilePath);

            foreach (var _object in objects)
            {
                if (_object.EndsWith(".asset"))
                {
                    string relativePath = "";

                    if (_object.StartsWith(Application.dataPath))
                    {
                        relativePath = "Assets" + _object.Substring(Application.dataPath.Length);
                    }





                }
            }

            if (EditorUtility.DisplayDialog("Reset All Data", "Data cleared", "Ok"))
            {

            }
        }
    }
}
