using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dalle : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 targetPosition;
    private float timeOffset;

    public string letter;
    private Animation _animation;
    private AudioSource _audio;

    Light[] lights;
    string lightName = "AreaLight";
    string emissionColor = "_EmissionColor";
    float offIntensity = 0.0f;
    float onIntensity = 300.0f;

    private Material material;
    private Coroutine lightsOff;

    void Start()
    {
        initPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(initPosition.x, initPosition.y + (UnityEngine.Random.value*0.07f), initPosition.z);
        timeOffset = UnityEngine.Random.value * 10f;

        _audio = GetComponent<AudioSource>();
        _animation = this.gameObject.GetComponentInChildren<Animation>();

        lights = GetComponentsInChildren<Light> ();
        Debug.Log(lights.Length);
        foreach (Light light in lights)
        {
            if(light.name == lightName)
            {
                light.enabled = false;
                light.intensity = offIntensity;
            }
        }

        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material = Instantiate<Material>(renderer.material);
    }

    // Update is called once per frame
    void Update()
    {
        //automatic tranform
        transform.position = MathUtils.Lerp(initPosition, targetPosition, Mathf.Sin(Time.time+timeOffset));
        
        //dalle animation + sound played handled here when keypress
        if (Input.GetKeyDown(letter))
        {
            _audio.Play();
            _animation.Play();
            foreach (Light light in lights)
            {
                if (light.name == lightName)
                {
                    light.enabled = true;
                    light.color = Color.blue;
                    light.intensity = onIntensity;
                    material.SetColor(emissionColor, Color.red);
                }
            }

            if (lightsOff != null)
                StopCoroutine(lightsOff);
            lightsOff = StartCoroutine(SwitchOffLights());
        }
    }

    private IEnumerator SwitchOffLights()
    {
        yield return new WaitForSeconds(_animation.clip.length);
        foreach (Light light in lights)
        {
            if (light.name == lightName)
            {
                light.enabled = false;
                light.color = Color.black;
                light.intensity = offIntensity;
            }
        }
    }
}
