using UnityEditor;
using UnityEngine;

public class DeletePlayPrefData : MonoBehaviour
{
    [MenuItem("Analytics/Clear Player Pref Data")]
    public static void  DeletePlayerPrefData()
    {
        PlayerPrefs.DeleteAll();
    }
}
