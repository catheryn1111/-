using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float speed = 5;
    Rigidbody rb;
    Vector3 direction;
    [SerializeField] Animator anim;
    [SerializeField] float jumpForce;

    [Header("����������")]
    [SerializeField] GameObject mainControls;
    [SerializeField] GameObject carControls;

    [Header("��������� ������")]
    [SerializeField] GameObject car;//���� ������
    [SerializeField] Transform point;//Waypoint
    [SerializeField] Camera carCamera;//������ ������
    [SerializeField] float radius;//������ � ������� ����� �������� �������
    CarController carController;//������ ������
    bool isDriver;//���������� ���� �� � ������ ��� ���
    NavMeshAgent agent; // ����� ��������� 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carController = car.GetComponent<CarController>();
        radius = 50;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        GameObject onoffjoystick = joystick.gameObject;

#if UNITY_STANDALONE //���������� - ������������ ��� ��
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                onoffjoystick.SetActive(false);
#endif
#if UNITY_WEBGL //���������� - ������������ ��� Webgl
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                onoffjoystick.SetActive(false);
#endif

#if UNITY_ANDROID // ���������� - ������������ ��� �������
        if (!isDriver)
        {
            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;
            direction = transform.TransformDirection(horizontal, 0, vertical);
            anim.SetFloat("move", Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical)));

        }
        onoffjoystick.SetActive(true);
#endif

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed + 5;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed - 5;
        }

        if (isDriver && agent.remainingDistance < .25f)
        {
            Invoke("SwitchCamera", 1f);
            isDriver = false;
            agent.enabled = false;
            transform.LookAt(car.transform);
            carController.enabled = true;
        }
    }

    void FixedUpdate()
    {
        if (!isDriver)
        {
            rb.MovePosition(transform.position + speed * direction * Time.deltaTime);
        }

    }

    private void SwitchCamera()
    {
        carCamera.enabled = true;
        gameObject.SetActive(false);
        gameObject.transform.SetParent(car.transform);
    }
    public void InCar()
    {
        StartCoroutine("IInCar");
    }
    IEnumerator IInCar()
    {
        if (Vector3.Distance(transform.position, car.transform.position) <= radius && !isDriver)
        {
            agent.enabled = true;
            agent.SetDestination(point.position);
            yield return new WaitForSeconds(1);
            isDriver = true;
            anim.SetFloat("move", 1f);
        }
    }
}
