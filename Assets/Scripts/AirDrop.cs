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
        RaycastHit hit; //������, � ������� ����� ���
        Ray ray = new Ray(transform.position, Vector3.down); // ����������� ���� ����
        Debug.DrawRay(transform.position, -transform.up, Color.red); // ������ ��� ��� ��������
        if (!isLanded) //���� ������� �� �����������
        {
            if (Physics.Raycast(ray, out hit, deploymentHeight)) // ���� ��� ����� ��������� �� ���������� deploymentHeight
            {
                if (hit.collider.tag == "Enviroment") // ���� ��� ����������� ��������� = Enviroment
                {
                    OpenParachute(); // ��������� �������
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

    private void OnCollisionEnter(Collision collision) // ���� ������ �������� ������ ����������
    {
        anim.SetTrigger("close"); // ������� �������� �������� ��������
    }
}
