using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax_manager : MonoBehaviour
{

    public bool can_move ;
    public bool move_left;
    public float speed=5;
    public float speed_reducer = 0.8f;
    public List<GameObject> layers = new List<GameObject>();
    List<float> layers_end_right = new List<float>();
    List<float> layers_end_left = new List<float>();
    List<float> layers_speed = new List<float>();

    bool temp_canmove;
    void Start()
    {
        temp_canmove = can_move;
        can_move = false;
        make_active();
       
        can_move = temp_canmove;
    }

    void Update()
    {
        if (can_move)
        {
            foreach (var item in layers)
            {
                if (move_left)
                {
                    if (item.transform.localPosition.x <= get_end(item))
                    {
                        item.transform.localPosition = new Vector3(item.transform.localPosition.x + Mathf.Abs(get_end(item)), item.transform.localPosition.y, item.transform.localPosition.z);
                    }
                    else
                    {
                        item.transform.localPosition += Vector3.left * get_speed(item) * Time.deltaTime;
                    }
                }
                else
                {
                    if (item.transform.localPosition.x >= get_end(item))
                    {
                        item.transform.localPosition = new Vector3(item.transform.localPosition.x - Mathf.Abs(get_end(item)), item.transform.localPosition.y, item.transform.localPosition.z);

                    }
                    else
                    {
                        item.transform.localPosition += Vector3.right * get_speed(item) * Time.deltaTime;
                    }
                }

            }
        }

    }


    void make_active()
    {
        foreach (var item in layers)
        {

            GameObject g = Instantiate(item, item.transform);
            g.transform.localScale = Vector3.one;
            GameObject g1 = Instantiate(g, item.transform);
            g1.transform.localScale = Vector3.one;

            float w = item.GetComponent<SpriteRenderer>().sprite.rect.width;
            float p = item.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            float x_plus = w / p;
            g.transform.localPosition = new Vector3(g.transform.localPosition.x + x_plus, 0, 0);
            g1.transform.localPosition = new Vector3(g1.transform.localPosition.x - x_plus, 0, 0);

            float ep = (w / p) * item.transform.localScale.x;
            layers_end_right.Add(ep);
            layers_end_left.Add(ep * -1);



        }
        float sp = speed;
        for (int i = 0; i < layers.Count; i++)
        {

            sp = sp * speed_reducer;
            layers_speed.Add(sp);

        }
    }

    //getting end point for right-move or left-move!
    float get_end(GameObject gm)
    {
        int i = layers.IndexOf(gm);
        if (move_left)
        {
            return layers_end_left[i];
        }
        else
        {
            return layers_end_right[i];
        }
    }
    //getting speed
    float get_speed(GameObject gm)
    {
        int i = layers.IndexOf(gm);
      
        return layers_speed[i];
        
    }

}
