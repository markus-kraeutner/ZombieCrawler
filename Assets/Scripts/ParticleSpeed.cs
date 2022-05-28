using UnityEngine;

public class ParticleSpeed : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0f;
    ParticleSystem system;
    private void Awake() {
        system = GetComponent<ParticleSystem>();
    }    
    void Update()
    {
        var velo = system.velocityOverLifetime;
        velo.speedModifier = speed*GameManager.Instance.GetSpeedup();        
    }
}
