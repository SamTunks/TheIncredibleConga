using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class Follower : MonoBehaviour
    {
        Node[] pathNode;
        Vector3 currentPositionHolder;
        int pathToFollow;
        int currentNode = 0;
        
        GameObject player;
        GameScript gameScript;
        GameObject toFollow = null;
        public bool isAttracted = false;
        public bool isPrimed = false;
        public bool inConga = false;
        public bool inQueue = false;
        public bool isSpawning = false;
        public bool lookingForSpawn = true;

        public bool isRival = false;

        public int rotationMult = 1;

        public Queue<Vector3> LeaderTrail;
        public Vector3 LastMovement;

        public RuntimeAnimatorController animA;
        public RuntimeAnimatorController animB;
        public RuntimeAnimatorController animC;
        private char characterType;

        protected Animator Animation;

        //public GameObject MasterObject;
        public int Offset;

        
        

        void Start()
        {
            Animation = GetComponent<Animator>();
            //Application.targetFrameRate = 15;

            

            player = GameObject.Find("CongaMan");
            gameScript = GameObject.Find("GameScript").GetComponent<GameScript>();
            LeaderTrail = new Queue<Vector3>();

            if (isRival != true)
            {
                GenerateRandom();
                CheckNode();
                gameScript.followerSearching = true;
            }
            else
            {
                lookingForSpawn = false;
                isAttracted = true;
            }

            int randNum = Random.Range(1, 4);
            switch (randNum)
            {
                case 1:
                    characterType = 'A';
                    Animation.runtimeAnimatorController = animA as RuntimeAnimatorController;
                    break;
                case 2:
                    characterType = 'B';
                    Animation.runtimeAnimatorController = animB as RuntimeAnimatorController;
                    
                    break;
                case 3:
                    characterType = 'C';
                    Animation.runtimeAnimatorController = animC as RuntimeAnimatorController;

                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isAttracted == true && toFollow == null)
            {
                if (isRival == true)
                {
                    RivalLeader RivalScript = GameObject.Find("RivalLeader(Clone)").GetComponent<RivalLeader>();
                    toFollow = RivalScript.lastPerson;
                    RivalScript.lastPerson = gameObject;
                }
                else
                {
                    PlayerScript playerScript = GameObject.Find("CongaMan").GetComponent<PlayerScript>();
                    toFollow = playerScript.lastPerson;
                    playerScript.lastPerson = gameObject;
                }
                
                Animation.Play("CongaAnimation"+ characterType, -1, toFollow.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
            }

            if (isPrimed == true && gameScript.GetSpawnBool(pathToFollow - 1) == true)
            {
                gameScript.SetSpawnBool(pathToFollow - 1, false);
            }
        }

        void FixedUpdate()
        {
            if (isAttracted == true && toFollow != null)
            {
                FindLeader();
                FollowLeader();
            }
            else if(isPrimed == true)
            {
                JoinConga();
            }
            else if (isSpawning == true)
            {
                FollowPath();
            }
            else if (lookingForSpawn == true)
            {
                if (gameScript.GetSpawnBool(pathToFollow - 1) == false)
                {
                    gameScript.SetSpawnBool(pathToFollow - 1, true);
                    gameScript.followerSearching = false;
                    lookingForSpawn = false;
                    isSpawning = true;
                }
                else
                {
                    GenerateRandom();
                    CheckNode();
                }
            }
            else
            {
                Idle();
            }
            
        }

        void FindLeader()
        {
            if (inQueue == true) { inConga = true; }
            if (toFollow.transform.position == LastMovement) return;

            LastMovement = toFollow.transform.position;
            LeaderTrail.Enqueue(toFollow.transform.position);
        }

        void FollowLeader()
        {

            if (LeaderTrail.Count > Offset)
            {
                Vector3 direction = LeaderTrail.Peek() - transform.position;
                transform.position = LeaderTrail.Peek();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                inQueue = true;
                LeaderTrail.Dequeue();
            }
        }

        void JoinConga()
        {
            
            Vector3 playerPos = player.transform.position;
            Vector3 angleVector = playerPos - transform.position;
            float angle = Mathf.Atan2(angleVector.y, angleVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            float currentAngle = transform.eulerAngles.z;
            transform.position += Quaternion.Euler(0, 0, currentAngle) * Vector3.right * Time.deltaTime * 1.2f;
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
                transform.position += Quaternion.Euler(0, 0, currentAngle) * Vector3.right * Time.deltaTime * 0.5f;
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
                    isSpawning = false;
                }                
            }
        }

        void CheckNode()
        {
            currentPositionHolder = pathNode[currentNode].transform.position;
        }

        void Idle()
        {
            transform.Rotate(0, 0, 0.5f * rotationMult);
            float genRand = Random.value;
            if (genRand > 0.99)
            {
                rotationMult *= -1;
            }
            
        }

        void GenerateRandom()
        {
            pathToFollow = Random.Range(1, 30);
            pathNode = GameObject.Find("FollowerPath" + pathToFollow).GetComponentsInChildren<Node>();
        }
    }
}
