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

    private string[] _letters = { "a", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    private GameObject[] _plateform = new GameObject[25];
    private float timer = 0.0f;

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

                AudioSource source = dalle.AddComponent<AudioSource>();
                source.clip = sounds[line];

                _plateform[index] = dalle;
                index++;
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        for (int i = 0; i < _plateform.Length; i++)
        {
            if (Input.GetKeyDown(_letters[i]))
            {
                _plateform[i].transform.Translate(0, 0.3f, 0);
                _plateform[i].GetComponent<AudioSource>().Play();
            }

            if (Input.GetKeyUp(_letters[i]))
            {
                _plateform[i].transform.Translate(0, -0.3f, 0);
            }
        }
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

        // Créer un timer pour appeler cette fonction tous les X temps (avec X la durée de l'animation d'une dalle
        GameObject dalle = _plateform[(int)RandomValue(25f)];

        if ( timer > 2f)
        {
            for (int i = 0; i < RandomValue(5); i++)
            {
                dalle.transform.Translate(0, Mathf.Sin(Time.time)*0.1f, 0);
            }

            timer = 0f;
            //dalle.transform.Translate(0, 0, 0);
        }
    }

    public float RandomValue(float max)
    {
        return Mathf.Round(Random.Range(0, max));
    }

    public GameObject[] GetTiles()
    {
        return _plateform;
    }
}
