using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    float damage = 1;
    public float GetDamage() {
        return damage;
    }
}
