using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SafeUnlock : MonoBehaviour
{
    [SerializeField] private Sprite[] Wheels;
    [SerializeField] private Image[] CurrentWheels;

    [SerializeField] private Transform Door;
    [SerializeField] private BoxCollider2D Collider;

    [SerializeField] private SpriteRenderer ClosedSafe;
    [SerializeField] private SpriteRenderer OpenedSafe;
    [SerializeField] private InteractableEntity SafeEntity;
    [SerializeField] private GameObject Key;

    private int[] WheelIndexes = { 0, 0, 0, 0, 0, 0 };
    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
    public void OnWheelUp(int index) => OnWheel(index, true);
    public void OnWheelDown(int index) => OnWheel(index, false);

    public void OnWheel(int index, bool up)
    {
        var increment = up ? 1 : -1;

        WheelIndexes[index] = mod((WheelIndexes[index] + increment), 10);
        CurrentWheels[index].sprite = Wheels[WheelIndexes[index]];

        var value = ReadValue();
        Debug.Log(value);

        if (Value == value)
        {
            // open door
            Debug.Log("safeOpen");
            StartCoroutine(OpenDoorCoroutine());
            return;
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        ClosedSafe.enabled = false;
        OpenedSafe.enabled = true;
        SafeEntity.enabled = false;
        Key.SetActive(true);

        UI.Main.ToggleSafe(false);
    }

    public string ReadValue()
    {
        return System.Convert.ToInt32(string.Join("", WheelIndexes)).ToString();
    }

    private string Value;
    void Start()
    {
        Value = NoteInspect.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
