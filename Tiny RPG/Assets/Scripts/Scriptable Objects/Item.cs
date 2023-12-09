using UnityEngine;
public abstract class Item : ScriptableObject
{
    new public string name;
    public int id;
    public Sprite sprite;
    public int value;
    public int weight;
    public int stackLimit;
    public int stack;
    public GameObject owner;
    public abstract void Use();
}
