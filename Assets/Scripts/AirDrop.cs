using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class AirDrop : MonoBehaviour
{
    [SerializeField] int airRes = 8;
    [SerializeField] bool isLanded = false;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] int deploymentHeight = 6;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; //объект, в который попал луч
        Ray ray = new Ray(transform.position, Vector3.down); // направление луча вниз
        Debug.DrawRay(transform.position, -transform.up, Color.red); // рисуем луч для проверки
        if (!isLanded) //если парашют не раскрывался
        {
            if (Physics.Raycast(ray, out hit, deploymentHeight)) // если луч задел коллайдер на расстоянии deploymentHeight
            {
                if (hit.collider.tag == "Enviroment") // если тэг затронутого коллайдер = Enviroment
                {
                    OpenParachute(); // открываем парашют
                }
            }
        }
    }

    public void OpenParachute()
    {
        isLanded = true;
        rb.drag = airRes;
        anim.SetTrigger("open");
    }

    private void OnCollisionEnter(Collision collision) // если объект коснется любого коллайдера
    {
        anim.SetTrigger("close"); // включим анимацию закрытия парашюта
    }
}
