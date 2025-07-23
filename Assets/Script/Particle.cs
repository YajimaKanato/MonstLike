using UnityEngine;

public class Particle : MonoBehaviour
{
    float _defaultSpeed;

    static float _simulateSpeed = 1;
    public static float SimulateSpeed { set { _simulateSpeed = value; } }

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
        var a = gameObject.GetComponent<ParticleSystem>().main;
        a.simulationSpeed = _defaultSpeed * Particle._simulateSpeed;
    }

    public void SpeedDown(float speed)
    {
        Particle._simulateSpeed = speed;
    }

    public void SpeedUp(float speed)
    {
        Particle._simulateSpeed = 1;
    }
}
