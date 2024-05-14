using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bar2dialog : MonoBehaviour
{
    public GameObject dial;
    public GameObject player;
    public GameObject startbutton;
    public GameObject gg;
    public GameObject barmen;
    public GameObject perexod;
    public bool EndDia = false;
    public bool start = false;
    public TextMeshProUGUI textDialog1;
    private string text;
    private int count = 0;
    string[] dialogi = {
        "��������:\n ������, ��� �������, ��� ��� ����� ������ ����, ��� ��� ��� �����?",
        "���: \n������, ��� ����, �� �������� ���� ����� ������ ��� ����� ��������� �������� � ��������� ������������.",
        "��������:\n ����-�� �� ��� �����������, ����� �� ����� � �� ��������. ��� �� ��������� ��� ����� ����� ?",
        "���:\n ��, ������ ��� ������� ����� �� ������ ����� ����, � ����� ���������."
        };



    void Update()
    {
        if (EndDia == true)
        {
            dial.SetActive(false);
            startbutton.SetActive(false);
            player.GetComponent<Player>().enabled = true;
            perexod.SetActive(true);




        }





    }
    public void skip()
    {
        if (EndDia != true )
        {
            count++;
            textDialog1.text = "";
            if (count == 4)
            {
                EndDia = true;
            }

            if (count == 1)
            {
                gg.SetActive(false);
                barmen.SetActive(true);
                textDialog1.text = dialogi[count];
      
            }
            if (count == 2)
            {

                gg.SetActive(true);
                barmen.SetActive(false);
                textDialog1.text = dialogi[count];

            }
            if (count == 3)
            {
                gg.SetActive(false);
                barmen.SetActive(true);
                textDialog1.text = dialogi[count];

            }






            start = false;


        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (EndDia != true)
        {
            startbutton.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {

        startbutton.SetActive(false);

    }
    public void startdialog()
    {

        if (EndDia != true)
        {

            dial.SetActive(true);
            player.GetComponent<Player>().enabled = false;
            textDialog1.text = dialogi[0];







        }
    }




}
