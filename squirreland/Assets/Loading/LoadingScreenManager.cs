using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public static LoadingScreenManager Instance; // ����������� ������ �� ���������

    private void Awake()
    {
        // ���������, ��� ��������� ��� �� ��� ����������
        if (Instance == null)
        {
            Instance = this; // ������������� ��������� �������� �������
            DontDestroyOnLoad(gameObject); // �� ���������� ��� �������� ����� �����
        }
        else
        {
            Destroy(gameObject); // ���������� ������������� ������
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // ����������� �������� ��������
            loadingBar.value = progress;

            yield return null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        loadingScreen.SetActive(false);
    }
}
