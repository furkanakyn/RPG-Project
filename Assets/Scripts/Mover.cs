using UnityEngine;
using UnityEngine.AI;
public class Mover : MonoBehaviour
{

    Ray ray;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveCharacter();
        }
        UpdateAnimator();
    }
    void MoveCharacter()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool moveHit = Physics.Raycast(ray, out hit);
        if (moveHit == true)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
    void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("fowardSpeed", speed);
    }
}
