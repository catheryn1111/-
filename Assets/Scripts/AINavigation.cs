using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
    protected NavMeshAgent agent; // ��������� ��� �������� ������������ 
    [SerializeField] List<Transform> points = new List<Transform>();
    protected Animator anim;  // ��������� ��� �������� ������������ 


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetDestination();

    }

    virtual protected void Update()  // ��������� ��� �������� ������������ 
    {
        if (agent.remainingDistance < 0.25f)
        {
            StartCoroutine("Idle");
        }
    }
    public void SetDestination()
    {
        Vector3 newTarget = points[Random.Range(0, points.Count)].position;
        agent.SetDestination(newTarget);
    }

    public void FearStart()
    {
        StartCoroutine("Fear");
    }

    IEnumerator Idle()
    {
        agent.speed = 0; // ������������� NPC
        SetDestination(); // ������ ����� �����
        anim.SetBool("idle", true); // ��������� ��������� �������� � Idle
        yield return new WaitForSeconds(2); //������� 2 ������(����� ������ ���������� ���
        agent.speed = 3.5f; // ���������� �������� ������
        anim.SetBool("idle", false); //��������� �������� ������� � ������      
    }

    IEnumerator Fear()
    {
        agent.speed = 10; // �������� NPC
        anim.SetBool("fear", true); // ��������� ��������� �������� � fear
        SetDestination(); // ������ ����� �����
        yield return new WaitForSeconds(5); //������� 5 ������(����� ������ ���������� ���)
        agent.speed = 3.5f; // ���������� �������� ������
        anim.SetBool("fear", false); //��������� �������� ������� � ������     
    }

}
