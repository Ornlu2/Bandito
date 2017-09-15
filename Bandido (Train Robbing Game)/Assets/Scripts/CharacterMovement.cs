using UnityEngine;
using System.Collections;


 public class CharacterMovement : MonoBehaviour
{
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject Weapon;
    Animator AttackAnim;


    private void Start()
    {
       
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;


            transform.parent = GetGroundTransform();
        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);


        Attack();
    }


    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            AttackAnim.SetBool("IsAttacking", true);
            Debug.Log("Attack");
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            AttackAnim.SetBool("IsAttacking", false);
        }
    }


    Transform GetGroundTransform()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.down);

        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down, Color.green);

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            
           Debug.Log("On Car "+hit.transform );
        return hit.transform;
    }


}