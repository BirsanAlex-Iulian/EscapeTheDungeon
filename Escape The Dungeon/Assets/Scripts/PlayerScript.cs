using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    private BoxCollider2D myBoxCollider2D;

    private Animator myAnimator;

    [SerializeField]   //Folosind SerializeField putem modifica datele direct in unity in inspector
    private float movementSpeed;

    private bool FacingRight;

    public int currentLevel;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private float jumpForce;

    AudioSource jumpSound;

    public Button LoadButton;

    void Start()
    {
        FacingRight = true;     //La inceput playerul va fi cu fata spre dreapta
        myRigidBody = GetComponent<Rigidbody2D>();  //Preluam datele din unity
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        jumpSound = GetComponent<AudioSource>();
    }

    public void SavePlayer()
    {
        PlayerPrefs.SetInt("GameHasBeenSaved", 1);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        SaveLoadSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        if (PlayerPrefs.GetInt("GameHasBeenSaved") == 1)
        {
            LoadButton.interactable = true;
            SaveData data1 = SaveLoadSystem.LoadPlayer();
            SceneManager.LoadScene(data1.currentLevel);
            Time.timeScale = 1f;
            PauseMenuScript.GameisPaused = false;
        }
    }

    void Update()      
    {
        if (!PauseMenuScript.GameisPaused)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");    //Primim din unity directia pe orizontala pe care am introdus-o
                                                                  //Folosind getaxisraw in loc de getaxis nu vom avea acceleratie(valorile variabilei vor fi -1,0 sau 1)
            Move(horizontal);

            Flip(horizontal);
        }
    }
    private void Move(float horizontal)
    {
        if (IsOnTheGround() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))          //Saltul se face la apasarea pe space sau a sagetii de sus
        {
            myRigidBody.velocity = new Vector2(horizontal * movementSpeed, jumpForce );     //Pe axa y avem valoarea jumpForce care este serialized
            jumpSound.Play();
            
        }
        else myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);            //Pe axa x ne vom misca in functie de variabila horizontal
                                                                                                                //si movementSpeed care este serialized
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));                                          //Parametrul float se modifica in functie de variabila horizontal pentru
                                                                                                      //a detecta daca ne miscam pe orizontala, este folosit pentru animatia Run
        myAnimator.SetBool("OnGround", IsOnTheGround());                    //Parametrul OnGround verifica daca suntem pe pe o platforma
                                                                            //Este folosit pentru tranzitia de la animatia Fall la Run sau Idle
        myAnimator.SetFloat("gravity", myRigidBody.velocity.y);        //Verificam daca playerul se afla in aer
                                                                       //Pentru tranzitiile animatiilor Fall si Jump
    }

    private void Flip(float horizontal)
    {
        if ((horizontal > 0 && !FacingRight) || (horizontal<0 && FacingRight))
        {
            FacingRight = !FacingRight;
            Vector3 PlayerScale = transform.localScale;   
            PlayerScale.x *= -1;                            //Intoarcem spriteul in cealalalta directie
            transform.localScale = PlayerScale;
        }
    }

    private bool IsOnTheGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(myBoxCollider2D.bounds.center, myBoxCollider2D.bounds.size, 0f, Vector2.down, 0.02f, whatIsGround);
        return raycastHit2D.collider != null;               //Functie pentru verificat daca boxcolliderul playerului se afla pe o platforma definita de noi prin whatIsGround
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spikes")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);     //La coliziunea cu un spike reincarcam nivelul de la inceput
        }
    }
}
