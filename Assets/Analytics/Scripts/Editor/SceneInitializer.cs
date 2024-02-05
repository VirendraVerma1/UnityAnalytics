using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : EditorWindow
{
    [MenuItem("Analytics/Initialize Base Analytics")]
    public static void InitFirstScene()
    {
        // Load the first scene from the build settings
        if (EditorBuildSettings.scenes.Length == 0)
        {
            Debug.LogError("No scenes are present in the build settings.");
            return;
        }

        string firstScenePath = EditorBuildSettings.scenes[0].path;
        Scene firstScene = EditorSceneManager.OpenScene(firstScenePath, OpenSceneMode.Additive);

        if (firstScene.IsValid())
        {
            // Check if any GameObject already has a BasicAnalyticsManager component
            BasicAnalyticsManager[] managers = Object.FindObjectsOfType<BasicAnalyticsManager>();
            if (managers.Length > 0)
            {
                Debug.LogError("A GameObject with BasicAnalyticsManager already exists in the scene.");
                return;
            }

            // Create a new GameObject
            GameObject analyticsManager = new GameObject("AnalyticsManager");

            // Add BasicAnalyticsManager script to the GameObject
            BasicAnalyticsManager managerComponent = analyticsManager.AddComponent<BasicAnalyticsManager>();

            // Find the AnalyticsConfiguration asset
            string[] guids = AssetDatabase.FindAssets("t:AnalyticsConfiguration");
            if (guids.Length == 0)
            {
                Debug.LogError("AnalyticsConfiguration asset not found.");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            AnalyticsConfiguration configAsset = AssetDatabase.LoadAssetAtPath<AnalyticsConfiguration>(path);

            // Assign the configuration to the BasicAnalyticsManager
            managerComponent.config = configAsset;

            // Move the GameObject to the first scene
            SceneManager.MoveGameObjectToScene(analyticsManager, firstScene);

            // Save the changes to the scene
            EditorSceneManager.SaveScene(firstScene);
            Debug.Log("AnalyticsManager GameObject with config has been added to the scene: " + firstScene.name);
        }
        else
        {
            Debug.LogError("Failed to load the first scene from the build settings.");
        }
    }
}
