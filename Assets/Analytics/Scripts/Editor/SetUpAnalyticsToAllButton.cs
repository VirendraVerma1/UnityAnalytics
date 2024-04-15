using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SetUpAnalyticsToAllButton : EditorWindow
{
    
    
    [MenuItem("Analytics/Setup button analytics for all buttons")]
    private static void AnalyzeScenes()
    {
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                Scene loadedScene = EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Single);
                GameObject[] rootObjects = loadedScene.GetRootGameObjects();
                foreach (GameObject obj in rootObjects)
                {
                    AddCustomAnalyticsToButtons(obj);
                }
                EditorSceneManager.SaveScene(loadedScene);
            }
        }
        Debug.Log("Completed analysis and modification of all scenes.");
    }

    private static void AddCustomAnalyticsToButtons(GameObject obj)
    {
        Button[] buttons = obj.GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            CustomAnalyticsButton customAnalytics = button.gameObject.GetComponent<CustomAnalyticsButton>();
            if (customAnalytics == null)
            {
                button.gameObject.AddComponent<CustomAnalyticsButton>();
            }
        }
    }
}