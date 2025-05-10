using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;


public class PlayerLook : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject Anchor;
    [SerializeField] Camera opticCamera;
    [SerializeField] GameObject enemy;


    [SerializeField]
    [Range(0.5f, 2f)]
    float mouseSense = 0.5f; // Чувствительность мыши

    [SerializeField]
    [Range(-30, 10)]
    float lookUp = 15;

    [SerializeField]
    [Range(5, 25)]
    float lookDown = 20;


    public void Update()
    {
        Look();

        foreach (var touch in Input.touches)
        {
            if (touch.position.x >= Screen.width / 2)
            {
                float rotateX = touch.deltaPosition.x * mouseSense;
                float rotateY = touch.deltaPosition.y * mouseSense;

                Vector3 rotPlayer = transform.rotation.eulerAngles;

                rotPlayer.x -= rotateY;
                rotPlayer.z = 0;
                rotPlayer.y += rotateX;

                transform.rotation = Quaternion.Euler(rotPlayer);

                if (enemy != null)//если ссылка на врага не пустая
                {
                    enemy.transform.Translate(-rotateX / 3, rotateY / 3, 0);
                }
            }
        }
    }

    public void Look()
    {
#if UNITY_STANDALONE
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;
        Vector3 rotCamera = transform.rotation.eulerAngles;
        Vector3 rotPlayer = player.transform.rotation.eulerAngles;

        rotCamera.x = (rotCamera.x > 180) ? rotCamera.x - 360 : rotCamera.x;
        rotCamera.x = Mathf.Clamp(rotCamera.x, lookUp, lookDown);
        rotCamera.x -= rotateY;

        rotCamera.z = 0;
        rotPlayer.y += rotateX;

        transform.rotation = Quaternion.Euler(rotCamera);
        player.transform.rotation = Quaternion.Euler(rotPlayer);
#endif

#if UNITY_WEBGL
        Cursor.lockState = CursorLockMode.Locked;
        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;
        Vector3 rotCamera = transform.rotation.eulerAngles;
        Vector3 rotPlayer = player.transform.rotation.eulerAngles;

        rotCamera.x = (rotCamera.x > 180) ? rotCamera.x - 360 : rotCamera.x;
        rotCamera.x = Mathf.Clamp(rotCamera.x, lookUp, lookDown);
        rotCamera.x -= rotateY;

        rotCamera.z = 0;
        rotPlayer.y += rotateX;

        transform.rotation = Quaternion.Euler(rotCamera);
        player.transform.rotation = Quaternion.Euler(rotPlayer);
#endif

#if UNITY_ANDROID
        foreach (var touch in Input.touches)
        {
            if (touch.position.x >= Screen.width / 2)
            {
                float rotateX = touch.deltaPosition.x * mouseSense;
                float rotateY = touch.deltaPosition.y * mouseSense;

                Vector3 rotCamera = transform.rotation.eulerAngles;
                Vector3 rotPlayer = player.transform.rotation.eulerAngles;

                rotCamera.x = (rotCamera.x > 180) ? rotCamera.x - 360 : rotCamera.x;
                rotCamera.x = Mathf.Clamp(rotCamera.x, lookUp, lookDown);
                rotCamera.x -= rotateY;

                rotCamera.z = 0;
                rotPlayer.y += rotateX;

                transform.rotation = Quaternion.Euler(rotCamera);
                player.transform.rotation = Quaternion.Euler(rotPlayer);
            }
        }
#endif
    }

    public void ChangeMouseSensivity(float count)
    {
        mouseSense = count;
    }

    public void FindEnemy(GameObject other)
    {
        enemy = other;
    }

}

