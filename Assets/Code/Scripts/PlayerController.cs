using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public HumanController Human;
    private HumanEntity Entity;
    private Rigidbody2D rb;

    private Camera MainCamera;
    private Vector2 inputDirection;

    private List<InteractableEntity> AvailableInteractions = new List<InteractableEntity>();

    private int InteractionsSafeLength => Mathf.Max(AvailableInteractions.Count, 1);
    private int SelectedAvailableInteraction = 0;

    public bool canMove = true;
    public bool canInteract = true;
    public bool canOpenInventory = true;
    public bool canUseItems = true;

    public bool AimAtMouse = false;

    public float DefaultSpeed = 0.1f;
    public float SpeedMultiplier = 1;

    private float Health = 100;
    private bool TakenDamage = false;
    public void Damage()
    {
        Health -= 0.5f;
        TakenDamage = true;

        if (Health < 0) Die();

        if (!isDamaged) StartCoroutine(DamageAnimation());
    }

    private bool isDamaged = false;
    private IEnumerator DamageAnimation()
    {
        var cs = Human.GetComponent<ColorSprites>();
        isDamaged = true;

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += Time.deltaTime * 3;
            UI.Main.SetDamageIndicator(lerp);

            cs.SetColor(Color.Lerp(Color.white, Color.green, lerp));

            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        while (lerp > 0)
        {
            lerp -= Time.deltaTime * 3;
            UI.Main.SetDamageIndicator(lerp);

            cs.SetColor(Color.Lerp(Color.white, Color.green, lerp));

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        isDamaged = false;
    }

    private bool isDead = false;
    private void Die()
    {
        isDead = true;
        this.UseItemEnd();
        handInventory.SetSlot(0, null);
        Human.SetDirection(new Vector2(1, 0));

        canInteract = false;
        canMove = false;
        canOpenInventory = false;
        canUseItems = false;

        GetComponents<Collider2D>().ToList().ForEach(c => c.enabled = false);
        StartCoroutine(DieAnimation());
    }

    private int CalculatePoints()
    {
        var points = (BugNestScript.TotalBugs - BugNestScript.RemainingBugs) * 1000;
        if (TempFlags.Check("paintingActivated123")) points += 5000;
        if (TempFlags.Check("bookshelfSecret")) points += 5000;
        if (TempFlags.Check("voodooDone")) points += 5000;
        if (TempFlags.Check("altar")) points += 5000;
        if (TempFlags.Check("basementDoor")) points += 5000;
        if (TempFlags.Check("bigbigsecret")) points += 5000;

        return points;
    }

    private int CalculateSecrets()
    {
        int secrets = 0;
        if (TempFlags.Check("basementDoor")) secrets++;
        if (TempFlags.Check("bigbigsecret")) secrets++;

        return secrets;
    }

    private string GetGrade()
    {
        if (TempFlags.Check("bigbigsecret")) return "S+";

        var killedBugs = BugNestScript.TotalBugs - BugNestScript.RemainingBugs;
        var remainingBugs = BugNestScript.RemainingBugs;
        var bugsRemain = remainingBugs > 0;

        var takenDamage = TakenDamage;
        var isDead = this.isDead;

        if(isDead && !TempFlags.Check("paintingActivated123")) // Player died before the painting
        {
            if (remainingBugs > 30) return "F";
            if (remainingBugs > 20) return "D";
            if (remainingBugs > 0) return "C";
            return "J";
        }

        if(!bugsRemain && !isDead && !TempFlags.Check("basementDoor")) // Player did the deed, maybe tried but failed to solve stuff, and drove away
        {
            if (takenDamage) return "C";
            return "B";
        }

        if(!bugsRemain && isDead && TempFlags.Check("paintingActivated123")) // Player found the painting and still died
        {
            return "J";
        }

        if(!bugsRemain && !isDead && TempFlags.Check("basementDoor") && !TempFlags.Check("birthdayActivated")) // Player opened the final door and drove away
        {
            return "S+++";
        }

        if(!isDead && TempFlags.Check("birthdayActivated") && !TempFlags.Check("secretDoor"))
        {
            if (takenDamage) return "A";
            return "S";
        }

        return "S+";
    }

    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(0.8f);

        Human.Death();

        yield return new WaitForSeconds(3);
        EndGame();
    }

    public void EndGame(bool instant = false)
    {
        UI.Main.CallGameOver(CalculatePoints(), CalculateSecrets(), GetGrade(), instant);
    }

    public Inventory Inventory;
    private Inventory handInventory;
    public Inventory bodyInventory;

    public bool OverrideIdle = false;

    void Start()
    {
        MainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        Entity = GetComponent<HumanEntity>();

        var inventories = this.GetComponents<Inventory>().ToList();
        Inventory = inventories.Find(i => i.Id == "player");
        handInventory = inventories.Find(i => i.Id == "hand");
        bodyInventory = inventories.Find(i => i.Id == "body");

        handInventory.OnSetSlot += EquipItem;
        bodyInventory.OnSetSlot += EquipBodyItem;
    }

    public void EquipItem(Item item)
    {
        Entity.HoldingItem?.OnUseEnd(Human.EquippedItem, Entity);
        Human.EquipHold(item);
        Entity.HoldingItem = item;
    }

    public void EquipBodyItem(Item item)
    {
        Human.LoadSheet(item?.Id);
    }

    // Update is called once per frame

    void Update()
    {
        if (isDead) return;
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        var mousePos = (Vector2)MainCamera.ScreenToWorldPoint(Input.mousePosition);
        var mouseDirection = (mousePos - (Vector2)transform.position).normalized;

        var faceDirection = (inputDirection.magnitude > 0 && !AimAtMouse) ? inputDirection : mouseDirection;
        
        if (canMove) Human.SetDirection(faceDirection);

        if (inputDirection.magnitude > 0 && canMove)
        {
            Human.Move();
        }
        else if(!OverrideIdle)
        {
            Human.Idle();
        }

        if (!canInteract)
        {
            UI.Main.SetInteractions(null, null);
        }
        else if(Time.frameCount % 60 == 0)
        {
            SetInteractionUI();
        }

        var a = AvailableInteractions.RemoveAll(l => !l.enabled);
        if (a > 0) SetInteractionUI();

        if(Input.GetKeyDown(KeyCode.R)) // Switch interaction
        {
            SelectedAvailableInteraction = (SelectedAvailableInteraction + 1) % InteractionsSafeLength;
            SetInteractionUI();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            UI.Main.FinishCurrentElement();
            StartInteraction();
        }

        if(Input.GetMouseButtonDown(0) && canUseItems)
        {
            UseItemStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            UseItemEnd();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !UI.Main.InventoryOpened)
        {
            UI.Main.PauseMenuA();
        }

        if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UI.Main.InventoryOpened && canOpenInventory && !Input.GetKeyDown(KeyCode.Escape))
            {
                canInteract = false;
                canUseItems = false;
                UI.Main.OpenInventory(Inventory, handInventory, bodyInventory);
            }
            else if (UI.Main.InventoryOpened && canOpenInventory)
            {
                canInteract = true;
                canUseItems = true;
                UI.Main.HideInventory();
            }
        }



        if(Application.isEditor)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                SpeedMultiplier = 5;
            }

            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                SpeedMultiplier = 1;
            }

            if(Input.GetKeyDown(KeyCode.M))
            {
                var bugs = FindObjectsOfType<BugNestScript>();
                foreach (var bug in bugs) bug.Die();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                var inv = GameObject.FindObjectOfType<InventoryUI>();
                inv.Display(GetComponent<Inventory>());
            }
        }
    }

    void UseItemStart()
    {
        if (Entity.HoldingItem is null || !canUseItems) return;

        var itemScript = Human.EquippedItem;
        Entity.HoldingItem.OnUseStart(itemScript, Entity);
    }

    public void UseItemEnd()
    {
        if (Entity.HoldingItem is null) return;

        var itemScript = Human.EquippedItem;
        Entity.HoldingItem.OnUseEnd(itemScript, Entity);
    }

    void UseItem()
    {
        if (Entity.HoldingItem is null || !canUseItems) return;

        var itemScript = Human.EquippedItem;
        Entity.HoldingItem.OnUse(itemScript, Entity);
    }

    void StartInteraction()
    {
        if (AvailableInteractions.Count == 0) return;
        if (!canInteract) return;

        canInteract = false;

        SelectedAvailableInteraction %= AvailableInteractions.Count;

        var interactionEntity = AvailableInteractions[SelectedAvailableInteraction];
        var interactionId = interactionEntity.InteractionId;

        if (!Data.Loaded.Interactions.ContainsKey(interactionId)) return;
        var interaction = Data.Loaded.Interactions[interactionId];

        UI.Main.StartInteraction(interaction, GetComponent<InteractableEntity>(), interactionEntity, 
            () => { canInteract = false; canMove = false; canUseItems = false; canOpenInventory = false; }, 
            () => { canInteract = true; canMove = true; canUseItems = true; canOpenInventory = true; });
    }

    public void StartInteractionOutside(Interaction interaction)
    {
        if (!canInteract) return;
        canInteract = false;
        UI.Main.StartInteraction(interaction, GetComponent<InteractableEntity>(), null,
            () => { canInteract = false; canMove = false; canUseItems = false; canOpenInventory = false; },
            () => { canInteract = true; canMove = true; canUseItems = true; canOpenInventory = true; });
    }

    void FixedUpdate()
    {
        if (isDead) return;

        Health = Mathf.Clamp(Health + 0.01f, -100, 100);
        Debug.Log(Health);

        if (canMove)
            rb.MovePosition(transform.position + (Vector3)inputDirection * (DefaultSpeed * SpeedMultiplier));

        if (Input.GetMouseButton(0))
        {
            UseItem();
        }
    }

    private void SetInteractionUI()
    {
        var e = AvailableInteractions.Count != 0 ? AvailableInteractions[SelectedAvailableInteraction % AvailableInteractions.Count] : null;
        UI.Main.SetInteractions(e, AvailableInteractions);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<InteractableEntity>(out var interactableEntity) || !interactableEntity.enabled || interactableEntity == Entity || AvailableInteractions.Contains(interactableEntity)) return;
        AvailableInteractions.Add(interactableEntity);
        SetInteractionUI();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<InteractableEntity>(out var interactableEntity) || interactableEntity == Entity) return;
        AvailableInteractions.Remove(interactableEntity);

        var e = AvailableInteractions.Count != 0 ? AvailableInteractions[SelectedAvailableInteraction % AvailableInteractions.Count] : null;
        UI.Main.SetInteractions(e, AvailableInteractions);
        SetInteractionUI();
    }
}
