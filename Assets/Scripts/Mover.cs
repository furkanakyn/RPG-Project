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
}
