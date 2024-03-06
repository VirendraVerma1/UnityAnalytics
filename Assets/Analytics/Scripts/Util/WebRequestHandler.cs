using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static  class WebRequestHandler
{
    public static IEnumerator PostToServer(WWWForm form, string posturl, Action<string> response)
    {
        if (!Application.isEditor)
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
                    //Debug.Log(www.error);
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

    private static Dictionary<string, UnityWebRequest> requestQueue = new Dictionary<string, UnityWebRequest>();

    // Method to process and send all queued requests
    public static void ProcessQueuedRequests()
    {
        foreach (var requestPair in requestQueue)
        {
            UnityWebRequest request = requestPair.Value;
            request.SendWebRequest();
            // You might want to handle the request's response or removal from the dictionary here
        }

        // Optionally, clear the queue after processing
        requestQueue.Clear();
    }

    // Modified PostToServerDirect method to enqueue requests if CustomerId is empty
    public static void PostToServerDirect(WWWForm form, string posturl)
    {
        if (!Application.isEditor)
        {
            if (string.IsNullOrEmpty(AnalyticsContainer.CustomerId))
            {
                // CustomerId is empty, enqueue the request
                string requestKey = posturl + System.DateTime.Now.Ticks; // Unique key for each request
                UnityWebRequest request = UnityWebRequest.Post(AnalyticsContainer.baseURL + posturl, form);
                requestQueue.Add(requestKey, request);
            }
            else
            {
                // Add required fields and send the request immediately
                ProcessQueuedRequests();
                AddRequiredFieldsAndSend(form, posturl);
            }
        }
    }

    // Method to add required fields and send the UnityWebRequest
    private static void AddRequiredFieldsAndSend(WWWForm form, string posturl)
    {
        form.AddField("user_id", AnalyticsContainer.CustomerId);
        form.AddField("customer_id", AnalyticsContainer.CustomerId);
        form.AddField("user_key", AnalyticsContainer.UserKey);
        form.AddField("app_key", AnalyticsContainer.AppKey);
        UnityWebRequest request = UnityWebRequest.Post(AnalyticsContainer.baseURL + posturl, form);
        request.SendWebRequest();
    }
}
