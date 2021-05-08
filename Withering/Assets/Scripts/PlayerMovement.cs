using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 targetPos;
    public float dashRange;
    public float speed;
    private Vector3 direction;
    private Animator animator;
    private enum Facing { UP, DOWN, LEFT, RIGHT};
    private Facing FacingDir = Facing.DOWN;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() 
    {
        TakeInput();
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        

        if(direction.x != 0 || direction.y != 0) {
            SetAnimatorMovement(direction);
        }
        else {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void TakeInput() 
    {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
           
            FacingDir = Facing.UP;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
            FacingDir = Facing.LEFT;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
            FacingDir = Facing.DOWN;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
            FacingDir = Facing.RIGHT;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 currentPos = transform.position;
            targetPos = Vector2.zero;
            if(FacingDir == Facing.UP)
            {
                targetPos.y = 1;
            }
            else if (FacingDir == Facing.DOWN)
            {
                targetPos.y = -1;
            }
            else if (FacingDir == Facing.LEFT)
            {
                targetPos.x = -1;
            }
            else if (FacingDir == Facing.RIGHT)
            {
                targetPos.x = 1;
            }
            //transform.Translate(targetPos * dashRange);
            GetComponent<Rigidbody>().velocity = targetPos;
        }
        
    }

    private void SetAnimatorMovement(Vector3 direction) {
        animator.SetLayerWeight(1,1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }

}
