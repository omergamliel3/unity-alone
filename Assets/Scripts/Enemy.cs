using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform[] patrolPoints;
    public string KindOfEnemy;
    private int Return;
    private int correctPoint;
    public float moveSpeed;
    public int health;
    private string name1;

	// Use this for initialization
	void Start () {

        health = 500;
        transform.position = patrolPoints[0].position;
        correctPoint = 0;
        Return = 0;
       name1 = transform.name;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (health <= 0)
        {
            Die();
        }
       
        if (KindOfEnemy == "Simple")
        {
            if (transform.position == patrolPoints[correctPoint].position)
            {
                correctPoint++;
            }


            if (correctPoint > patrolPoints.Length - 1)
            {
                correctPoint = 0;
            }

        }

        else if (KindOfEnemy == "Complect")
        {

            if (Return == 0)
            {
                    if (transform.position == patrolPoints[correctPoint].position)
                    {
                        correctPoint++;
                    }


                    if (correctPoint > patrolPoints.Length - 1)
                    {
                        Return = 1;
                        correctPoint = 3;
                    }               
            }

             if (Return == 1)
            {      
                    if (transform.position == patrolPoints[correctPoint].position)
                    {
                        correctPoint--;
                    }
                    if (correctPoint < 0)
                    {
                        Return = 0;
                        correctPoint = 0;
                    }                
            }
                            
        }

          else if (KindOfEnemy == "OneWay")
        {
            if (Return == 0)
            {
                if (transform.position == patrolPoints[correctPoint].position)
                {
                    correctPoint++;
                }

                if (correctPoint > patrolPoints.Length - 1)
                {
                    Return = 1;
                }

            }

             if (Return == 1)
            {

                transform.position = patrolPoints[0].position;
                correctPoint = 0;
                Return = 0;
                
            }

           
        }

            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[correctPoint].position, moveSpeed * Time.deltaTime);
	}


    public void TakeDamage(int damage)
    {
        health -= damage;
        print(name1 + " now has " + health + " health");
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

}

