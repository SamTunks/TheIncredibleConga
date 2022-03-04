using System.Collections.Generic;
using UnityEngine;
     

namespace Player
{
    public class PlayerTrail : MonoBehaviour
    {
        // ========================================================================
        // FIELDS
        // ========================================================================

        public Queue<Vector3> LeaderTrail;
        public Vector3 LastMovement;

        // ========================================================================
        // METHODS
        // ========================================================================

        private void Start()
        {
            LeaderTrail = new Queue<Vector3>();
        }

        private void FixedUpdate()
        {
            if (transform.position == LastMovement) return;

            LastMovement = transform.position;
            LeaderTrail.Enqueue(transform.position);
        }
    }
}
