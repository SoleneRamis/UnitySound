using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Plateform : Singleton<Plateform>
{
    public GameObject sampleDalle;
    public AudioClip[] sounds;

    public float margin = 10f;
    public float speed = 0.05f;

    private GameObject[] _plateform = new GameObject[25];
    private string[] _letters = { "a", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    void Start()
    {
        GameObject dalle;
        
     
        int index = 0;
        float dalleWidth = 1.8f;
        int line;
        int column;
        float offsetOriginPosition = 2.5f;

        for (line = 0; line < 5; line++)
        {
            for (column = 0; column < 5; column++)
            {
                float x = ((line - offsetOriginPosition) * dalleWidth + (line - offsetOriginPosition) / margin);
                float z = (column - offsetOriginPosition) * dalleWidth + (column - offsetOriginPosition) / margin;

                dalle = Instantiate<GameObject>(sampleDalle, transform);
                dalle.transform.position = new Vector3(x, 0, z);

                dalle.GetComponent<Dalle>().letter = _letters[index];

                AudioSource source = dalle.AddComponent<AudioSource>();
                source.clip = sounds[line];

                _plateform[index] = dalle;
                index++;
            }
        }
    }

    private void Update()
    {


    }

    private void FixedUpdate()
    {
        // rotation Plateform
        Quaternion rotation = transform.rotation;
        Vector3 angles = rotation.eulerAngles;

        angles.x = Mathf.Sin(Time.time) * 1.2f;
        angles.y = Mathf.Sin(Time.time) * 1.2f;
        angles.z = Mathf.Sin(Time.time) * 1.2f;

        rotation.eulerAngles = angles;
        transform.rotation = rotation;

    }

    public GameObject[] GetTiles()
    {
        return _plateform;
    }
}
