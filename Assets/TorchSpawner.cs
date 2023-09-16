using UnityEngine;

public class TorchSpawner : MonoBehaviour
{
    public GameObject torchPrefab;
    public float spawnRadius = 10f;
    public float despawnRadius = 20f;
    public float spawnInterval = 5f;

    private GameObject[] torches;
    public Transform playerTransform;
    private float lastSpawnTime;
    public int maxTorches=10;

    void Start()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnTorch();
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnInterval && torches.Length < maxTorches)
        {
            SpawnTorch();
            lastSpawnTime = Time.time;
        }

        for (int i = 0; i < torches.Length; i++)
        {
            if (torches[i] != null && Vector3.Distance(torches[i].transform.position, playerTransform.position) > despawnRadius)
            {
                Destroy(torches[i]);
            }
        }
    }

    void SpawnTorch()
    {
        Vector3 randomPos = Random.insideUnitSphere * spawnRadius + playerTransform.position;
        randomPos.y = 0f; // Ensure the torch is at ground level
        GameObject newTorch = Instantiate(torchPrefab, randomPos, Quaternion.identity);
        newTorch.transform.parent = transform;
        AddTorch(newTorch);
    }

    void AddTorch(GameObject torch)
    {
        if (torches == null)
        {
            torches = new GameObject[] { torch };
            return;
        }

        GameObject[] newTorches = new GameObject[torches.Length + 1];
        for (int i = 0; i < torches.Length; i++)
        {
            newTorches[i] = torches[i];
        }
        newTorches[torches.Length] = torch;

        torches = newTorches;
    }
}
