using UnityEngine;
using UnityEngine.InputSystem;
public class DeplacementPersonnage : MonoBehaviour
{
    // Composant du personnage

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    [Header("Actions du Personnage")]

    public InputAction actionMarche;
    public InputAction actionSaut;

    public InputAction actionDash;

    [Header("Déplacement horizontal")]

    public float vitesse = 10f;
    public float inputMarche;

    [Header("Saut")]
    public float forceSaut = 5f;

    public float forceDash = 10f;
    public bool inputSaut;

    public bool inputDash;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        // Set a activer les écouteurs ou désactiver
        actionMarche.Enable();
        actionSaut.Enable();
        actionDash.Enable();
    }

    void OnDisable()
    {
        actionMarche.Disable();
        actionSaut.Disable();
        actionDash.Disable();
    }




    // Update is called once per frame
    void Update()
    {
        inputMarche = actionMarche.ReadValue<float>();
        if (actionSaut.WasPressedThisFrame())
        {
            inputSaut = true;
        }
        else
        {
            inputSaut = false;
        }

        if (actionDash.WasPressedThisFrame())
        {
            inputDash = true;

             anim.SetTrigger("estDash");
        }
        else
        {
            inputDash = false;
        }

        if (inputMarche < 0f)
        {
            sr.flipX = true;
        }
        else if (inputMarche > 0f)
        {
            sr.flipX = false;
        }

        float vitesseAbsolue = Mathf.Abs(rb.linearVelocityX);
        anim.SetFloat("vitesse", vitesseAbsolue);
        

        // if ()
        // {
        //     anim.SetTrigger("estDash");
        // }

        // if (rb.linearVelocity < 0f)
        // {
        //     sr.flipX;
        // }
    }

    void FixedUpdate()
    {
        if (inputMarche != 0)
        {
            Debug.Log("IM WALKING");
            rb.linearVelocityX = vitesse * inputMarche;
        }

        if (inputSaut == true)
        {
            // rb.AddForce(new Vector2(0,1) * forceSaut);
            rb.AddForce(Vector2.up * forceSaut, ForceMode2D.Impulse);
        }

        if (inputDash == true)
        {

            if (sr.flipX == true)
            {
                rb.AddForce(Vector2.left * forceDash, ForceMode2D.Impulse);
            }
            else if (sr.flipX == false)
            {
                rb.AddForce(Vector2.right * forceDash, ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ennemi")
        {
            anim.SetTrigger("estBlesse");
        }
    }
}
    
