using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        float rotationSpeed = 100f;
        float movementSpeed = 1f;
        float attractionCooldown = 0;

        public GameObject followerObject;
        public GameObject attractionObject;
        public GameObject lastPerson = null;

        public static int score = 0;
        GameObject scoreText;

        // Start is called before the first frame update
        void Start()
        {
            scoreText = GameObject.Find("Score");
            lastPerson = this.gameObject;
            //Application.targetFrameRate = 30;
        }

        // Update is called once per frame
        void Update()
        {
            //Player rotation
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 angleVector = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            float angleToMouse = Mathf.Acos(angleVector.x / angleVector.magnitude) * (180 / Mathf.PI);
            float currentAngle = transform.eulerAngles.z;

            if (mousePos.y < transform.position.y)
            {
                angleToMouse = 360 - angleToMouse;
            }

            //transform.right = angleVector;
            //Debug.Log(angleToMouse);
            //Debug.Log(currentAngle);

            if (Mathf.Abs(currentAngle - angleToMouse) >= 3)
            {
                if (currentAngle > angleToMouse)
                {
                    if ((currentAngle - angleToMouse) < 180)
                    {
                        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                    }
                    else
                    {
                        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                    }
                }
                else
                {
                    if ((angleToMouse - currentAngle) < 180)
                    {
                        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                    }
                }
            }

            //Player movement
            transform.position += Quaternion.Euler(0, 0, currentAngle) * Vector3.right * Time.deltaTime * movementSpeed;

            //Create follower for testing purposes
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Vector3 spawnLocation = new Vector3(-1.11f, -4.29f, 0);
                Instantiate(followerObject, spawnLocation, transform.rotation);
            }

            //Create attraction zone
            if (Input.GetKeyDown(KeyCode.Space) && (attractionCooldown <= 0))
            {
                Instantiate(attractionObject, transform.position, transform.rotation);
                attractionCooldown += 4;
            }            
        }

        private void FixedUpdate()
        {
            if (attractionCooldown > 0)
            {
                attractionCooldown -= 0.02f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log("Trigger");
            //Debug.Log(collision.gameObject.name);
            if (collision.gameObject.tag == "Follower")
            {
                Follower colFollower = collision.gameObject.GetComponent<Follower>();
                if (colFollower.inConga == true)
                {
                    //Fail
                    Debug.Log(colFollower.isPrimed + ", " + colFollower.isAttracted + ", " + colFollower.inConga);
                    //Destroy(gameObject);
                }
            }

            if (collision.gameObject.tag == "Wall")
            {
                //Fail
                //Debug.Log("You fail");
                //Destroy(gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            //Debug.Log("Trigger");
            //Debug.Log(collision.gameObject.name);
            if (collision.gameObject.tag == "Follower")
            {
                Follower colFollower = collision.gameObject.GetComponent<Follower>();                
                if (colFollower.isPrimed == true)
                {
                    score = score + 1;
                    scoreText.GetComponent<Text>().text = "Score: " + score.ToString();
                    colFollower.isPrimed = false;
                    colFollower.isAttracted = true;
                }
            }
        }
    }
}

