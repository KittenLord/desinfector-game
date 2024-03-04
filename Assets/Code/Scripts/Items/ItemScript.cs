using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private GameObject Up;
    [SerializeField] private GameObject Down;
    [SerializeField] private GameObject Right;
    [SerializeField] private GameObject Left;

    private GameObject Active;

    public void SetDirection(Vector2 direction)
    {
        var dotUp = Mathf.RoundToInt(Vector2.Dot(Vector2.up, direction));
        var dotRight = Mathf.RoundToInt(Vector2.Dot(Vector2.right, direction));

        if (dotRight == 1) ActivateOne(Right);
        else if (dotRight == -1) ActivateOne(Left);
        else if (dotUp == 1) ActivateOne(Up);
        else if (dotUp == -1) ActivateOne(Down);
    }

    private void ActivateOne(GameObject activate)
    {
        if (Active == activate) return;
        Active = activate;

        Down.gameObject.SetActive(false);
        Up.gameObject.SetActive(false);
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);

        Active.gameObject.SetActive(true);
    }

    protected virtual void Start()
    {

    }
}
