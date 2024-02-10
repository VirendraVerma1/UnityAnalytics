using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;

public static  class WebRequestHandler
{
    public static IEnumerator PostToServer(WWWForm form, string posturl, Action<string> response)
    {
        form.AddField("user_id",AnalyticsContainer.CustomerId);
        form.AddField("customer_id",AnalyticsContainer.CustomerId);
        form.AddField("user_key",AnalyticsContainer.UserKey);
        form.AddField("app_key",AnalyticsContainer.AppKey);
        using (UnityWebRequest www = UnityWebRequest.Post(AnalyticsContainer.baseURL+posturl, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
 
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                response?.Invoke("");
            }
            else
            {
                string responseText = www.downloadHandler.text;
                response?.Invoke(responseText);
            }
        }
    }

    
}
