using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class ClickControl : MonoBehaviour
{
    public static string obj_name;
    public static int count;// ���������� ������� 
    public Text text;
    public AudioSource audioSource;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnMouseDown()//��� �����
    {
        obj_name = gameObject.name;
        //Debug.Log(obj_name);

        if (count < 4)//���� ���������� ������� ������ 4�
        {
            audioSource.Play();//���� ������� �� ������
            Destroy(gameObject);//��������� ������
            count++;
            text.text = count.ToString();//����� ��������� �������
            
        }
    }

}
