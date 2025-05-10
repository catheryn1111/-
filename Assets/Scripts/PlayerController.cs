using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using System.Security.Cryptography;
using TMPro;

using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Text HpText;
    int health;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject rifleStart;
    [SerializeField] float shootForce = 100f;
    private int ammo;
    private int capacity;
    private int capacityMax = 30;
    [SerializeField] Text ammoText;
    float range = 100f;
    [SerializeField] ParticleSystem flash;
    [SerializeField] GameObject impact;
    [SerializeField] Text timer;
    private float time;
    bool shoot;
    private float shootTimer;
    [SerializeField] Animator anim;
    public int money;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] NavMeshAgent agent;
    public bool isInCar;

    [Header ("Настройки машины")]
    [SerializeField] GameObject car;//наша машина
    [SerializeField] Transform point;//Waypoint
    [SerializeField] Camera carCamera;//Камера машины
    [SerializeField] float radius;//радиус в котором будет работать посадка
    CarController carController;//скрипт машины
    bool isDriver;//определяет идем мы к машине или нет


    public void ChangeHealth(int count)
    {
        health = health + count;
        HpText.text = "Health: " + health.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeHealth(100);
        AddAmmo(150);
        capacity = 30;
        ammoText.text = "Ammo:" + capacity + "/" + ammo;
        PlayerPrefs.SetInt("BringItem", 1);
        carController = car.GetComponent<CarController>();
    }

    public Transform GiveTransform()
    {
        Transform transform = GetComponent<Transform>();
        return transform;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shoot && shootTimer > 0.1f)//условие выстрела
        {
            if (capacity <= 0)
            {
                return;
            }
            shootTimer = 0;
            capacity -= 1;
            ammoText.text = capacity + " / " + ammo;
            //GameObject buf = Instantiate(bullet);
            //buf.GetComponent<Bullet>().SetDirection(rifleStart.transform.forward);
            //buf.transform.position = rifleStart.transform.position;
            //buf.transform.rotation = rifleStart.transform.rotation;
        }

        RaycastHit hit; //объект, в который попал луч
        Ray ray = new Ray(transform.position, transform.forward); // направление луча вперед
        Debug.DrawRay(transform.position, transform.forward, Color.red); // рисуем луч для проверки

        if (Physics.Raycast(ray, out hit, 15f)) // на расстоянии 15 считываем NPC (можно поставить значение)
        {
            if (hit.collider.tag == "AI") // если тэг затронутого коллайдер = AI
            {
                hit.transform.GetComponent<AINavigation>().FearStart(); // запускаем функцию в другом коде 
            }
        }

        time += Time.deltaTime;
        timer.text = "Time:" + time.ToString("f1");

        if (isDriver && agent.remainingDistance < .25f)
        {
            Invoke("SwitchCamera", 1f);
            isDriver = false;
            agent.enabled = false;
            transform.LookAt(car.transform);
            carController.enabled = true;
                        SwitchCamera();
        }
    }
    public void onPointerDownBoost()
    {
        shoot = true;
    }

    public void onPointerUpBoost()
    {
        shoot = false;
    }
    public void AddAmmo(int count)
    {
        ammo += count;
        ammoText.text = "Ammo:" + capacity + "/" + ammo;
    }

    public void Reload()
    {
        int need = capacityMax - capacity;
        if (need <= ammo)
        {
            ammo -= need;
            capacity += need;
        }
        else
        {
            ammo = 0;
        }
        ammoText.text = "Ammo: " + capacity + "/" + ammo;
    }

    public void Shoot()
    {
        flash.Play();
        RaycastHit shootObj;

        if (Physics.Raycast(rifleStart.transform.position, rifleStart.transform.forward, out shootObj, range))
        {
            //Инициируем эффект в точке касании рейкастом объекта
            GameObject inst = Instantiate(impact, shootObj.point, Quaternion.LookRotation(shootObj.normal));
            //Уничтожаем эффект через 0,2сек
            Destroy(inst, 0.2f);
            Debug.Log(shootObj.collider.name);

            if (shootObj.rigidbody != null)
            {
                shootObj.rigidbody.AddForce(-shootObj.normal * shootForce);
            }

            if (shootObj.collider.tag.ToString() == "Enemy")
            {
                EnemyAI enemy = FindObjectOfType<EnemyAI>();
                
            }
        }
    }

    public void SayHello()
    {
        anim.SetTrigger("hi");
    }
    public void HelloGuys(string say)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var people in colliders)
        {
            if (people.tag == "people")
            {
                people.GetComponent<Animator>().SetTrigger("hi");
                print(say);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("QuestItem"))
        {
            if (PlayerPrefs.GetInt("BringItem") == 2)
            {
                Destroy(collision.gameObject);
                PlayerPrefs.SetInt("BringItem", 3);
            }
        }
    }
    public void GetMoney(int count)
    {
        money += count;
        moneyText.text = "Money: " + money.ToString();
    }

    public void GoToTheCar()
    {
        StartCoroutine(IInCar());
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

    private void SwitchCamera()
    {
        carCamera.enabled = true;
        gameObject.SetActive(false);
        gameObject.transform.SetParent(car.transform);
    }
}


