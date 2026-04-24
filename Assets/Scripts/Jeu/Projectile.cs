using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float vitesse = 4;
    public float direction = 1;
    

    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.linearVelocityX = direction * vitesse;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(this.gameObject);
    }
}
