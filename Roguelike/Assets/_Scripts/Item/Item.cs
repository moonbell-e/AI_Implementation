using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string _name;
    public int _id;
    public bool _isStackable;
    public int _count;
    [Multiline(5)]
    public string _description;
    public string _pathInventoryIcon;
    public string _pathSceneItem;
}
