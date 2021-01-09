using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SearchTag
{
    
    public static bool search(string tag, GameObject obj)
    {
        Tag tagComponent = obj.GetComponent<Tag>();
        if (tagComponent == null)
            return false;
        return tagComponent.searchInTag(tag);
    }



}
