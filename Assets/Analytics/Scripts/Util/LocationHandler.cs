using System.Collections;
using UnityEngine;

public class LocationHandler : MonoBehaviour
{
    public static LocationHandler instance;
    
    void Awake()
    {
        instance = this;
    }
    
    public IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)

            Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }
        
        Input.location.Stop();
    }

    public Vector2 GetLocation()
    {
        if(Application.isEditor)
            return new Vector2(26, 80);
        else
            return new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
    }
}