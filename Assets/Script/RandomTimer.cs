using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimer : MonoBehaviour
{
    private float RemainingTime;
    [SerializeField] public float minTime;
    [SerializeField] public float maxTime;
    public bool isOver { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        RemainingTime = Random.Range(minTime,maxTime);
    }

    public void UpdateTimer()
    {
        if (!isOver) RemainingTime -= Time.deltaTime;
        if (RemainingTime <= 0) isOver = true;
    }

    public void Reset()
    {
        RemainingTime = Random.Range(minTime, maxTime);
        isOver = false;
    }
}
