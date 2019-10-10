using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : Singleton<Plateform>
{
    public GameObject sampleDalle;
    public AudioClip[] sounds;

    public float margin = 10f;

    private string[] _letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "x", "y", "z" };
    private GameObject[] _plateform = new GameObject[25];
    // Start is called before the first frame update
    void Start()
    {
        GameObject dalle;
     
        int index = 0;
        float dalleWidth = 1.8f;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                float x = i * dalleWidth + i / margin;
                float z = j * dalleWidth + j / margin;

                dalle = Instantiate<GameObject>(sampleDalle, transform);
                AudioSource source = dalle.AddComponent<AudioSource>();

                dalle.transform.position = new Vector3(x, 0, z);

                source.clip = sounds[i];
                _plateform[index] = dalle;

                index++;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < _plateform.Length; i++)
        {
            if (Input.GetKeyDown(_letters[i]))
            {
                _plateform[i].transform.Translate(0, 0.3f, 0);

				//_plateform[i].GetComponent<Animator>().Play();
                _plateform[i].GetComponent<AudioSource>().Play();
            }
            if (Input.GetKeyUp(_letters[i]))
            {
                _plateform[i].transform.Translate(0, -0.3f, 0);
            }
        }
        
    }

    public GameObject[] GetTiles()
    {
        return _plateform;
    }
}
