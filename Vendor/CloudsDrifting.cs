using UnityEngine;

public class CloudsDrifting : MonoBehaviour
{
    [SerializeField] [Tooltip ("Set the cloud move speed here:")]
    float cloudMoveSpeed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*cloudMoveSpeed);
    }
}
