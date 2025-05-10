using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : AINavigation
{
    [SerializeField][Range(0, 360)] private float ViewAngle = 90f; // угол обзора врага
    [SerializeField] private float ViewDistance = 15f; //дальность обзора врага      
    [SerializeField] private Transform Target; // объект за которым будем следить
    [SerializeField] float attackDistance, distance;
    [SerializeField] int damage;
    [SerializeField] float cooldown;
    private float timer;
    [SerializeField] Transform bulletPos;
    // [SerializeField] GameObject impact; // для particle столкновения рейкастом по желанию
    [SerializeField] GameObject player; // либо ищем в старте по тэгу/скрипту

    private bool IsInView()
    {
        //угол между врагом и персонажем
        float currentAngle = Vector3.Angle(transform.forward, Target.position - transform.position);
        RaycastHit hit;
        //Пускаем Raycast от врага в сторону текущего положения персонажа на расстояние переменной ViewDistance
        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, ViewDistance))
        {
            //если угол между врагом и персонажем меньше угла обнаружения/2 И расстояние до персонажа меньше ViewDistance  И луч врезался именно во врага, а не в препятствие между ними
            if (currentAngle < ViewAngle / 2f && Vector3.Distance(transform.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
            {
                return true; // то bool IsInView = true
            }
        }
        return false; // иначе bool IsInView = false
    }
    private void MoveToTarget()
    {
        agent.isStopped = false;
        agent.speed = 3.5f;
        agent.SetDestination(Target.position);
    }
    override protected void Update() //переписываем функцию
    {
        player = GameObject.FindGameObjectWithTag("Player"); // ДЗ М4У3
        distance = Vector3.Distance(transform.position, player.transform.position); // ДЗ М4У3
        Attack(); // ДЗ М4У3
        DrawView();
        //расстояние от врага до персонажа
        float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);
        if (IsInView()) // если цель в зоне видимости
        {
            if (distanceToPlayer >= 2f) // если расстояние больше 2 единиц
                MoveToTarget(); // враг двигается к персонажу
            else
            {
                agent.isStopped = true; // если враг подошел близко к персонажу, то останавливаем его
                anim.SetBool("Idle", true);
            }
        }
        else //если цель пропала из зоны видимости
        {
            agent.isStopped = false; //возвращаем возможность передвигаться           
            base.Update(); //добавляем строки из функции Update основного скрипта
        }
    }

    public void Attack() // ДЗ М4У3
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
