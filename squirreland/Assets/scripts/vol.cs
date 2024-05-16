using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class vol : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    private float savedVolume = 1f;

    void Start()
    {
        // �������� ������������ �������� ��������� ��� ��������� �� ���������
        if (PlayerPrefs.HasKey("Volume"))
        {
            savedVolume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", savedVolume);
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
        PlayerPrefs.SetFloat("Volume", savedVolume);
        PlayerPrefs.Save();
    }
}
