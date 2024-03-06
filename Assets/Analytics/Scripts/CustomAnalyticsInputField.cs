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
        string positionJson = $"{{\"x\":{position.x},\"y\":{position.y},\"z\":{position.z}}}";
        WWWForm form = new WWWForm();
        form.AddField("type_id", 2);
        form.AddField("event_key", keyName); 
        form.AddField("position", positionJson);
        form.AddField("value", inputText);
        WebRequestHandler.PostToServerDirect(form, AnalyticsContainer.customButtonAnalyticsURL);
    }
}