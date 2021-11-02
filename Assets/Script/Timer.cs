using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float RemainingTime;
    [SerializeField] public float InitialTime;
    public bool isOver { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        RemainingTime = InitialTime;
    }

    public void UpdateTimer()
    {
        if(!isOver) RemainingTime -= Time.deltaTime;
        if (RemainingTime <= 0) isOver = true;
    }

    public void Reset()
    {
        RemainingTime = InitialTime;
        isOver = false;
    }
}
