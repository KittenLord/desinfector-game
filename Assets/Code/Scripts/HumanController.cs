using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    [SerializeField] private string SheetName = "default";

    [SerializeField] private HumanSideController Down;
    [SerializeField] private HumanSideController Up;
    [SerializeField] private HumanSideController Left;
    [SerializeField] private HumanSideController Right;
    private List<HumanSideController> sides = new List<HumanSideController>();

    public ItemScript EquippedItem { get; private set; }
    private bool rotateCurrentItem = false;

    private HumanSideController Active;

    public void SetDirection(Vector2 direction)
    {
        var dotUp = Mathf.RoundToInt(Vector2.Dot(Vector2.up, direction));
        var dotRight = Mathf.RoundToInt(Vector2.Dot(Vector2.right, direction));

        if(EquippedItem is not null)
        {
            EquippedItem.SetDirection(direction);

            Vector2 dir = Vector2.zero;
            Quaternion rot = Quaternion.identity;

            if (rotateCurrentItem) { dir = direction; rot = Quaternion.Euler(0, 0, 90); }
            else { dir = FindClosest(direction, Vector2.up, Vector2.down, Vector2.right, Vector2.left); rot = Quaternion.Euler(0, 0, -90); }

            EquippedItem.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir) * rot;
        }

        Debug.Log(dotUp);
        Debug.Log(dotRight);

        if (dotRight == 1) ActivateOne(Right);
        else if (dotRight == -1) ActivateOne(Left);
        else if (dotUp == 1) ActivateOne(Up);
        else if (dotUp == -1) ActivateOne(Down);
    }

    private Vector2 FindClosest(Vector2 reference, params Vector2[] search)
    {
        return search.OrderBy(s => Vector2.Dot(reference, s)).First();
    }

    public void LoadSheet(string sheet)
    {
        if (sheet is null || sheet == "") sheet = "default";

        Down.LoadSheet(sheet, true);
        Up.LoadSheet(sheet, true);
        Right.LoadSheet(sheet, false);
        Left.LoadSheet(sheet, false);
    }

    public void Idle()
    {
        if (Active == null) return;
        Active.Idle();
    }

    public void Move()
    {
        if (Active == null) return;
        Active.Move();
    }

    public void Death()
    {
        if (Active == null) return;
        Active.Death();
    }

    public void EquipHold(Item item)
    {
        if (item is null)
        {
            Destroy(EquippedItem?.gameObject);
            EquippedItem = null;
            return;
        }

        var modelName = item.OverrideModel == "" ? item.Id : item.OverrideModel;
        var itemPrefab = Resources.Load<GameObject>($"ItemPrefabs/{modelName}");

        
        EquippedItem = Instantiate(itemPrefab, this.transform).GetComponent<ItemScript>();

        rotateCurrentItem = item.AimWhenHolding;
    }

    private void ActivateOne(HumanSideController activate)
    {
        if (Active == activate) return;
        Active = activate;

        Down.gameObject.SetActive(false);
        Up.gameObject.SetActive(false);
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);

        Active.gameObject.SetActive(true);
        Active.Idle();
    }

    void Start()
    {
        sides.Add(Down);
        sides.Add(Up);
        sides.Add(Left);
        sides.Add(Right);

        LoadSheet(SheetName);
    }

    void Update()
    {
        
    }
}
