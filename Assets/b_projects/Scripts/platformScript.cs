using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour
{
    public GameObject sampleDalle;
    public GameObject sphere;
    public float margin = 10f;

    private AudioSource _source;
    private GameObject[] _plateform;

    // Start is called before the first frame update
    void Start()
    {
        GameObject dalle;

        int index = 0;
        float dalleWidth = 1.8f;
 
        _source = GetComponent<AudioSource>();
        _plateform = new GameObject[25];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                float x = i * dalleWidth + i / margin;
                float z = j * dalleWidth + j / margin;

                dalle = Instantiate<GameObject>(sampleDalle, transform);
                dalle.transform.position = new Vector3(x, 0, z);
                _plateform[index] = dalle;

                index++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _plateform.Length; i++)
        {
            if (Mathf.Abs(_plateform[i].transform.eulerAngles.z) >= 180)
            {
                _plateform[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                _plateform[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (sphere.transform.position.y < -5)
        {
            sphere.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            sphere.transform.position = new Vector3(-0.5f, 5, 0);
        }
    }
}
