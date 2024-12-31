using System;
using UnityEngine;

public class DesktopInput : IInput
{
    public event Action<Vector3> ClickDown;
    public event Action<Vector3> ClickUp;
    public event Action<Vector3> Drag;
    public void Update()
    {
        Debug.Log(Input.mousePosition);
        // Обрабатываем нажатие кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            ClickDown?.Invoke(Input.mousePosition);
            Debug.Log("Mouse Down");
        }

        // Обрабатываем отпускание кнопки мыши
        if (Input.GetMouseButtonUp(0))
        {
            ClickUp?.Invoke(Input.mousePosition);
        }

        // Обрабатываем перетаскивание
        if (Input.GetMouseButton(0))
        {
            Drag?.Invoke(Input.mousePosition);
        }
    }
}
