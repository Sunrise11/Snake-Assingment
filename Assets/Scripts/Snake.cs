using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    LinkedList<Transform> tail = new LinkedList<Transform>();

     

    Vector2 direction = Vector2.right;
    bool armor = false;
    bool ate = false;
    public GameObject tailPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = -Vector2.right;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = -Vector2.up;
        }
    }

    void Move()
    {
        Vector2 v = transform.position;

        transform.Translate(direction);

        if (ate)
        {
            GameObject g = Instantiate(tailPrefab, v, Quaternion.identity);
            tail.AddFirst(g.transform);
            ate = false;
        }


        if(tail.Count > 0)
        {
            tail.Last().position = v;

            tail.AddFirst(tail.Last());
            tail.RemoveLast();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.StartsWith("FoodPrefab"))
        {
            ate = true;

            Destroy(other.gameObject);
        }
        else if (other.name.StartsWith("ArmorPrefab"))
        {
            armor = true;

            Destroy(other.gameObject);
        }
        else if(armor)
        {
            armor = false;
        }
        else
        {
            GameObject[] tailParts = GameObject.FindGameObjectsWithTag("TailPart");


            foreach(GameObject tailpart in tailParts)
            {
                GameObject.Destroy(tailpart);
            }

            Destroy(gameObject);

            tail.Clear();
        }
       
    }

    
}
