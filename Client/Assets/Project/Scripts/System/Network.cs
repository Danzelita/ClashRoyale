using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{
    #region Singleton
    public static Network Instance {get; private set;}
    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    
    public void Post(string url, Dictionary<string, string> data, Action<string> succes, Action<string> error) =>
        StartCoroutine(PostProcess(url, data, succes, error));

    private IEnumerator PostProcess(string url, Dictionary<string, string> data, Action<string> succes, Action<string> error)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, data))
        {
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
                error?.Invoke(www.error);
            else
                succes?.Invoke(www.downloadHandler.text);
        }
    }
}
