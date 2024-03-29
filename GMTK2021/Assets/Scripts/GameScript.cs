using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GameScript : MonoBehaviour
    {
        public GameObject followerObject;
        public GameObject rivalObject;
        public bool[] spawnLocation = new bool[29];
        float spawnFollowerCooldown = 1;
        float spawnRivalCooldown = 30;
        public bool followerSearching = false;
        public bool spawningRivalConga = false;
        public bool rivalCongaExists = false;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 29; i++)
            {
                spawnLocation[i] = false;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            if (spawnFollowerCooldown <= 0 && followerSearching == false)
            {
                int fullSlots = 0;
                for (int i = 0; i < 29; i++)
                {
                    if (spawnLocation[i] == true)
                    {
                        fullSlots += 1;
                    }
                }

                if (fullSlots < 20)
                {
                    SpawnNewFollower();
                    spawnFollowerCooldown = Random.Range(1, 3);
                }
            }

            if (spawnFollowerCooldown > 0 && followerSearching == false && spawningRivalConga == false)
            {
                spawnFollowerCooldown -= 0.02f;
            }

            if (spawnRivalCooldown > 0 && rivalCongaExists == false)
            {
                spawnRivalCooldown -= 0.02f;
                Debug.Log(spawnRivalCooldown);
            }

            if (spawnRivalCooldown <= 0 && rivalCongaExists == false)
            {
                spawnRivalCooldown = 60;
                spawningRivalConga = true;
                rivalCongaExists = true;
                SpawnRivalConga();
            }
        }

        void SpawnNewFollower()
        {
            Instantiate(followerObject, transform.position, transform.rotation);
        }

        public void SetSpawnBool(int pos, bool type)
        {
            spawnLocation[pos] = type;
        }
        public bool GetSpawnBool(int pos)
        {
            return spawnLocation[pos];
        }

        void SpawnRivalConga()
        {
            Instantiate(rivalObject, transform.position, transform.rotation);
            int RandNum = Random.Range(15, 20);
            for (int i = 1; i < RandNum; i++)
            {
                Follower followerInstance = Instantiate(followerObject, transform.position, transform.rotation).GetComponent<Follower>();
                followerInstance.isRival = true;
            }
            spawningRivalConga = false;
        }
    }
}

