using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class musicvolume : MonoBehaviour
{
    public Slider slider;
    private AudioSource audioSource;
    private float savedVolume = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // �������� ������������ �������� ��������� ��� ��������� �� ���������
        if (PlayerPrefs.HasKey("Volumem"))
        {
            savedVolume = PlayerPrefs.GetFloat("Volumem");
        }
        else
        {
            PlayerPrefs.SetFloat("Volumem", savedVolume);
            PlayerPrefs.Save();
        }

        // ��������� ��������� �������� � ��������� �����
        slider.value = savedVolume;
        audioSource.volume = savedVolume;

        // ���������� ��������� ��� ��������� ���������
        slider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    public void ChangeVolume()
    {
        audioSource.volume = slider.value;
        savedVolume = slider.value;
        PlayerPrefs.SetFloat("Volumem", savedVolume);
        PlayerPrefs.Save();
    }
}
