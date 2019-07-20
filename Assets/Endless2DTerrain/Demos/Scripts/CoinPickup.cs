using UnityEngine;
using System.Collections;
using Endless2DTerrain;

public class CoinPickup : MonoBehaviour {

    public Transform target;
    public int speed = 5;
    bool startMove = false;

    private void Update()
    {
        if(startMove)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
            Material material = GetComponent<MeshRenderer>().material;
            Color color = material.color;
            color.a -= 0.15f; 
            material.color = color;
        }

        Debug.Log("transform.position" + transform.position);
        //Debug.Log("target.position" + target.position);

        if(Mathf.Approximately(transform.position.y, target.position.y))
        {
            Debug.Log("in the if: ");
            var terrainDisplayer = GameObject.FindObjectOfType(typeof(TerrainDisplayer)) as TerrainDisplayer;
            startMove = false;
            terrainDisplayer.PrefabManager.Pool.Remove(this.gameObject);
        }

        Debug.Log("startMove: " + startMove);
    }

    void OnTriggerEnter(Collider item)
    {
        var terrainDisplayer = GameObject.FindObjectOfType(typeof(TerrainDisplayer)) as TerrainDisplayer;
        //Assume only one terrain displayer at a time
        if(item.name == "surfer_Sphere")
        {
            if (terrainDisplayer != null && terrainDisplayer.PrefabManager != null && terrainDisplayer.PrefabManager.Pool != null)
            {
                Scoring.Instance.ScorePickup();
                startMove = true;


                //terrainDisplayer.PrefabManager.Pool.Remove(this.gameObject);
            }
        }
    }

}
