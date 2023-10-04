using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    int ID;
    public GameObject MergedObject;
    Transform Block1;
    Transform Block2;
    public float Distance;
    public float MergeSpeed;
    public GameObject vfxMerge;
    bool CanMerge;

    public bool onFight = false;

    void Start()
    {
        ID = GetInstanceID();
    }

    private void FixedUpdate()
    {
        MoveTowards();
    }
    public void MoveTowards()
    {
        if (CanMerge)
        {
            transform.position = Vector2.MoveTowards(Block1.position, Block2.position, MergeSpeed);
            if (Vector2.Distance(Block1.position, Block2.position) < Distance)
            {
                if (ID < Block2.gameObject.GetComponent<Merge>().ID) { return; }
                GameObject vfx = Instantiate(vfxMerge, transform.position, Quaternion.identity) as GameObject;
                GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity) as GameObject;
                Destroy(vfx, 1f);
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].soldiers.Remove(gameObject.GetComponent<Soldier>());
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].soldiers.Remove(Block2.gameObject.GetComponent<Soldier>());
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].soldiers.Add(O.GetComponent<Soldier>());
                O.transform.SetParent(GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObject.transform);
                Destroy(Block2.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onFight) return;
        if (collision.gameObject.CompareTag("MergeBlock"))
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == GetComponent<SpriteRenderer>().sprite)
            {
                Block1 = transform;
                Block2 = collision.transform;
                CanMerge = true;
                Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                Destroy(GetComponent<Rigidbody2D>());
            }
        }

    }
}