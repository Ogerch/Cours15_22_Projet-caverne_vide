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

    [Header("Déplacement horizontal")]

    public float vitesse = 10f;
    public float inputMarche; 

    [Header("Saut")]
    public float forceSaut = 5f;
    public bool inputSaut;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>(); 
    }

    void OnEnable() {
        // Set a activer les écouteurs ou désactiver
        actionMarche.Enable();
        actionSaut.Enable();
    }

    void OnDisable() {
        actionMarche.Disable();
        actionSaut.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        inputMarche = actionMarche.ReadValue<float>();
        if (actionSaut.WasPressedThisFrame())
        {
            inputSaut = true;
        } else {
            inputSaut = false;
        }
    }

    void FixedUpdate(){
        if (inputMarche != 0) {
            Debug.Log("IM WALKING");
            rb.linearVelocityX = vitesse * inputMarche;
        }

        if (inputSaut == true) {
            // rb.AddForce(new Vector2(0,1) * forceSaut);
            rb.AddForce(Vector2.up * forceSaut, ForceMode2D.Impulse);
        }
    }
}
