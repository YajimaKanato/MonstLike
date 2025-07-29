using UnityEngine;

public class Particle : MonoBehaviour,ISimulate
{
    float _defaultSpeed;

    static float _simulateSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _defaultSpeed = gameObject.GetComponent<ParticleSystem>().main.simulationSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        var particle = gameObject.GetComponent<ParticleSystem>().main;
        particle.simulationSpeed = _defaultSpeed * Particle._simulateSpeed;
    }

    public void SpeedDown(float speed)
    {
        Particle._simulateSpeed = speed;
    }

    public void SpeedUp(float speed)
    {
        Particle._simulateSpeed = 1;
    }

    public void SimulateChange(float speed = 1)
    {
        _simulateSpeed = speed;
    }
}
