using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Soldier : MonoBehaviour
{
    public TextMeshProUGUI text;

    public SoldierSO soldierSO;

    private int power;

    public bool canAttack = false;

    private float speed;

    public GameObject vfxFail;

    public GameObject vfxKill;

    Vector3 startPos;

    private void Awake()
    {
        power = soldierSO.power;
        speed = soldierSO.speed;
        startPos = transform.position;

        text.SetText("Power : {0}", power);
    }

    private void FixedUpdate()
    {
        if (canAttack)
        {
            Attack(GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].GetTarget());
        }
    }

    void Attack(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Enemy") && canAttack)
        {
            if (power > collision.gameObject.GetComponent<Enemy>().power)
            {
                GameObject vfx = Instantiate(vfxKill, transform.position, Quaternion.identity) as GameObject;
                Destroy(vfx, 1f);
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(collision.gameObject);
                Destroy(collision.gameObject);
                GameManager.Instance.CheckLevelUp();
            }
            else
            {
                ResetPos();
            }
        }
    }

    void ResetPos()
    {
        GameObject vfx = Instantiate(vfxFail, transform.position, Quaternion.identity) as GameObject;
        Destroy(vfx, 1f);
        transform.position = startPos;
        canAttack = false;
        GetComponent<Merge>().onFight = false;
    }
}