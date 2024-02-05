using UnityEngine;

[CreateAssetMenu(fileName = "AnalyticsConfiguration", menuName = "Analytics/Setup", order = 1)]
public class AnalyticsConfiguration : ScriptableObject
{
    public string UserKey;
    public string AppKey;
}