using UnityEngine;
using UnityEditor;

public class AnalyticsConfigurationCreator
{
    [MenuItem("Analytics/Setup")]
    public static void CreateAnalyticsConfiguration()
    {
        // Define the asset path
        string assetPath = "Assets/AnalyticsConfiguration.asset";

        // Try to load the AnalyticsConfiguration asset from the specified path
        AnalyticsConfiguration asset = AssetDatabase.LoadAssetAtPath<AnalyticsConfiguration>(assetPath);

        // Check if the asset does not exist
        if (asset == null)
        {
            // Asset does not exist, so create it
            asset = ScriptableObject.CreateInstance<AnalyticsConfiguration>();
            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();
            Debug.Log("AnalyticsConfiguration asset created.");
        }
        else
        {
            Debug.Log("AnalyticsConfiguration asset already exists.");
        }

        // Focus the Project window and highlight the asset
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}