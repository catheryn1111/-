using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : AINavigation
{
    [SerializeField][Range(0, 360)] private float ViewAngle = 90f; // ���� ������ �����
    [SerializeField] private float ViewDistance = 15f; //��������� ������ �����      
    [SerializeField] private Transform Target; // ������ �� ������� ����� �������
    [SerializeField] float attackDistance, distance;
    [SerializeField] int damage;
    [SerializeField] float cooldown;
    private float timer;
    [SerializeField] Transform bulletPos;
    // [SerializeField] GameObject impact; // ��� particle ������������ ��������� �� �������
    [SerializeField] GameObject player; // ���� ���� � ������ �� ����/�������

    private bool IsInView()
    {
        //���� ����� ������ � ����������
        float currentAngle = Vector3.Angle(transform.forward, Target.position - transform.position);
        RaycastHit hit;
        //������� Raycast �� ����� � ������� �������� ��������� ��������� �� ���������� ���������� ViewDistance
        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, ViewDistance))
        {
            //���� ���� ����� ������ � ���������� ������ ���� �����������/2 � ���������� �� ��������� ������ ViewDistance  � ��� �������� ������ �� �����, � �� � ����������� ����� ����
            if (currentAngle < ViewAngle / 2f && Vector3.Distance(transform.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
            {
                return true; // �� bool IsInView = true
            }
        }
        return false; // ����� bool IsInView = false
    }
    private void MoveToTarget()
    {
        agent.isStopped = false;
        agent.speed = 3.5f;
        agent.SetDestination(Target.position);
    }
    override protected void Update() //������������ �������
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �� �4�3
        distance = Vector3.Distance(transform.position, player.transform.position); // �� �4�3
        Attack(); // �� �4�3
        DrawView();
        //���������� �� ����� �� ���������
        float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);
        if (IsInView()) // ���� ���� � ���� ���������
        {
            if (distanceToPlayer >= 2f) // ���� ���������� ������ 2 ������
                MoveToTarget(); // ���� ��������� � ���������
            else
            {
                agent.isStopped = true; // ���� ���� ������� ������ � ���������, �� ������������� ���
                anim.SetBool("Idle", true);
            }
        }
        else //���� ���� ������� �� ���� ���������
        {
            agent.isStopped = false; //���������� ����������� �������������           
            base.Update(); //��������� ������ �� ������� Update ��������� �������
        }
    }

    public void Attack() // �� �4�3
    {
        timer += Time.deltaTime;
        transform.LookAt(player.transform.position);
        if (distance < attackDistance && timer > cooldown)
        {
            RaycastHit hit;
            timer = 0;
            if (Physics.Raycast(bulletPos.position, transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.gameObject.GetComponent<PlayerController>().ChangeHealth(-damage);
                }
            }
        }
    }

    private void DrawView()
    {
        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, ViewAngle / 2f, 0)) * (transform.forward * ViewDistance);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, ViewAngle / 2f, 0)) * (transform.forward * ViewDistance);
        Debug.DrawLine(transform.position, left, Color.yellow);
        Debug.DrawLine(transform.position, right, Color.yellow);
    }
}
