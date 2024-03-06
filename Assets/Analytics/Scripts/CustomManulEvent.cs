using UnityEngine;

public class CustomManulEvent : MonoBehaviour
{
    public string keyName;
    public bool trackPosition = false;
    
    public void SendButtonEvents()
    {
        Vector3 position = gameObject.transform.position;
       
        WWWForm form = new WWWForm();
        form.AddField("type_id",1);
        form.AddField("event_key",keyName);
        if(trackPosition)
            form.AddField("position", $"{{\"x\":{position.x},\"y\":{position.y},\"z\":{position.z}}}");
        form.AddField("value", 1);
        WebRequestHandler.PostToServerDirect(form, AnalyticsContainer.customButtonAnalyticsURL);
    }
}
