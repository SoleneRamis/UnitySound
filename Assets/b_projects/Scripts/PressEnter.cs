using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEnter : MonoBehaviour
{

    public Canvas _canvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            _canvas.enabled = false;
        }
    }
}
