using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    public List<string> tags = new List<string>();
    public Tag()
    {

    }

    public bool searchInTag(string tag)
    {
        for (int i = 0; i < tags.Count; i++)
            if (tags[i] == tag)
                return true;
        return false;
    }

    public void addTag(string tag)
    {
        tags.Add(tag);
    }

    public string getTag()
    {
        string app = "";
        for (int i = 0; i < tags.Count; i++)
            app += tags[i];
        return app;
    }
}
