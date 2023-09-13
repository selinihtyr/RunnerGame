using System.Collections;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Animator anim;
    public GameObject girl;
    private Rigidbody rb;
    private ScoreController scoreController;
    public ParticleSystem bloodSplash;
    public float currentSpeed = 2f;
    public float moveSpeed = 4f;
    private bool Running = false;
    private bool TurnLeft = false;
    private bool TurnRight = false;
    private bool TakingDamage = false;
    private bool Idle = false;
    private bool Die = false;
    private bool Jump = false;
    private bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        scoreController = FindObjectOfType<ScoreController>();
    }

    void Update()
    {
        if(!Running && canMove && Input.GetKeyDown(KeyCode.Space))
        {
            Running = true;
            AnimatorParameters();
        }

        if(Running && canMove)
        {
            Movement();
        }    
    }

    public void Movement()
    {
        rb.MovePosition(rb.position + (Vector3.forward * Time.deltaTime * currentSpeed));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.MovePosition(rb.position + Vector3.left * Time.deltaTime * moveSpeed);
            TurnLeft = true;
            AnimatorParameters();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.MovePosition(rb.position + Vector3.right * Time.deltaTime * moveSpeed);
            TurnRight = true;
            AnimatorParameters();
        }
        else
        {
            TurnRight = false;
            TurnLeft = false;
            AnimatorParameters();
        }

        /*
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.World);
         
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            TurnLeft = true;
            AnimatorParameters();
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed * -1);
            TurnRight = true;
            AnimatorParameters();
        }
        else
        {
            TurnRight = false;
            TurnLeft = false;
            AnimatorParameters();
        }      
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "girl")
        {
            Idle = true;
            Running = false;
            Jump = true;
            AnimatorParameters();
            if(Jump == true)
            {
                scoreController.WinGame();        
                canMove = false;    
            }
        }

        if(other.tag == "star")
        {
            scoreController.AddScore();
            Destroy(other.gameObject);
        }

        if (other.tag == ("obstacles"))
        {
            scoreController.SubtractScore();

            TakingDamage = true;
            AnimatorParameters();
            bloodSplash.Play();
        }

        if(other.tag == "door")
        {
            if(scoreController.score >= 15)
            {
                scoreController.DestroyDoor();
            }
            else if(scoreController.score < 15)
            {
                Dead();
            }
        }

        if(other.tag == "door1")
        {
            if(scoreController.score >= 25)
            {
                scoreController.DestroyDoor1();
            }
            else if(scoreController.score < 25)
            {
                Dead();
            }
        }
        
        if(other.tag == "door2")
        {
            if(scoreController.score >= 40)
            {
                scoreController.DestroyDoor2();
            }
            else if(scoreController.score < 40)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        Idle = true;
        Running = false;
        Die = true;
        AnimatorParameters();
        canMove = false;
        Debug.Log("die çalıştı");
        scoreController.HeartScore();
        scoreController.DefeatGame();
        StartCoroutine(WaitAndStartGame(3));
    }

    private IEnumerator WaitAndStartGame(int waitTime)
    {
        yield return new WaitForSeconds(3);
        scoreController.StartGame();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "obstacles")
        {
            TakingDamage = false;
            AnimatorParameters();
        }
    }

        private void AnimatorParameters()
    {
        anim.SetBool("isRunning", Running);
        anim.SetBool("TakeDamage", TakingDamage);
        anim.SetBool("Stop", Idle);
        anim.SetBool("RunLeft", TurnLeft);
        anim.SetBool("RunRight", TurnRight);
        anim.SetBool("Die", Die);
        anim.SetBool("Jump", Jump);
    }
}
    
//sonda koşmaya devam ediyor
//koşarak çarbınca anim oynamıyor