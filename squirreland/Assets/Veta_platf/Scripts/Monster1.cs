using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster1 : Monser//�����

{
    //[SerializeField] private AudioSource audioSourceDieMonster;
    [SerializeField] private Animator anim;

    private void Start()
    {
        lives = 1;
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
                Debug.Log("����� ������� �� ������� ������, ���� ��������� �������.");
                lives--;
                Debug.Log("� ������� " + lives + " ������");
                //
;
                if (lives < 1)
                {
                    //audioSourceDieMonster.Play();
                    Die();
                }
            }
            else
            {
                // ����� �������� ������� ����� ��� �����, ������� ����
                Player.Instance.GetDamage();
                anim.SetBool("isAttacking", true);
                Debug.Log("����� ������� ����");
                anim.SetBool("isAttacking", false);

            }
        }
    }



}
