using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControlller : MonoBehaviour
{
    [SerializeField] GameObject cam;
    private CameraAnimator animator;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject optionUI;
    [SerializeField] GameObject startPos;
    [SerializeField] GameObject endPos;
    [SerializeField] Button start;
    [SerializeField] Button exit;
    [SerializeField] Button options;
    [SerializeField] bool isOptions;

    void Start()
    {
      animator = cam.GetComponent<CameraAnimator>();
    }


    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
        //Команда, которая меняет позицию камеры
        cam.transform.position = Vector3.Lerp(cam.transform.position, Input.mousePosition / 450, 0.02f);
#endif

        if (isOptions)
        {
            //Перемещение MainUI при включённом isOptions
            mainUI.transform.position = Vector2.Lerp(mainUI.transform.position, endPos.transform.position, 0.05f);
            animator.Animation();
        }
        else
        {
            //Перемещение на стартовую позицию
            mainUI.transform.position = Vector2.Lerp(mainUI.transform.position, startPos.transform.position, 0.05f);
            animator.OffAnimation();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {

    }

    public void Options()
    {
        isOptions = true;
        optionUI.SetActive(true);
        backButton.SetActive(true);
        start.enabled = false;
        options.enabled = false;
        exit.enabled = false;
        
    }

    public void Back()
    {
        isOptions = false;
        optionUI.SetActive(false);
        backButton.SetActive(false);
        start.enabled = true;
        options.enabled = true;
        exit.enabled = true;
    }
}
