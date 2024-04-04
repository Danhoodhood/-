using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform endMarker;
    public Transform player;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public Button restartButton;
    

    private bool hasWon = false;
    private bool hasLost = false;
    // �������� �����
    //public LoadingScreenManager loadingScreenManager;
    public string startSceneName = "Platformer"; // �����, ������� �� ������ ��������� ��� ������

    void Start()
    {
        // �������� LoadScene ����� Singleton
        if (LoadingScreenManager.Instance != null)
        {
            LoadingScreenManager.Instance.LoadScene(startSceneName);
        }
        else
        {
            Debug.LogError("LoadingScreenManager �� ���������������. ���������, ��� ��������� ������.");
        }
    }

    void Update()
    {
        // �������� ������� ��������� 1: ����� ���� ����
        if (player.position.y < 0f && !hasLost)
        {
            hasLost = true;
            ShowLoseMessage();
        }

        // �������� ������� ��������� 2: ����� ������ �� ������ ��������� ����
        if (hasLost && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        // �������� ������� ��������: �������� �� ��������� ���� �����
        if (Vector2.Distance(player.position, endMarker.position) < 1.5f && !hasWon)
        {
            ShowWinMessage();
        }
    }

    void ShowWinMessage()
    {
        winText.gameObject.SetActive(true);
    }

    void ShowLoseMessage()
    {
        loseText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
