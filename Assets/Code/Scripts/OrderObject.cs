using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderObject : MonoBehaviour
{
    [SerializeField] private Vector2 MiddlePoint = Vector2.zero;
    private Transform Player;

    private List<SpriteRenderer> renderers;
    private bool DoesObstruct = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        renderers = this.GetComponentsInChildren<SpriteRenderer>(true).ToList();

        //if (this.TryGetComponent<Renderer>(out var r)) renderers.Add(r);
        renderers.Where(r => r != null).ToList().ForEach(r => r.sortingOrder -= 10);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 p = (Vector2)transform.position + MiddlePoint;
        p.z = -5;

        var left = p;
        p.x -= 5;

        var right = p;
        p.x += 5;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(left, right);
    }

    // Update is called once per frame
    void Update()
    {
        var delta = (transform.position + (Vector3)MiddlePoint) - Player.position;
        var dy = delta.y;

        if(dy > 0 && DoesObstruct)
        {
            DoesObstruct = false;
            renderers.Where(r => r != null).ToList().ForEach(r => r.sortingOrder = r.sortingOrder - 20);
        }

        if(dy < 0 && !DoesObstruct)
        {
            DoesObstruct = true;
            renderers.Where(r => r != null).ToList().ForEach(r => r.sortingOrder = r.sortingOrder + 20);
        }
    }
}
