using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BonusPlayerController : MonoBehaviour
{

    private Vector3 move;
    public float speed, jumpForce, gravity, verticalVelocity;
    private AudioSource popSound, yaySound,coinCollection;
    private CharacterController charController;
    private Animator anim;
    public List<GameObject> ballons = new List<GameObject>();
    public GameObject player;
    int counter;
    private bool superJump;
    public GameObject patlama;
    public float pushPower = 2f;

    public float force = 10f; //Force 10000f
    public float stunTime = 0.5f;
    private bool falls = false;

    public static BonusPlayerController instance;
    private SkinnedMeshRenderer playerColor;
    public Material[] colors;

    public GameObject fallControl;
    public int knifeCounter, howmanyknives;

    public GameObject confeti;

    public int money;
    public TextMeshProUGUI moneyText;


    void Awake()
    {
        instance = this;
        charController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        counter = ballons.Count;

        gameObject.name = PlayerPrefs.GetString("PlayerName", "Player");
        Application.targetFrameRate = 60;
        popSound = GameObject.Find("PopSound").GetComponent<AudioSource>();
        yaySound = GameObject.Find("YaySound").GetComponent<AudioSource>();
        coinCollection = GameObject.Find("coinCollection").GetComponent<AudioSource>();
        playerColor = GameObject.Find("PlayerColor").GetComponent<SkinnedMeshRenderer>();
        //playerColor.material = colors[PlayerPrefs.GetInt("PlayerColor", 0)];       
        money = PlayerPrefs.GetInt("Money", 0);
    }


    void Update()
    {
        moneyText.text = money.ToString();
        if (BonusManager.instance.finish)
        {            
            move = Vector3.zero;
            if (!charController.isGrounded)
                verticalVelocity -= gravity * Time.deltaTime;
            else
                verticalVelocity = 0;

            move.y = verticalVelocity;

            //charController.Move(new Vector3(0, move.y*Time.deltaTime, 0));
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
            {
                anim.SetTrigger("Dance");
                transform.eulerAngles = Vector3.up * 180;
            }
            return;

        }

        if (!BonusManager.instance.start)
        {
            return;
        }

        move = Vector3.zero;
        move = transform.forward;

        if (charController.isGrounded)
        {
            verticalVelocity = 0;
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }
        }

        if (superJump)
        {
            superJump = false;
            verticalVelocity = jumpForce * 3f;
            yaySound.Play();
            //anim.SetTrigger("Jump");

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
                    player.transform.position = new Vector3(-4.55f, player.transform.position.y, ballons[counter - 1].transform.position.z - 20f);
                }

            }

        }


    }

    void Jump()
    {
        verticalVelocity = jumpForce;
        anim.SetTrigger("Jump");
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
        {
            superJump = true;
        }
        else if (hit.collider.tag == "engel")
        {
            anim.SetBool("obstacleFall", true);
            StartCoroutine(finisAnim());
            StartCoroutine(falling());
        }
        else if (hit.collider.tag == "falling")
        {
            new WaitForSeconds(2);
            transform.position = new Vector3(-6.24382067f, 0.200000763f, -163.199997f);
        }
        else if (hit.gameObject.tag == "knife")
        {
            coinCollection.Play();
            knifeCounter++;//oyunun bitmesi için knife counter farklý deðiþkende tutuldu. money ile olsaydý onu set ettiðimiz için bitirmezdi.
            money += 3;
            PlayerPrefs.SetInt("Money", money);

            Destroy(hit.gameObject);
        }
        else if (hit.gameObject.tag == "KnifeAnimPoint" && howmanyknives == knifeCounter)// her bonus levelde farklý býçak olacaðý için býçak sayýsýný insprectorden veriyoruz
        {
            Instantiate(confeti, transform.position, Quaternion.identity);
            BonusManager.instance.finish = true;
        }



        /*Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;
        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, 0f);
        body.velocity = pushDir * pushPower;   */

    }



    IEnumerator falling()
    {
        yield return new WaitForSeconds(2);
        transform.position = new Vector3(-4.36314726f, -0.298592567f, -120f);

    }

    IEnumerator finisAnim()
    {
        yield return new WaitForSeconds(3);
        anim.SetBool("obstacleFall", false);
    }


}


