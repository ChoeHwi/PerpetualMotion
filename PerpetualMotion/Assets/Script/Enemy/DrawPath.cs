using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(10)]
public class DrawPath : MonoBehaviour
{
    [SerializeField] LineRenderer line;

    [SerializeField] Transform startPos, endPos;

    private NavMeshPath path;

    void Awake()
    {
        path = new NavMeshPath();
        //var result = NavMesh.CalculatePath(startPos.position, endPos.position, NavMesh.AllAreas, path);
        var corners = path.corners;
        line.positionCount = corners.Length;
        line.SetPositions(corners);
        
        /*enabled = line.enabled = result;

        if (result)
        {
            
        }*/
    }

    void OnEnable()
    {
        
    }
}
