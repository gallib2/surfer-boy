using UnityEngine;
using System.Collections;
using Endless2DTerrain;

public class CoinPickup : MonoBehaviour {
    public delegate void CoinPicked(bool isPicked);
    public static event CoinPicked OnCoinPicked;

    public AudioClip bottleSound;
    private AudioSource audioSource;

    public int speed = 5;
    bool startMove = false;

    private Color initialColor;
    Material material;
    public Transform target;
    private TerrainDisplayer terrainDisplayer;

    private void Start()
    {
        initialColor = GetComponent<MeshRenderer>().material.color;
        material = GetComponent<MeshRenderer>().material;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(startMove)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
            Color color = material.color;
            color.a -= 0.15f; 
            material.color = color;
        }

        //Debug.Log("transform.position" + transform.position);
        //Debug.Log("target.position" + target.position);

        if(Mathf.Approximately(transform.position.y, target.position.y))
        {
            OnCoinPicked?.Invoke(false);
            //var terrainDisplayer = GameObject.FindObjectOfType(typeof(TerrainDisplayer)) as TerrainDisplayer;
            startMove = false;
            if (terrainDisplayer != null && terrainDisplayer.PrefabManager != null && terrainDisplayer.PrefabManager.Pool != null)
            {
                material.color = initialColor;
                terrainDisplayer.PrefabManager.Pool.Remove(this.gameObject);
                //terrainDisplayer.PrefabManager.Pool.Remove(this.gameObject);
            }
        }

        //Debug.Log("startMove: " + startMove);
    }

    void OnTriggerEnter(Collider item)
    {
        terrainDisplayer = GameObject.FindObjectOfType(typeof(TerrainDisplayer)) as TerrainDisplayer;
        //Assume only one terrain displayer at a time
        if(item.name == "surfer_Sphere")
        {
            if (terrainDisplayer != null && terrainDisplayer.PrefabManager != null && terrainDisplayer.PrefabManager.Pool != null)
            {
                audioSource.PlayOneShot(bottleSound);
                Scoring.Instance.ScorePickup();
                OnCoinPicked?.Invoke(true);
                startMove = true;


                //terrainDisplayer.PrefabManager.Pool.Remove(this.gameObject);
            }
        }
    }

}
