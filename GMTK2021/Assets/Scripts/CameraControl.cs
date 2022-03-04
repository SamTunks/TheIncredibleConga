using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject leader;
    private Vector3 offset;
    private Vector3 newPosition;
    public float xmin;
    public float xmax;
    public float ymin;
    public float ymax;
    void Start()
    {
        offset = transform.position;
    }
    void LateUpdate()
    {
        newPosition = leader.transform.position + offset;
        //if(newPosition.x <= xmin)
        //{
        //    newPosition.x = xmin;
        //}else if(newPosition.x >= xmax)
        //{
        //    newPosition.x = xmax;
        //}

        //if (newPosition.y <= ymin)
        //{
        //    newPosition.y = ymin;
        //}
        //else if (newPosition.y >= ymax)
        //{
        //    newPosition.y = ymax;
        //}
        transform.position = newPosition;

    }
}
