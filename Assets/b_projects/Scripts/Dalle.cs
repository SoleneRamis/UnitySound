using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dalle : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 targetPosition;
    private float timeOffset;

    public string letter;

    void Start()
    {
        initPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(initPosition.x, initPosition.y + (Random.value*0.07f), initPosition.z);
        timeOffset = Random.value * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        //automatic tranform
        transform.position = MathUtils.Lerp(initPosition, targetPosition, Mathf.Sin(Time.time+timeOffset));

        //dalle animation + sound played handled here when keypress

        if (Input.GetKeyDown(letter))
        {
            transform.Translate(0, 0.5f, 0);
            GetComponent<AudioSource>().Play();
        }
    }
}
