using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class diawitch2 : MonoBehaviour
{
    public GameObject dial;
    public GameObject player;
    public GameObject startbutton;
    public GameObject gg;
    public GameObject barmen;
    public GameObject music;
    public GameObject sound;
    public GameObject drink;
    public bool EndDia = false;
    public TextMeshProUGUI textDialog1;
    public TextMeshProUGUI textDialog2;
    private string text;
    private int count = 0;
    string[] dialogi = {
        "������:\n�� ��� ���� ��� ��������?",
        "��������:\n��� �� � ����� �� ����� ����������.",
        };


    void Start()
    {

        if (EndDia != true)
        {

            dial.SetActive(true);
            player.GetComponent<Player>().enabled = false;
            textDialog1.text = dialogi[0];







        }
    }
    void Update()
    {
        if (EndDia == true)
        {
            dial.SetActive(false);
            drink.SetActive(true);
            music.SetActive(false);
            sound.GetComponent<AudioSource>().Stop();
            text = "� ���� ��� ����� ����������� ��-������� ��� ��������� ��-�� ������� ����������� ���� �� ��� ���?";
            StartCoroutine(TextCoroutine2());
            Invoke("lastwords", 6);
            Invoke("sceneloade", 18);
            EndDia =false;




        }


        


    }
    public void lastwords()
    {
        textDialog2.text = "";
        text = "��� ����� �������� �������� ���� ����23:\n����� �. �.,\n �������� �. �., \n ������� �. ., \n ��������� �. �.";
        StartCoroutine(TextCoroutine2());
        Invoke("lastwords2", 6);
    }
    public void lastwords2()
    {
        textDialog2.text = "";
        text = "�������, ��� ������ ���� ����!";
        StartCoroutine(TextCoroutine2());
    }
    public void skip()
    {
        if (EndDia != true)
        {
            count++;
            textDialog1.text = "";
            if (count == 2)
            {
                EndDia = true;
            }

            if(count == 1) 
            {
                gg.SetActive(true);
                barmen.SetActive(false);
                textDialog1.text = dialogi[count];

            }




            





        }

    }
    
  public void sceneloade()
    {
        SceneManager.LoadScene("main_menu");
    }




    IEnumerator TextCoroutine2()
    {
        foreach (char abc in text)
        {
            textDialog2.text += abc;
            yield return new WaitForSeconds(0.05f);

        }
    }
}
