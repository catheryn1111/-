using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{
    const float secondsInDay = 86400f;

    [SerializeField] float timeScale = 60f;
    float time;

    [SerializeField] Color nightLightColor;
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] AnimationCurve nightTimeCurve;

    [SerializeField] Text text;

    [SerializeField] Volume globalVolume;
    ColorAdjustments colorAdjustments;

    float Hours
    {
        get { return time / 3600f; }
    }

    private void Start()
    {

    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        if (time >= secondsInDay)
            time = 0f;

        text.text = Hours.ToString("00.00");

        float normalizedTime = time / secondsInDay;
        float curveValue = nightTimeCurve.Evaluate(normalizedTime);

        if (colorAdjustments != null)
        {
            colorAdjustments.colorFilter.value = Color.Lerp(nightLightColor, dayLightColor, curveValue);
        }
    }
}


