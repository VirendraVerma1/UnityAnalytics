using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BasicAnalyticsManager : MonoBehaviour
{
    public AnalyticsConfiguration config;

    public static BasicAnalyticsManager instance;

    void Awake()
    {
        if (instance == null || instance == this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (config != null)
        {
            AnalyticsContainer.UserKey = config.UserKey;
            AnalyticsContainer.AppKey = config.AppKey;
            
            if (PlayerPrefs.HasKey("CustomerId")) 
            {
                AnalyticsContainer.CustomerId = PlayerPrefs.GetString("CustomerId");
            }
            else
            {
                gameObject.AddComponent<LocationHandler>();
                StartCoroutine(LocationHandler.instance.StartLocationService());
                if (AnalyticsContainer.CustomerId == "")
                {
                    print("have customer id");
                    Invoke("CreateNewUser",3);
                }
                else
                {
                    print("dont have customer id");
                    //send the analytics for punching in
                    SendSessionStartData(0);
                }
            }
            
        }
        else
        {
            Debug.LogError("Analytics configuration is not set!");
            return;
        }
    }
    
    void Start()
    {
        StartCoroutine(StartTimer());
        SceneManager.activeSceneChanged += MySceneChanged;
    }
    
    //this class sends timer on every scene change
    private int secCounter = 0;
    private bool isPause = false;

    IEnumerator StartTimer()
    {
        secCounter = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(!isPause)
                secCounter += 1;
        }
    }

    void MySceneChanged(Scene myScene,Scene anotherScene)
    {
        SendSessionStartData(0);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            isPause = true;
        else
            isPause = false;
        SendSessionStartData(0);
    }

    private void OnApplicationQuit()
    {
        SendSessionStartData(1);
    }

    void CreateNewUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("customer_name","Player"+Random.Range(111111,99999999));
        form.AddField("lat",LocationHandler.instance.GetLocation().x.ToString());
        form.AddField("lon",LocationHandler.instance.GetLocation().y.ToString());
        form.AddField("device_name",SystemInfo.deviceName);
        form.AddField("app_version",Application.version);
        StartCoroutine(WebRequestHandler.PostToServer(form, AnalyticsContainer.createCustomerURL, (response) =>
        {
            print(response);
            if (response != null&& response!="")
            {
                
                AnalyticsContainer.CustomerId = response;
                PlayerPrefs.SetString("CustomerId",AnalyticsContainer.CustomerId);
                SendSessionStartData(0);
            }
        }));
    }

    void SendSessionStartData(int session)
    {
        WWWForm form = new WWWForm();
        form.AddField("scene_name",SceneManager.GetActiveScene().name);
        form.AddField("scene_duration",secCounter.ToString());
        form.AddField("started_event",session);
        print("Scene Duration = "+secCounter.ToString()+"|"+session);
        StartCoroutine(WebRequestHandler.PostToServer(form, AnalyticsContainer.baseAalyticsURL, (response) =>
        {
            if (response != null&& response!="")
            {
                secCounter = 0;
            }
        }));
    }
}