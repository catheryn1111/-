using UnityEngine;
using UnityEngine.UI;
public class OpticalSight : MonoBehaviour
{
    [SerializeField] Camera cameraMain;
    [SerializeField] Camera opticCamera;
    [SerializeField] GameObject scopeUI;
    [SerializeField] Slider slider;
    PlayerLook playerLook; //
    float maxFOV = 0.5f;
    float mouseMax = 60f;
    [SerializeField]float mouse;

    private bool isScopeOn;
    void Start()
    {
        playerLook = GetComponent<PlayerLook>(); //
        isScopeOn = false;
        scopeUI.SetActive(false);
    }

    public void SwitchScope()
    {
        if (isScopeOn)
        {
            isScopeOn = false;
            cameraMain.enabled = true;
            opticCamera.enabled = false;
            scopeUI.SetActive(false);
            opticCamera.fieldOfView = Mathf.Lerp(opticCamera.fieldOfView, slider.value, 10 * Time.deltaTime);
        }
        else
        {
            isScopeOn = true;
            cameraMain.enabled = false;
            opticCamera.enabled = true;
            scopeUI.SetActive(true);
        }


    }

    public void OnScopeChanged(float value)
    {
        opticCamera.fieldOfView = value;
        mouse = value / maxFOV * mouseMax;
        playerLook.ChangeMouseSensivity(mouse);
    }
}
