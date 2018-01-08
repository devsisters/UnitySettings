using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        Settings.Installer.Install();
        Settings.Installer.Instance.AddView(new SampleListView());
    }

    void OnEnable()
    {
        StartCoroutine(CoroutineLog());
    }

    void LogSame()
    {
        for (var i = 0; i != 40; ++i)
            Debug.Log("some message some message some message");
    }

    void Log()
    {
        for (var i = 0; i != 40; ++i)
            Debug.Log(i + "some message some message some message");
    }

    private System.Collections.IEnumerator CoroutineLog()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log(Time.realtimeSinceStartup.ToString("0.000") + " coroutine log");
        }
    }
}
