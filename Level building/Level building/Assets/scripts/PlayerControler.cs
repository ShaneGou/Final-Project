using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    // [SerializeField] LayerMask groundLayer;
    public float moveSpeed;
    // public Rigidbody rb;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator Anim;

    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    // private float checkForGroundDistance = 0.2f;
    // private bool onGround = false;
    
    GameObject currFloor;
    [SerializeField] float fadeSpeed;
    [SerializeField] float disappearTime;
    Color startColor;
    // use this for initialization
    void Start()
    {
        //  rb= GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        //     rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, Input.GetAxis("Vertical")* moveSpeed);

        //   if (Input.GetButtonDown("Jump"))
        // {
        //    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        //}

        // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        // onGround = CheckForGround();
        // print($"Is On Ground: {onGround}");
        // print($"Position: {transform.position}");
        print($"Player is grounded: {controller.isGrounded}");

        if (knockBackCounter <= 0)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)

        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        // Anim.SetBool("isGround", controller.isGrounded);

        // Anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) * Mathf.Abs(Input.GetAxis("Horizontal"))));
        // Anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
            Anim.SetBool("isRunning", true);
        } else {
            Anim.SetBool("isRunning", false);
        }
    }

    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("dFloor"))
        {
            currFloor = other.gameObject;
            startColor = currFloor.GetComponent<Renderer>().material.color;
            StartCoroutine(FadeOut());

        }
        IEnumerator FadeOut()
        {
            while (currFloor.GetComponent<Renderer>().material.color.a >= .5f)
            {
                Color startingColor = currFloor.GetComponent<Renderer>().material.color;
                float fadeAmount = startingColor.a - (fadeSpeed * Time.deltaTime);

                startingColor = new Color(startingColor.r, startingColor.g, startingColor.b, fadeAmount);
                currFloor.GetComponent<Renderer>().material.color = startingColor;
                if (currFloor.GetComponent<Renderer>().material.color.a <= .5f)
                {
                    StartCoroutine(Disappear());
                }
                yield return null;
            }

        }

        //disappearing platforms
        IEnumerator Disappear()
        {
            print("Trigger Disappear");
            currFloor.transform.position = new Vector3(currFloor.transform.position.x, currFloor.transform.position.y - 1f, currFloor.transform.position.z);
            currFloor.SetActive(false);
            yield return new WaitForSeconds(disappearTime);
            StartCoroutine(Appear());
        }

        IEnumerator Appear()
        {
            yield return new WaitForSeconds(1f);
            print("activate");
            currFloor.SetActive(true);
            currFloor.transform.position = new Vector3(currFloor.transform.position.x, currFloor.transform.position.y + 1f, currFloor.transform.position.z);
            currFloor.GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g, startColor.b, 1);
        }

    }

    // private RaycastHit CheckBelow() {
    //     RaycastHit hit;
    //     Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, out hit, 20, groundLayer, QueryTriggerInteraction.Ignore);

    //     return hit;
    // }

    // private bool CheckForGround() {

    //     RaycastHit rayTarget = CheckBelow();

    //     // Debug.DrawLine(transform.position + Vector3.up * 0.05f, transform.position + Vector3.down * checkForGroundDistance, Color.cyan);

    //     if (rayTarget.distance < checkForGroundDistance) {
    //         return true;
    //     }

    //     return false;
    // }

}

