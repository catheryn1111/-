using UnityEngine;

public class RedBarrel : MonoBehaviour
{
    [SerializeField] float radius = 5;
    [SerializeField] Rigidbody[] enemyRb;
    public void Boom()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            player.ChangeHealth(-80);
        }

        Destroy(gameObject);
    }

    public void Boom2()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rb = colliders[i].attachedRigidbody;
            if (rb)
            {
                
                rb.AddExplosionForce(1000, transform.position, radius);
                Destroy(gameObject);
            }
        }
    }
}

