using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Monser
{

    //private float speed = 3.5f;//��������
    private Vector3 dir;//����������� ��������
    private SpriteRenderer sprite;//������(Sircle ������ �������2)


    [SerializeField]private float minX;
    [SerializeField]private float maxX;

    [SerializeField] private Animator anim;
    private void Start()
    {
        dir = transform.right;//��������� ����������� ��������
        lives = 2;

    }

    private void Update()
    {
        Move();
    }
   

    private void Move()
    {
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up*0.1f+ transform.right*dir.x*0.7f, 0.1f);
        
        //if (colliders.Length> 1) dir*= -1f;
        if (transform.position.x < minX) dir = transform.right;
        else if (transform.position.x > maxX) dir = -transform.right;

        transform.position =Vector3.MoveTowards(transform.position, transform.position+dir, Time.deltaTime);
        sprite.flipX = dir.x > 0.0f;
    }
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public override void GetDamage()
    {
        
        lives  -= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ���������, ��� ����������� � �������
        {
            float playerY = collision.gameObject.transform.position.y;
            float monsterY = transform.position.y;

            if (playerY > monsterY) // ���������, ��� ����� ��������� ���� �� ��� Y
            {
                // ����� ������� �� ������� ������, ������� ���� �������
                GetDamage();
                Debug.Log("����� ������� �� ������� ������, ���� ��������� �������.");

                if (lives < 1)
                    Die();
            }
            else
            {
                // ����� �������� ������� ����� ��� �����, ���� �� ��������� ������
                Player.Instance.GetDamage();
                anim.SetBool("isAttacking", true);
                Debug.Log("������ �������� ������ ����� ��� �����, �� ���� �� ���������.");
               // anim.SetBool("isAttacking", false);
            }
        }
    }

}
