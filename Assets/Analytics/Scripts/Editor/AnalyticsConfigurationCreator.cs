using UnityEngine;
using UnityEditor;

public class AnalyticsConfigurationCreator
{
    [MenuItem("Analytics/Setup")]
    public static void CreateAnalyticsConfiguration()
    {
        AnalyticsConfiguration asset = ScriptableObject.CreateInstance<AnalyticsConfiguration>();

        AssetDatabase.CreateAsset(asset, "Assets/AnalyticsConfiguration.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
