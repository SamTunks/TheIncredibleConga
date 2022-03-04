using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AttractionCone : MonoBehaviour
    {

        float coneCooldown = 1;

        GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("CongaMan");
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        // Update is called once per frame
        void Update()
        {
            if (coneCooldown <= 0)
            {
                Destroy(gameObject);
            }

            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
        }

        private void FixedUpdate()
        {
            coneCooldown -= 0.02f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Follower(Clone)" || collision.gameObject.name == "Follower")
            {
                Follower colFollower = collision.gameObject.GetComponent<Follower>();
                if (colFollower.inConga == false && colFollower.isAttracted == false && colFollower.isPrimed == false)
                {
                    colFollower.isPrimed = true;
                }
            }
        }
    }
}

