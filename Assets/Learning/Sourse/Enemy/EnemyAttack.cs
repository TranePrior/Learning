using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 10.0f;
    [SerializeField] private float _attackRange = 2.0f;
    [SerializeField] private float _attackCooldown = 1.5f;

    private float _nextAttackTime;

    public void TryAttack(Transform target)
    {
        if (target == null) return;
        
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > _attackRange) return;

        if (Time.time >= _nextAttackTime) ;
        {
            if (target.TryGetComponent<IHealth>(out var health))
            {
                health.TakeDamage(_damage);
            }

            _nextAttackTime = Time.time + _attackCooldown;
        }
    }
}
