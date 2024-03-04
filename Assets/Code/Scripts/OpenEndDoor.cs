using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEndDoor : MonoBehaviour
{
    [SerializeField] private Transform DoorSprite;
    [SerializeField] private BoxCollider2D DoorCollider;

    private bool done = false;

    public void Open()
    {
        if (done) return;
        done = true;
        GetComponent<InteractableEntity>().enabled = false;

        StartCoroutine(Animate());
    }

    private void Update()
    {
        if(TempFlags.Check("basementDoor"))
        {
            Open();
        }
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(2);

        float lerp = 0;
        Vector2 op = DoorSprite.transform.position;

        while(lerp < 1)
        {
            lerp += 0.15f * Time.deltaTime;
            DoorSprite.transform.position = Vector2.Lerp(op, op + new Vector2(0, -4.2f), lerp);

            yield return null;
        }

        DoorCollider.enabled = false;
    }
}
