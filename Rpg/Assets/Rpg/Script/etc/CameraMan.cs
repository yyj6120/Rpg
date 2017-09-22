using UnityEngine;
public class CameraMan : MonoBehaviour
{
    public Transform targetPos;
    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }
    void LateUpdate()
    {
        if (targetPos.gameObject.activeSelf)
        {
            transform.position = targetPos.position;
        }
    }
}
