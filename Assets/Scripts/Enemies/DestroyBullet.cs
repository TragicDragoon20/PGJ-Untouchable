using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField]
    private float trailTimer = 0f;
    [SerializeField]
    private float playerForce = 0f;

    private TrailRenderer trailRenderer;

    private ObjectPooler objectPooler;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        objectPooler = ObjectPooler.Instance;
        trailRenderer = this.GetComponent<TrailRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit PLayer");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * playerForce);
            KillPlayer(collision.gameObject);
        }
        rb.velocity = Vector3.zero;
        objectPooler.ReturnObject(this.gameObject);
    }

    private void KillPlayer(GameObject player)
    {
        Destroy(player);
    }
}
