using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 30f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private GameObject hitEffect;

    private Vector3 _target;
    private bool _hasTarget;
    private Vector3 randomRotateAxis;

    private void Start()
    {
        randomRotateAxis = Random.onUnitSphere.normalized;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Rotate(randomRotateAxis * (rotationSpeed * Time.deltaTime), Space.Self);

        if (!_hasTarget) return;

        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target) < 0.5f)
            HitTarget();
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        _hasTarget = true;

        Vector3 dir = (_target - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void HitTarget()
    {
        if (hitEffect)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            var boss = other.GetComponentInParent<BossManager>();
            if (boss != null)
                boss.TakeDamage(1);

            HitTarget();
        }
    }
}