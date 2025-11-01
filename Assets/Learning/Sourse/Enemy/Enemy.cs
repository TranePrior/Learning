using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    private EnemyMovement _movement;
    private EnemyAttack _attack;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
        _attack = GetComponent<EnemyAttack>();
    }

    private void FixedUpdate()
    {
        if (_target == null) return;
        
        _movement.MoveTowards(_target.position);
        _attack.TryAttack(_target);
    }
    
   
}
