using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomAnalyticsInputField : MonoBehaviour
{
    private TMP_InputField attachedInputField;
    [Tooltip("Enter your unique key name for this Button")]
    public string keyName;
    private void Start()
    {
        attachedInputField = GetComponent<TMP_InputField>();
        if (attachedInputField != null)
        {
            attachedInputField.onValueChanged.AddListener(SendInputEvents); 
        }
    }

    public void SendInputEvents(string inputText)
    {
        Vector3 position = gameObject.transform.position;
        Dictionary<string, string> postData = new Dictionary<string, string>();

        // Serialize the position to a dictionary instead of a JSON string
        Dictionary<string, float> positionDict = new Dictionary<string, float>
        {
            {"x", position.x},
            {"y", position.y},
            {"z", position.z}
        };

        // Add fields to the dictionary
        postData.Add("type_id", "2");
        postData.Add("event_key", keyName);
        postData.Add("position", positionDict.ToString()); // Add position as a nested dictionary
        postData.Add("value", inputText);

        WebRequestHandler.PostToServerDirect(postData, AnalyticsContainer.customButtonAnalyticsURL);
    }
}