using System;
using Firebase.RemoteConfig;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private string _buttonColor;

    private void Start()
    {
        StartCoroutine(GetColor());
    }

    IEnumerator GetColor()
    {
        FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        
        Task fetchTask = remoteConfig.FetchAsync(TimeSpan.Zero);

        yield return new WaitUntil(() => fetchTask.IsCompleted);

        if (fetchTask.Exception != null)
        {
            Debug.LogError($"Can't fetch remote config: {fetchTask.Exception}");
            yield break;
        }
        
        remoteConfig.ActivateAsync();

        _buttonColor = FirebaseRemoteConfig.DefaultInstance.GetValue("button_color").StringValue;

        if (_buttonColor == "red")
        {
            _button.image.color = Color.red;
        }
        else if (_buttonColor == "green")
        {
            _button.image.color = Color.green;
        }
    }
}