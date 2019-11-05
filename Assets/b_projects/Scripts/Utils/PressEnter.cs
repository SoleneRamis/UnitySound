using UnityEngine;

public class PressEnter : MonoBehaviour
{
    public Canvas _canvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            _canvas.enabled = false;
        }
    }
}
