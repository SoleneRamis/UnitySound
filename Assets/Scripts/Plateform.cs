﻿using System.Collections;
using UnityEngine;

public class Plateform : MonoBehaviour
{
    public GameObject sampleDalle;
    public AudioClip[] sounds;
    public GameObject[] animations;

    public float margin = 10f;
    public float speed = 0.05f;

    public Gradient[] particleColors;

    private GameObject[] _plateform = new GameObject[16];
    private string[] _letters = { "y", "c", "b", "h", "v", "d", "o", "k", "i", "j", "n", "f", "g", "t", "u", "x"};

    void Start()
    {
        GameObject dalle;
        GameObject animationObject;
        
        int index = 0;
        float dalleWidth = 1.8f;
        int line;
        int column;
        float offsetOriginPosition = 2.5f;

        for (line = 0; line < 4; line++)
        {
            for (column = 0; column < 4; column++)
            {
                float x = ((line - offsetOriginPosition) * dalleWidth + (line - offsetOriginPosition) / margin);
                float z = (column - offsetOriginPosition) * dalleWidth + (column - offsetOriginPosition) / margin;

                // Create tile and set position
                dalle = Instantiate<GameObject>(sampleDalle, transform);
                dalle.transform.position = new Vector3(x, 0, z);

                // Set a letter to a tile
                dalle.GetComponent<Dalle>().letter[0] = _letters[index];

                // Doublons letters
                switch (_letters[index])
                {
                    case "u":
                        dalle.GetComponent<Dalle>().letter[1] = "a";
                        break;
                    case "t":
                        dalle.GetComponent<Dalle>().letter[1] = "q";
                        break;
                    case "g":
                        dalle.GetComponent<Dalle>().letter[1] = "e";
                        break;
                    case "j":
                        dalle.GetComponent<Dalle>().letter[1] = "z";
                        break;
                    case "o":
                        dalle.GetComponent<Dalle>().letter[1] = "l";
                        break;
                    case "h":
                        dalle.GetComponent<Dalle>().letter[1] = "s";
                        break;
                    case "b":
                        dalle.GetComponent<Dalle>().letter[1] = "p";
                        break;
                    case "c":
                        dalle.GetComponent<Dalle>().letter[1] = "r";
                        break;
                }

                // Set color to a tile
                dalle.GetComponent<Dalle>().particleColor = particleColors[index];

                // Add a sound to a tile
                AudioSource source = dalle.AddComponent<AudioSource>();
                source.clip = sounds[index];
                source.playOnAwake = false;

                // Add an animation to a tile
                animationObject = Instantiate<GameObject>(animations[index], transform);
                animationObject.transform.position = dalle.transform.position;
                animationObject.transform.SetParent(dalle.transform);
                animationObject.GetComponent<Animation>().Stop();

                _plateform[index] = dalle;
                index++;
            }
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown("w"))
        {
            StartCoroutine(animTile());
            GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator animTile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        foreach (GameObject p in _plateform)
        {
            p.GetComponentsInChildren<Animation>()[0].Play();
            yield return wait;
        }
    }

    private void FixedUpdate()
    {
        // Rotation Plateform
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
