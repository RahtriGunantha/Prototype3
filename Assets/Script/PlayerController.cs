using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explodeParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip gameOverSound;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround =  true;
    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        // explodeParticle = GetComponentInChildren<ParticleSystem>(); // tidak bisa dipakai karena pariclenya lebih dari 1
        // Physics.gravity = Physics.gravity * gravityModifier; (sama seperti dibawah cuma lebih panjang)
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()                                   // sama arti cuma beda penulisan
    {                                              // (gameOver == false), (gameOver != true)
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            // untuk membalikan posisi bool ke true lagi
            isOnGround = true; 
            dirtParticle.Play();   

        } else if(other.gameObject.CompareTag("Obstacle"))
            {
                gameOver = true;
                Debug.Log("Game Over");
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explodeParticle.Play();
                dirtParticle.Stop();
                playerAudio.PlayOneShot(gameOverSound, 1.0f);
                
            }
    }
    
}
