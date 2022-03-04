using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class RivalLeader : MonoBehaviour
    {
        Node[] pathNode;
        Vector3 currentPositionHolder;
        int currentNode = 0;

        float movementSpeed = 1f;
        public GameObject lastPerson = null;

        // Start is called before the first frame update
        void Start()
        {
            lastPerson = this.gameObject;
            CheckNode();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            FollowPath();
        }

        void CheckNode()
        {
            pathNode = GameObject.Find("RivalPath").GetComponentsInChildren<Node>();
            Debug.Log(pathNode.Length);
            Debug.Log(currentNode);
            currentPositionHolder = pathNode[currentNode].transform.position;
        }

        void FollowPath()
        {
            float distanceToNode = (currentPositionHolder - transform.position).magnitude;
            //Debug.Log(distanceToNode);
            if (distanceToNode > 0.01f)
            {
                Vector3 angleVector = currentPositionHolder - transform.position;
                float angle = Mathf.Atan2(angleVector.y, angleVector.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                float currentAngle = transform.eulerAngles.z;
                transform.position += Quaternion.Euler(0, 0, currentAngle) * Vector3.right * Time.deltaTime * movementSpeed;
            }
            else
            {
                if (currentNode < pathNode.Length - 1)
                {
                    currentNode++;
                    CheckNode();
                }
                else
                {
                    currentNode = 2;
                }
            }
        }
    }
}

