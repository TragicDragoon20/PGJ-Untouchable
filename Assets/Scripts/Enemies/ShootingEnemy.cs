using UnityEngine;

public class ShootingEnemy : EnemyAI
{
    [Header("Targeting Shit")]
    [SerializeField]
    protected Transform bulletSpawn;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected float fireRate;
    protected float nextFire;
    [SerializeField]
    protected float errorMargin;
    [SerializeField]
    private float bulletTimer = 0f;
    [SerializeField]
    private AudioClip[] gunShots = null;


    protected override void Tartgeting()
    {
        if (player != null)
        {


            playerDist = this.transform.position - player.transform.position;
            playerDist.y = 0f;

            if (playerDist.magnitude < range)
            {
                Vector3 direction = player.transform.position - this.transform.position;
                direction.y = 0f;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

                if (Time.time > nextFire)
                {
                    audioSource.PlayOneShot(gunShots[Random.Range(0, gunShots.Length)]);
                    Vector3 shootDirection = player.transform.position + Random.insideUnitSphere * errorMargin - bulletSpawn.position;
                    nextFire = Time.time + fireRate;
                    GameObject bullet = objectPooler.GetPooledObject();
                    Debug.Log(bullet);
                    bullet.transform.SetPositionAndRotation(bulletSpawn.position, bulletSpawn.rotation);
                    bullet.SetActive(true);
                    
                    bullet.GetComponent<Rigidbody>().AddForce(shootDirection * bulletSpeed);
                }
            }
        }
    }

    protected override void ChangeState(int id)
    {
        if (id == this.id)
        {
            audioSource.outputAudioMixerGroup = sfxVolume;
            audioSource.spatialBlend = 1.0f;
            state = EnemyState.shooting;
        }
    }
}
