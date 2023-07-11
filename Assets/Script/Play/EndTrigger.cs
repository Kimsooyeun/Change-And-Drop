using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public int Count = 0;
    float elapsedTime;
    bool isReadyToCheck = false;
    bool isGameWin = false;

    public static Action onGameClearEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isReadyToCheck = true;
            elapsedTime = 0f;
        }
    }

    private void Update()
    {
        if (!isReadyToCheck)
        {
            return;
        }

        if (isGameWin)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 2f)
        {
            isGameWin = true;
            onGameClearEvent?.Invoke();
        }
    }
}
