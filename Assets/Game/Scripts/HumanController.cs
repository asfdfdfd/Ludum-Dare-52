using UnityEngine;

public class HumanController : MonoBehaviour
{
    public void DestroyWithShaiHuludTeeths()
    {
        Destroy(gameObject);
    }

    public void DestroyWithHeight()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            DestroyWithHeight();
        }
    }
}
