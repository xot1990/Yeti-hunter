using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Interfaces;
using UnityEngine;

public class Player_Test : MonoBehaviour
{
    Rigidbody2D body;    
    public float speed;       
    bool ShootAnima;
    public GameObject rangeStrike;
    public Transform strikePoint;
    public int HP = 10;
    public int MaxHp = 10;
    Animator anim;
    AudioSource audioSource;
    LayerMask mask;
    public float MaxSpeed = 2;
    public bool onLend;
    public Transform HealsBar;
    public GameObject Menu;
    public GameObject Victory;
    public bool pause;
    public AudioClip hart;
    public AudioClip shot;
    public AudioClip jump;
    public AudioClip[] steps = new AudioClip[6];
    KeyCode[] keys;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void PlaySoundStep()
    {
        audioSource.clip = steps[Random.Range(0, 6)];
        audioSource.Play();
    }

    void Update()
    {
        if (!pause)
        {
            if (body.velocity.y < -1)
            {
                anim.SetBool("FlyDown", true);
                anim.SetBool("Lending", false);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Menu.activeInHierarchy) Menu.SetActive(false);
                else Menu.SetActive(true);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                if (Mathf.Abs(body.velocity.x) < MaxSpeed)
                {
                    Vector3 f = transform.right * Time.deltaTime * speed;
                    body.AddForce(f, ForceMode2D.Impulse);
                }

                anim.SetBool("Run", true);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("Run", false);
                audioSource.Stop();
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                if (Mathf.Abs(body.velocity.x) < MaxSpeed)
                {
                    Vector3 f = transform.right * Time.deltaTime * speed;
                    body.AddForce(f,ForceMode2D.Impulse);
                }

                anim.SetBool("Run", true);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("Run", false);
                audioSource.Stop();
            }

            if (Input.GetKey(KeyCode.Q))
            {
                strikePoint.transform.rotation = Quaternion.Euler(0, 0, 45);
            }

            if(Input.GetKeyUp(KeyCode.Q))
            {
                strikePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && onLend)
            {
                onLend = false;
                anim.SetBool("Lending", false);
                anim.SetBool("StartingJump", true);
                body.AddForce(Vector2.up * 2 * MaxSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {

            }



            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                audioSource.clip = shot;
                if (onLend) anim.SetBool("Strike", true);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (onLend) anim.SetBool("Strike", false);
            }
        }

        if (pause)
        {
            if(!Victory.activeInHierarchy) Victory.SetActive(true);
        }

        
    }

    public void AnimalDead()
    {
        Destroy(gameObject);
    }

    

    

    

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Lend")
        {
            anim.SetBool("StartingJump", false);
            anim.SetBool("FlyDown", false);
            anim.SetBool("Lending", true);
            onLend = true;
        }

        if (collision.transform.tag == "Lawa")
        {
            anim.SetBool("Hit", true);
            anim.SetBool("Dead", true);
        }

        if (collision.transform.tag == "Boss")
        {
            anim.SetBool("Hit", true);
            anim.SetBool("Dead", true);
        }

    }

    

    public void StopHit()
    {
        anim.SetBool("Hit", false);
        if (HP <= 0)
        {
            anim.SetBool("Dead", true);
        }
    }
}
