using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGate : MonoBehaviour
{
    [SerializeField] float raiseHeight;
    [SerializeField] float raiseSpeed;
    BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
    }
    public IEnumerator RaiseGate()
    {
        while (transform.position.y < raiseHeight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, raiseHeight), raiseSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        boxCollider.enabled = false;

    }

    public void SetRaised()
    {
        transform.position = new Vector3(transform.position.x, raiseHeight);
    }
}
