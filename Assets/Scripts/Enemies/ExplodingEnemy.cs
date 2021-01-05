using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplodingEnemy : EnemyAI
{
    [SerializeField]
    private float explosiveForce = 0f;
    [SerializeField]
    private float explosiveRadius = 0f;
    [SerializeField]
    private GameObject explosion = null;

    private Vector3 playerPos;
    private Vector3 thisPos;

    protected override void Exploding()
    {
        playerPos = player.transform.position; 
        thisPos = this.transform.position;

        Vector3 direction = playerPos - thisPos;
        direction.y = 0f;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        playerDist = thisPos - playerPos;
        playerDist.y = 0f;

        if (direction.magnitude > .8)
        {
            moveDir = direction.normalized;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (playerDist.magnitude < range)
        {
            GameObject explosive = Instantiate(explosion, thisPos, this.transform.rotation);
            player.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, thisPos, explosiveRadius);
            Destroy(this.gameObject);
            Destroy(player);
        }
    }
    protected override void ChangeState(int id)
    {
        if (id == this.id)
        {
            state = EnemyState.exploding;
        }
    }
}
