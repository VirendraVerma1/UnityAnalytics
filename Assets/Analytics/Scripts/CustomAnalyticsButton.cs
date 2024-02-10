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
        WWWForm form = new WWWForm();
        form.AddField("type_id",1);
        form.AddField("event_key",keyName);
        form.AddField("position", positionJson);
        form.AddField("value", 1);
        StartCoroutine(WebRequestHandler.PostToServer(form, AnalyticsContainer.customButtonAnalyticsURL, (response) => { }));
    }
}
