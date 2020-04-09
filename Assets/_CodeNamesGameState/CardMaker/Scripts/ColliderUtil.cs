using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUtil : MonoBehaviour
{
    public CardColor GetColor()
    {
        if (colliders.Count == 0)
        {
            return CardColor.Brown;
        }

        for (int i = 0; i < colliders.Count; i++)
        {
            try
            {
                CardColor color = (CardColor)System.Enum.Parse(typeof(CardColor), colliders[i].tag);
                return color;
            }
            catch
            {
                //Debug.Log("Warning: Deleted game object not implemented");
            }
            
        }

        return CardColor.Brown;
        
    }

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }


}
