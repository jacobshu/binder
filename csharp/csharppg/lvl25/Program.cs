class Pack
{
  public InventoryItem[] Items;
  public float MaxWeight { get; }
  public float MaxVolume { get; }
  public int MaxItems { get; }
  public int ItemCount { get => Items.Length; }
  public float Weight
  {
    get
    {
      float total = 0;
      for (int i = 0; i < Items.Length; i++)
      {
        total += Items[i].Weight;
      }
      return total;
    }
  }

  public float Volume
  {
    get
    {
      float total = 0;
      for (int i = 0; i < Items.Length; i++)
      {
        total += Items[i].Volume;
      }
      return total;
    }
  }

  public override string ToString()
  {
    string allItems = "The pack has: ";
    for (int i = 0; i < ItemCount; i++)
    {
      allItems += Items[i].ToString() + " ";
    }

    return allItems;  
  }

  public Pack(int itemLimit, float weightLimit, float volumeLimit)
  {
    MaxWeight = weightLimit;
    MaxVolume = volumeLimit;
    Items = new InventoryItem[itemLimit];
  }

  public bool Add(InventoryItem item)
  {
    if (Weight + item.Weight < MaxWeight && Volume + item.Volume < MaxVolume && ItemCount + 1 < MaxItems)
    {
      Array.Resize(ref Items, Items.Length + 1);
      Items[Items.Length - 1] = item;
      return true;
    }
    return false;
  }
}

class InventoryItem
{
  public float Weight { get; set; }
  public float Volume { get; set; }

  public InventoryItem(float weight, float volume)
  {
    Weight = weight;
    Volume = volume;
  }


}

class Arrow : InventoryItem
{
  public Arrow() : base(0.1f, 0.05f) { }

  public override string ToString()
  {
    return "Arrow";
  }
}

class Bow : InventoryItem
{
  public Bow() : base(1f, 4f) { }

  public override string ToString()
  {
    return "Bow";
  }
}

class Rope : InventoryItem
{
  public Rope() : base(1f, 1.5f) { }

  public override string ToString()
  {
    return "Rope";
  }
}

class Water : InventoryItem
{
  public Water() : base(2f, 3f) { }

  public override string ToString()
  {
    return "Water";
  }
}

class Rations : InventoryItem
{
  public Rations() : base(1f, 0.5f) { }

  public override string ToString()
  {
    return "Rations";
  }
}

class Sword : InventoryItem
{
  public Sword() : base(5f, 3f) { }

  public override string ToString()
  {
    return "Sword";
  }
}
