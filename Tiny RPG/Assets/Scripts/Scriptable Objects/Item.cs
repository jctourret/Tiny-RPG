using UnityEngine;
public abstract class Item : ScriptableObject
{
    [Header("Basics")]
    new public string name;
    public int id;
    public Sprite sprite;
    public int value;
    public int weight;
    public int stackLimit;
    public abstract void Use();
}
