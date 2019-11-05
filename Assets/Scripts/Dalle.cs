using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Dalle : MonoBehaviour
{

    public string[] letter;
    public Gradient particleColor;

    private Vector3 initPosition;
    private Vector3 targetPosition;
    private float timeOffset;

    private Animation _animation;
    private AudioSource _audio;

    private Light[] _lights;
    private string _lightName = "AreaLight";
    private float _offIntensity = 0.0f;
    private float _onIntensity = 100.0f;
    private Coroutine _lightsOff;

    private ParticleSystem[] _particles;
    private string _particlestName = "ParticleSystem";
    private Coroutine _particlesOff;


    void Start()
    {
        initPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(initPosition.x, initPosition.y + (Random.value*0.07f), initPosition.z);
        timeOffset = Random.value * 10f;

        _audio = GetComponent<AudioSource>();
        _animation = GetComponentsInChildren<Animation>()[1];

        // Light
        _lights = GetComponentsInChildren<Light> ();
        foreach (Light light in _lights)
        {
            if(light.name == _lightName)
            {
                light.enabled = false;
                light.intensity = _offIntensity;
            }
        }

        // Particles
        _particles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in _particles)
        {
            if (particle.name == _particlestName)
            {
                particle.Stop();
            }
        }
    }

    void Update()
    {
        // Automatic tranform
        transform.position = MathUtils.Lerp(initPosition, targetPosition, Mathf.Sin(Time.time+timeOffset));

        for (int i = 0; i < letter.Length; i++)
        {
            if (Input.GetKeyDown(letter[i]))
            {
                _audio.Play();
                _animation.Play();

                // Light
                foreach (Light light in _lights)
                {
                    if (light.name == _lightName)
                    {
                        Color color = new Color32(163, 245, 245, 255);
                        light.enabled = true;
                        light.color = color;
                        light.intensity = _onIntensity;
                    }
                }

                if (_lightsOff != null)
                    StopCoroutine(_lightsOff);
                _lightsOff = StartCoroutine(SwitchOffLights());

                // Particles
                foreach (ParticleSystem particle in _particles)
                {
                    if (particle.name == _particlestName)
                    {
                        ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve(0, _animation.clip.length * 0.5f * Random.value);
                        var main = particle.main;

                        main.startLifetime = curve;
                        main.duration = _animation.clip.length;

                        var burst = particle.emission.GetBurst(0);
                        burst.repeatInterval = _animation.clip.length * 2;
                        particle.emission.SetBurst(0, burst);

                        var colors = particle.colorOverLifetime;
                        colors.enabled = true;
                        MinMaxGradient gradient = particleColor;
                        Color c = gradient.colorMin;
                        c.a = 1.0f;
                        gradient.colorMin = c;
                        c = gradient.colorMax;
                        c.a = 1.0f;
                        gradient.colorMax = c;
                        colors.color = gradient;

                        particle.Play();
                    }
                }

                if (_particlesOff != null)
                    StopCoroutine(_particlesOff);
                _particlesOff = StartCoroutine(SwitchOffParticles());
            }
        }        
    }

    // Coroutine light
    private IEnumerator SwitchOffLights()
    {
        yield return new WaitForSeconds(_animation.clip.length);
        foreach (Light light in _lights)
        {
            if (light.name == _lightName)
            {
                light.enabled = false;
                light.color = Color.black;
                light.intensity = _offIntensity;
            }
        }
    }

    // Coroutine particles
    private IEnumerator SwitchOffParticles()
    {
        yield return new WaitForSeconds(_animation.clip.length);
        foreach (ParticleSystem particle in _particles)
        {
            if (particle.name == _particlestName)
            {
                particle.Stop();
            }
        }
    }
}
