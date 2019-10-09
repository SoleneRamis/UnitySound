using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.OpenScene("PlateformScene");
        SceneController.Instance.OpenScene("SecondScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
