using System.Collections.Generic;
using UnityEngine;

public class CustomManulEvent : MonoBehaviour
{
    public string keyName;
    public bool trackPosition = false;
    
    public void SendButtonEvents()
    {
        Vector3 position = gameObject.transform.position;

        Dictionary<string, string> formData = new Dictionary<string, string>();

        formData.Add("type_id", "1");
        formData.Add("event_key", keyName);

        if (trackPosition)
        {
            var positionData = new Dictionary<string, float>
            {
                {"x", position.x},
                {"y", position.y},
                {"z", position.z}
            };
            formData.Add("position", JsonUtility.ToJson(positionData));
        }

        formData.Add("value", "1");
        WebRequestHandler.PostToServerDirect(formData, AnalyticsContainer.customButtonAnalyticsURL);
    }
}
