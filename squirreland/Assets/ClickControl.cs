using UnityEngine;
using UnityEngine.UI;

public class ClickControl : MonoBehaviour
{
    public static string obj_name;
    public static int count;// ���������� ������� 
    public Text text;
    public AudioSource audioSource;
    public GameObject pauseMenu;

    void Start()
    {
        timer_for_hide.startTime = 60;
        Time.timeScale = 1;
    }

    void Update()
    {

    }

    void OnMouseDown()//��� �����
    {
        if (!pauseMenu.activeSelf)
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

}
