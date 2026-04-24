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

    public InputAction actionTir;

    [Header("Déplacement horizontal")]

    public float vitesse = 10f;
    public float inputMarche;

    [Header("Saut")]
    public float forceSaut = 5f;

    public float forceDash = 25f;
    public bool inputSaut;

    public bool inputDash;

    public bool estAuSol;

    public LayerMask masqueSol; 

    [Header ("Tir")]

    public GameObject projectilePrefab;

    public GameObject pointDeCreation;

    public float directionProjectile = 1f;

    public float delaiTir = 0.25f;
    public float tempsEntreTirs = 0;

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
        actionTir.Enable();
    }

    void OnDisable()
    {
        actionMarche.Disable();
        actionSaut.Disable();
        actionDash.Disable();
        actionTir.Disable();
    }




    // Update is called once per frame
    void Update()
    {
        inputMarche = actionMarche.ReadValue<float>();
        // new vector2(0,-1)
        estAuSol = Physics2D.Raycast(transform.position,Vector2.down, 0.1f,masqueSol); //returnes true or false

        Debug.DrawRay(transform.position, Vector2.down * 1, Color.orange);

        if (actionSaut.WasPressedThisFrame() && estAuSol)
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


        // Minuterie pour declancher le tir
        if(tempsEntreTirs > 0){

            tempsEntreTirs -= Time.deltaTime;
        }
        // On peut tirer si on appuie sur le boutton
        if(actionTir.WasPressedThisFrame() == true && tempsEntreTirs <= 0)
        {
            tempsEntreTirs = delaiTir;
           GameObject clone = Instantiate(projectilePrefab, pointDeCreation.transform.position, pointDeCreation.transform.rotation);
            clone.GetComponent<Projectile>().direction = directionProjectile;
        }



        if (inputMarche < 0f)
        {
            sr.flipX = true;
            Vector2 nouvellePosition = pointDeCreation.transform.localPosition;
            nouvellePosition.x = -1.5f;
            pointDeCreation.transform.localPosition = nouvellePosition;

            directionProjectile = -1;
        }
        else if (inputMarche > 0f)
        {
            sr.flipX = false;
              Vector2 nouvellePosition = pointDeCreation.transform.localPosition;
            nouvellePosition.x = 1.5f;
            pointDeCreation.transform.localPosition = nouvellePosition;

            directionProjectile = 1;
        }


        float vitesseAbsolue = Mathf.Abs(rb.linearVelocityX);
        anim.SetFloat("vitesse", vitesseAbsolue);
         anim.SetBool("estDansLair", estAuSol == false);
        

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
    
