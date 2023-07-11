using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCube : CubeBase
{
    [SerializeField]
    private GameObject splinter;
    private bool onSplinter;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        ClickController.onChangeColor += ChangeObject;
    }
    private void OnDisable()
    {
        ClickController.onChangeColor -= ChangeObject;
    }

    private void Start()
    {
        onSplinter = splinter.activeSelf;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            if(!onSplinter)
            {
                return;
            }

            Ball ball = other.GetComponent<Ball>();
            ball.Init();
            BallPool.ReturnObject(ball);
            --GameManager.Instance.CurrentBallCount;
            ++GameManager.Instance.DeletBallCount;
        }
    }

    protected override void ChangeObject()
    {
        onSplinter = !onSplinter;
        splinter.SetActive(onSplinter);
    }
}
