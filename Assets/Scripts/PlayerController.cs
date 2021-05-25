using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // floats
    public float moveSpeed;

    //bools
    public bool isMoving;
    

    //vectors
    private Vector2 input;

    //animators
    private Animator animator;

    //layers
    public LayerMask collidabllayer;
    public LayerMask grasslayer;
    public LayerMask npclayer;

    public event Action OnEncounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            if (input.x != 0)
            {
                input.y = 0;
            }

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //if position is not 0
            if (input != Vector2.zero)
            {
                animator.SetFloat("vertical", input.x);
                animator.SetFloat("horizontal", input.y);

                var targetPos = transform.position;
                //move player up or down
                targetPos.x += input.x;
                //move player right or left
                targetPos.y += input.y;
                //if area is free of objects
                if (canwalk(targetPos))
                StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        Encounters();
    }
    void interact()
    {
        var faceing = new Vector3(animator.GetFloat("vertical"), animator.GetFloat("horizontal"));
        var npclook = transform.position + faceing;

        // Debug.DrawLine(transform.position, npclook, Color.green, 0.5f);

        var collide = Physics2D.OverlapCircle(npclook, 0.3f, npclayer);

        if (collide != null)
        {
            collide.GetComponent<Interaction>()?.Interact();
        }
    }

    //checking if area is walkable
    public bool canwalk(Vector3 targetPos)
    {
      //checking the overlap of the layers at player's position and if nothing is there then you can walk
      if (Physics2D.OverlapCircle(targetPos, 0.3f, collidabllayer | npclayer) != null){
            return false;
        }
      //else the player cannot walk
        return true;
    }
    private void Encounters()
    {
        //while player is walking on grass
        if (Physics2D.OverlapCircle(transform.position, .3f, grasslayer) != null)
        {
            //if random number generated is less than 10, they get a pokemon encounter
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                animator.SetBool("isMoving", false);
                OnEncounter();
                Debug.Log("Encountered a wild pokemon!");
            }
        }
    }
}
