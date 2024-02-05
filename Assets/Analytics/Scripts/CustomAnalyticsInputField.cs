using TMPro;
using UnityEngine;

public class CustomAnalyticsInputField : MonoBehaviour
{
    private TMP_InputField attachedInputField;

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
        form.AddField("event_key", inputText); 
        form.AddField("position", positionJson);
        StartCoroutine(WebRequestHandler.PostToServer(form, AnalyticsContainer.customButtonAnalyticsURL, (response) => { }));
    }
}