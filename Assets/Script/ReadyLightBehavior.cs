using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyLightBehavior : MonoBehaviour
{
    private Light light;
    public RandomTimer randomTimer { get; private set; }
    [SerializeField] AudioSource readySFX;
    [SerializeField] AudioSource countdownSFX;
    private void Awake()
    {
        randomTimer = gameObject.GetComponent<RandomTimer>();
        light = gameObject.GetComponent<Light>();
        light.color = Color.yellow;
    }
   
    public void Reset()
    {
        randomTimer.Reset();
        light.color = Color.red;
    }

    public void Count()
    {
        randomTimer.UpdateTimer();
        if (randomTimer.isOver)
        {
            readySFX.Play();
            light.color = Color.green;
        }
    }

    public void Wait()
    {
        countdownSFX.Play();
        light.color = Color.yellow;
    }
}
