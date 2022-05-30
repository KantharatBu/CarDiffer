using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    public float forwardSpeed;

    private int lane = 1;
    // 0 : ซ้าย : 1 กลาง 2 : ขวา
    public float laneDistance = 4; // ระยะระหว่างเลน

    public float jumpForce;
    public float Gravity = -20;

    public Animator anim;
    private bool isSliding = false;

    public float maxSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        if(forwardSpeed < maxSpeed)
        {
            forwardSpeed += 1f * Time.deltaTime;
        }

        

        anim.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        

        direction.y += Gravity * Time.deltaTime;
        if (controller.isGrounded)
        {

            anim.SetBool("isGrounded", true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                {
                    Jump();
                }
            }
        }
        else
        {
           
        }

        if (Input.GetKeyDown(KeyCode.S) && !isSliding) 
        {
            StartCoroutine(Slide());
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lane++;
            if (lane == 3)
            {
                lane = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lane--;
            if (lane == -1)
            {
                lane = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (lane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

            //transform.position = Vector3.Lerp(transform.position, targetPosition, 50 * Time.fixedDeltaTime);
        if(transform.position == targetPosition)
        
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
        

    }


    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;

        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
        anim.SetBool("isGrounded", false);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        
        controller.center = new Vector3(0, 0, 0);
        controller.height = 1.4f;
        anim.SetBool("isSliding", false);
        isSliding = false;
    }
}
