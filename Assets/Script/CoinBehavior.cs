using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] private MeshRenderer coin1;
    [SerializeField] private MeshRenderer coin2;
    [SerializeField] private MeshRenderer coin3;

    public void Show(int RoundWins)
    {
        if (RoundWins == 0) return;
        switch (RoundWins)
        {
            case 1:
                coin1.enabled = true;
                break;
            case 2:
                coin2.enabled = true;
                break;
            case 3:
                coin3.enabled = true;
                break;
            default:
                break;
        }
    }

    public void Reset()
    {
        coin1.enabled = false;
        coin2.enabled = false;
        coin3.enabled = false;
    }





}
