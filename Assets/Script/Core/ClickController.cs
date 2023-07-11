using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    PlayInputAction input;
    public Action onStart;
    public static Action onChangeColor;
    public Action onDrop;
    public bool isSwitich = false;
    public bool isDrop = false;

    private void Awake()
    {
        input = new PlayInputAction();
    }

    private void OnEnable()
    {
        input.Play.Enable();
        input.Play.Click.performed += OnClick;
        input.Play.Click.canceled += OnClickOff;
    }


    private void OnDisable()
    {
        input.Play.Disable();
        input.Play.Click.performed -= OnClick;
        input.Play.Click.canceled -= OnClickOff;
    }


    private void OnClick(InputAction.CallbackContext obj)
    {
        if (!isDrop)
        {
            onStart?.Invoke();
        }

        if (isSwitich && isDrop== true)
        {
            onChangeColor?.Invoke();
        }
    }
    private void OnClickOff(InputAction.CallbackContext obj)
    {
        if (!isDrop)
        {
            onDrop?.Invoke();
            isSwitich = true;
            isDrop = true;
        }
    }
}
