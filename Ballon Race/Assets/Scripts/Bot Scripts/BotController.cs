using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    private Vector3 move;
    public float speed, jumpForce, gravity, verticalVelocity;

    private CharacterController charController;
    private Animator anim;
   
    public List<GameObject> ballons = new List<GameObject>();
    public GameObject player;
    private AudioSource popSound;
    int counter;
    public GameObject patlama;

    private bool superJump;

    public Vector3 throwBackPosition;
    void Awake()
    {
        charController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        counter = ballons.Count;
        gameObject.name = Names.BotNames[Random.Range(0, Names.BotNames.Length)];
        popSound = GameObject.Find("PopSound").GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameManager.instance.finish)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
            {
                anim.SetTrigger("Dance");
                transform.eulerAngles = Vector3.up * 180;
            }
            return;
        }

        if (!GameManager.instance.start)
            return;
        
        move = Vector3.zero;
        move = transform.forward;

        if(charController.isGrounded)
        {
            verticalVelocity = 0;
            Raycasting();           
        }
        if (superJump)
        {
            superJump = false;
            verticalVelocity = jumpForce * 1.50f;
           
            anim.SetTrigger("Jump");
        }
        else
        {
            gravity = 30;

            verticalVelocity -= gravity * Time.deltaTime;
        }
        anim.SetBool("Grounded", charController.isGrounded);
        move.Normalize();//harketimizin hep ayný hýzda olmasýný saðlýyoruz
        move *= speed;
        move.y = verticalVelocity;
        charController.Move(move * Time.deltaTime);
        for (int i = 0; i < counter; i++)
        {
            if (ballons[i] != null)
            {
                if (ballons[counter - 1].transform.position.z < player.transform.position.z)
                {
                    player.transform.position = new Vector3(3.58f, player.transform.position.y, ballons[counter - 1].transform.position.z - 20f);
                }
            }

        }
    }

    void Raycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position , transform.forward, out hit, 4f))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if(hit.collider.tag=="BotCollider")
            {
                verticalVelocity = jumpForce;
                anim.SetTrigger("Jump");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            GameObject go = Instantiate(patlama, ballons[counter - 1].transform.position, ballons[counter - 1].transform.rotation) as GameObject;
            Destroy(go, 0.333f);
            Destroy(collision.transform.parent.gameObject);
            popSound.Play();
        }
        
        counter--;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Trampoline" && charController.isGrounded)
            superJump = true;
        else if (hit.collider.tag == "engel")
        {
            anim.SetBool("obstacleFall", true);
            StartCoroutine(finisAnim());
            StartCoroutine(falling());
        }
        else if (hit.collider.tag == "falling")
        {
            new WaitForSeconds(2);
            transform.position = new Vector3(3.70000005f, 0.600000024f, -163.699997f);
        }
    }

    

    IEnumerator falling()
    {
        yield return new WaitForSeconds(2);
        transform.position = throwBackPosition;

    }

    IEnumerator finisAnim()
    {
        yield return new WaitForSeconds(3);
        anim.SetBool("obstacleFall", false);
    }
}
