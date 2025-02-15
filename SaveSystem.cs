using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;

[InitializeOnLoad]
public static class SaveSystem
{
    private static DateTime lastSaveTime = DateTime.UtcNow;
    private const double saveInterval = 1.0; // Dakika cinsinden

    static SaveSystem()
    {
        EditorApplication.update += () =>
        {
            if (Application.isPlaying) return;

            if ((DateTime.UtcNow - lastSaveTime).TotalMinutes >= saveInterval)
            {
                SaveProject();
                lastSaveTime = DateTime.UtcNow;
            }
        };
    }

    private static void SaveProject()
    {
        if (Application.isPlaying) return;

        var activeScene = EditorSceneManager.GetActiveScene();
        if (activeScene.isDirty)
        {
            Debug.Log("Auto-saving scene and project...");
            EditorSceneManager.SaveScene(activeScene);
            AssetDatabase.SaveAssets();
        }
    }
}
