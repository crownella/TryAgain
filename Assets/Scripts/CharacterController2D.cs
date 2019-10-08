using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    public Camera cam;

    public Canvas canvas;

    public TMPro.TextMeshProUGUI qoute;

    public TMPro.TextMeshProUGUI extra;

    public int level = 0;

    public bool phase2 = false;

    //public GameManager gM;

    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration while in the air.")]
    float airDeceleration = 5;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;


    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    /// <summary>
    /// Set to true when the character intersects a collider beneath
    /// them in the previous frame.
    /// </summary>
    private bool grounded;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        //gM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        //camera settings and screen text update
        float playPos = transform.position.x;

        if (playPos > 171) //ending surprise 10
        {
            cam.gameObject.transform.position = new Vector3(playPos, transform.position.y + 10f, -10);
            qoute.text = " ";
            extra.text = " ";
            phase2 = true;
            cam.backgroundColor = new Color(Random.Range(0, 10)/10, Random.Range(0, 10)/10, Random.Range(0,10)/10);
            level = 10;
        }
        else if (playPos > 153.4) //9
        {
            cam.gameObject.transform.position = new Vector3(162.4f, 0, -10);
            qoute.text = "Try, try again                                             - Thomas Palmer";
            extra.text = "Game Design          Kate Howell";
            level = 9;
        }
        else if (playPos > 135.4) //8
        {
            cam.gameObject.transform.position = new Vector3(144.5f, 0, -10); 
            qoute.text = "All that other folks can do,            Why, with patience, should not you?           Only keep this rule in view:                   Try, try again; ";
            extra.text = " ";
            level = 8;
        }
        else if (playPos > 117.4) //7
        {
            cam.gameObject.transform.position = new Vector3(126.4f, 0, -10);
            qoute.text = "Time will bring you your award,            Try, try again; ";
            extra.text = " ";
            level = 7;
        }
        else if (playPos > 99.2) //6
        {
            cam.gameObject.transform.position = new Vector3(108.3f, 0, -10);
            qoute.text = "If you find your task is hard,            Try, try again; ";
            extra.text = " ";
            level = 6;
        }
        else if (playPos > 81.2) //5
        {
            cam.gameObject.transform.position = new Vector3(90.2f, 0, -10);
            qoute.text = "If we strive, 'tis no disgrace              Though we do not win the race;                What should you do in the case?              Try, try again ";
            extra.text = " ";
            level = 5;
        }
        else if (playPos > 63) //4
        {
            cam.gameObject.transform.position = new Vector3(72.1f, 0, -10);
            qoute.text = "If you would at last prevail,                  Try, try again; ";
            extra.text = " ";
            level = 4;
        }
        else if(playPos > 45) //3
        {
            cam.gameObject.transform.position = new Vector3(54.1f, 0, -10);
            qoute.text = "Once or twice, though you should fail,           Try, try again; ";
            extra.text = " ";
            level = 3;
        }
        else if (playPos > 27) //2
        {
            cam.gameObject.transform.position = new Vector3(36f, 0, -10);
            qoute.text = "Then your courage should appear,    For if you will persevere,                     You will conquer, never fear                    Try, try again; ";
            extra.text = " ";
            level = 2;
        }
        else if (playPos > 9) //1
        {
            cam.gameObject.transform.position = new Vector3(18f, 0, -10);
            qoute.text = "If at first you don't succeed,            Try, try again; ";
            extra.text = " ";
            level = 1;
        }
        else//0
        {
            cam.gameObject.transform.position = new Vector3(0, 0, -10);
            qoute.text = "`Tis a lesson you should head,        Try, try again; ";
            extra.text = " ";
            level = 0;
        }




        // Use GetAxisRaw to ensure our input is either 0, 1 or -1.
        float moveInput = Input.GetAxisRaw("Horizontal");

        //handle jump
        if (grounded)
        {
            velocity.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                // Calculate the velocity required to achieve the target jump height.
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                jumpHeight += .1f;
            }
        }


        //calcucate velocity
        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : airDeceleration;

        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        //tanslate player
        transform.Translate(velocity * Time.deltaTime);

        grounded = false;

        // Retrieve all colliders we have intersected after velocity has been applied.
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            // Ignore our own collider.
            if (hit == boxCollider)
                continue;

            if (hit.transform.tag == "Respawn")
                continue;

            if (hit.transform.tag == "Goal") //win state
            {
                SceneManager.LoadScene("WinScreen");
                continue;
            }
            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            // Ensure that we are still overlapping this collider.
            // The overlap may no longer exist due to another intersected collider
            // pushing us out of this one.
            
            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                // If we intersect an object beneath us, set grounded to true. 
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }

        
    }
}
