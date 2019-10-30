using System.Collections;
using UnityEngine;

public class Dalle : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 targetPosition;
    private float timeOffset;

    public string letter;
    private Animation _animation;
    private AudioSource _audio;

    private Light[] _lights;
    private string _lightName = "AreaLight";
    private float _offIntensity = 0.0f;
    private float _onIntensity = 1000.0f;
    private Coroutine _lightsOff;

    private Material _material;

    private ParticleSystem[] _particles;
    private string _particlestName = "ParticleSystem";
    private Coroutine _particlesOff;

    public Gradient particleColor;

    void Start()
    {
        initPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(initPosition.x, initPosition.y + (Random.value*0.07f), initPosition.z);
        timeOffset = Random.value * 10f;

        _audio = GetComponent<AudioSource>();
        _animation = GetComponentsInChildren<Animation>()[1];

        // LIGHT
        _lights = GetComponentsInChildren<Light> ();
        foreach (Light light in _lights)
        {
            if(light.name == _lightName)
            {
                light.enabled = false;
                light.intensity = _offIntensity;
            }
        }

        // MATERIAL
        Renderer renderer = GetComponentInChildren<Renderer>();
        _material = renderer.material = Instantiate<Material>(renderer.material);

        //PARTICLES
        _particles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in _particles)
        {
            if (particle.name == _particlestName)
            {
                particle.Stop();
            }
        }
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

            // LIGHT
            foreach (Light light in _lights)
            {
                if (light.name == _lightName)
                {
                    Color color = new Color32(193, 171, 206, 255);
                    light.enabled = true;
                    light.color = color;
                    light.intensity = _onIntensity;
                }
            }

            if (_lightsOff != null)
                StopCoroutine(_lightsOff);
            _lightsOff = StartCoroutine(SwitchOffLights());

            // PARTICLES
            foreach (ParticleSystem particle in _particles)
            {
                if (particle.name == _particlestName)
                {
                    ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve(0, _animation.clip.length * 0.5f * Random.value);
                    var main = particle.main;

                    main.startLifetime = curve;//_animation.clip.length * 0.5f * Random.value;
                    main.duration = _animation.clip.length;

                    var burst = particle.emission.GetBurst(0);
                    burst.repeatInterval = _animation.clip.length * 2;
                    particle.emission.SetBurst(0, burst);

                    var colors = particle.colorOverLifetime;
                    colors.enabled = true;
                    colors.color = particleColor;

                    particle.Play();
                }
            }

            if (_particlesOff != null)
                StopCoroutine(_particlesOff);
            _particlesOff = StartCoroutine(SwitchOffParticles());
        }
    }

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
