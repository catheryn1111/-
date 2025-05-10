using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{

    [SerializeField] Animator anim;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Animation()
    {
        anim.SetBool("Settings", true);
    }

    public void OffAnimation()
    {
        anim.SetBool("Settings", false);

    }
}
