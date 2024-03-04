using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UI : MonoBehaviour
{
    [field: SerializeField] public Canvas Canvas { get; private set; }
    [field:SerializeField] public Transform World { get; private set; }

    [SerializeField] private CarveVoodoo Voodoo;
    [SerializeField] private GameObject InspectPainting;
    [SerializeField] private CanvasGroup BlackScreen;
    [SerializeField] private InventoryMainUI Inventory;
    [SerializeField] private InteractionSelect InteractionSelect;
    [SerializeField] private NoteInspect NoteInspect;
    [SerializeField] private SafeUnlock SafeUnlock;
    [SerializeField] private CanvasGroup DamageIndicator;
    [SerializeField] private GameOverAnimate GameOver;
    [SerializeField] private GameObject PauseMenu;

    public static UI Main { get; private set; }
    private void Awake() { Main = this; }


    [field: SerializeField] public InteractionBar InteractionBar { get; private set; }

    public void CallGameOver(int points, int secrets, string grade, bool instant = false)
    {
        if(instant)
        {
            GameOver.gameObject.SetActive(true);
            GameOver.Activate(points, secrets, grade);
            return;
        }

        StartCoroutine(GameOverAnimation(points, secrets, grade));
    }

    public void PauseMenuA()
    {
        if (BlackScreen.alpha > 0.1f) return;
        PauseMenu.SetActive(true);
    }

    private IEnumerator GameOverAnimation(int points, int secrets, string grade)
    {
        GameOver.gameObject.SetActive(false);
        BlackScreen.alpha = 0;

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += Time.deltaTime * 3;
            BlackScreen.alpha = Mathf.Clamp01(lerp);

            yield return null;
        }

        GameOver.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        while(lerp > 0)
        {
            lerp -= Time.deltaTime * 3;
            BlackScreen.alpha = Mathf.Clamp01(lerp);

            yield return null;
        }

        GameOver.Activate(points, secrets, grade);
    }

    public void SetDamageIndicator(float opacity)
    {
        DamageIndicator.alpha = Mathf.Clamp01(opacity);
    }

    public void FinishCurrentElement()
    {
        InteractionBar.FinishCurrentElement();
    }
    public void StartInteraction(Interaction interaction, InteractableEntity player, InteractableEntity other, Action onStart, Action onEnd)
    {
        InteractionBar.StartInteraction(interaction, player, other, onStart, onEnd);
    }

    public GameObject OpenPaintingInspection()
    {
        InspectPainting.SetActive(true);
        return InspectPainting;
    }

    public GameObject OpenVoodoo()
    {
        Voodoo.gameObject.SetActive(true);
        Voodoo.Open();

        return Voodoo.gameObject;
    }

    public void SetInteractions(InteractableEntity entity, List<InteractableEntity> e)
    {
        if (entity is null)
        {
            InteractionSelect.Hide();
            return;
        }

        InteractionSelect.Set(entity);
        InteractionSelect.SetChange(e.Count > 1);
    }

    public CanvasGroup ToggleSafe(bool on)
    {
        var cg = SafeUnlock.GetComponent<CanvasGroup>();
        cg.alpha = on ? 1 : 0;
        cg.blocksRaycasts = on;
        cg.interactable = on;

        return cg;
    }

    public bool ToggleNote()
    {
        NoteInspect.gameObject.SetActive(!NoteInspect.gameObject.activeSelf);
        return NoteInspect.gameObject.activeSelf;
    }

    public bool InventoryOpened => Inventory.gameObject.activeSelf;

    public void OpenInventory(Inventory player, params Inventory[] other)
    {
        Inventory.gameObject.SetActive(true);
        Inventory.Display(player, other);
    }

    public void HideInventory()
    {
        Inventory.gameObject.SetActive(false);
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(false));
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(true));
    }

    private IEnumerator Fade(bool i)
    {
        BlackScreen.gameObject.SetActive(true);

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += 3f * Time.deltaTime;
            BlackScreen.alpha = Mathf.Clamp01(i ? 1 - lerp : lerp);

            yield return null;
        }
    }

    private void Start()
    {
        NoteInspect.Generate();
    }
}
