using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomAnalyticsButton : MonoBehaviour
{
    private Button attachedButton;

    [Tooltip("Enter your unique key name for this Button")]
    public string keyName;
    private void Start()
    {
        attachedButton = GetComponent<Button>();
        if (attachedButton)
        {
            attachedButton.onClick.AddListener(SendButtonEvents);
        }
    }

    public void SendButtonEvents()
    {
        Vector3 position = gameObject.transform.position;
        string positionJson = $"{{\"x\":{position.x},\"y\":{position.y},\"z\":{position.z}}}";

        // Creating the dictionary
        var data = new Dictionary<string, string>
        {
            {"type_id", "1"},
            {"event_key", keyName},
            {"position", positionJson},
            {"value", "1"}
        };
        WebRequestHandler.PostToServerDirect(data, AnalyticsContainer.customButtonAnalyticsURL);
    }
}
