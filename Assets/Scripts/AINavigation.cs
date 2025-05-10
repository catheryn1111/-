using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
    protected NavMeshAgent agent; // добавляем для будущего наследования 
    [SerializeField] List<Transform> points = new List<Transform>();
    protected Animator anim;  // добавляем для будущего наследования 


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetDestination();

    }

    virtual protected void Update()  // добавляем для будущего наследования 
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
        agent.speed = 0; // останавливаем NPC
        SetDestination(); // задаем новую точку
        anim.SetBool("idle", true); // переводим состояние анимации в Idle
        yield return new WaitForSeconds(2); //ожидаем 2 секунд(время можете установить своё
        agent.speed = 3.5f; // возвращаем скорость агенту
        anim.SetBool("idle", false); //переводим анимацию обратно в ходьбу      
    }

    IEnumerator Fear()
    {
        agent.speed = 10; // ускоряем NPC
        anim.SetBool("fear", true); // переводим состояние анимации в fear
        SetDestination(); // задаем новую точку
        yield return new WaitForSeconds(5); //ожидаем 5 секунд(время можете установить своё)
        agent.speed = 3.5f; // возвращаем скорость агенту
        anim.SetBool("fear", false); //переводим анимацию обратно в ходьбу     
    }

}
