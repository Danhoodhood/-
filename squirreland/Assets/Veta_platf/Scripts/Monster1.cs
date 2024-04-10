using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster1 : Monser//�����

{
    
    
    private void Start()
    {
        lives = 2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ���������, ��� ����������� � �������
        {
            float playerY = collision.gameObject.transform.position.y;
            float monsterY = transform.position.y;

            if (playerY > monsterY) // ���������, ��� ����� ��������� ���� �� ��� Y
            {
                // ����� ������� �� ������� ������, �� ������� ����
                Debug.Log("����� ������� �� ������� ������, �� ����� �� ���������.");
            }
            else
            {
                // ����� �������� ������� ����� ��� �����, ������� ����
                Player.Instance.GetDamage();
                lives--;
                Debug.Log("� ������� " + lives + " ������");

                if (lives < 1)
                    Die();
            }
        }
    }



}
