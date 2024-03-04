using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionSelect : MonoBehaviour
{
    [SerializeField] private GameObject Available;
    [SerializeField] private TMP_Text AvailableName;

    [SerializeField] private GameObject Change;

    public void Set(InteractableEntity e)
    {
        if (Available == null || Change == null) return;

        Available.SetActive(true);
        AvailableName.text = e.Name;
    }

    public void SetChange(bool a)
    {
        if (Available == null || Change == null) return;
        Change.SetActive(a);
    }

    public void Hide()
    {
        if (Available == null || Change == null) return;
        Available.SetActive(false);
        Change.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
