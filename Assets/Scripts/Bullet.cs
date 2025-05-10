using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
        speed += 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy_ragdoll"))
        {
           
            other.transform.root.GetComponent<Enemy>().Death(true);
            other.transform.root.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward), ForceMode.Impulse);
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().ChangeHealth(Random.Range(-20, -30));
        }
        if (other.tag == "RedBarrel")
        {
            other.GetComponent<RedBarrel>().Boom();
        }
        Destroy(gameObject);
        other.transform.root.GetComponent<Enemy>().Death(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        
    }

    

}
