using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : Singleton<Plateform>
{
    public GameObject sampleDalle;
    public float margin = 10f;

    private GameObject[] _plateform;

    // Start is called before the first frame update
    void Start()
    {
        GameObject dalle;

        int index = 0;
        float dalleWidth = 1.8f;

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

    public GameObject[] GetTiles()
    {
        return _plateform;
    }
}
