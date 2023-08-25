// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial  
// declarations in another file.
// </auto-generated>

using Quantum;
using UnityEngine;

[CreateAssetMenu(menuName = "Quantum/UnitData", order = Quantum.EditorDefines.AssetMenuPriorityStart + 520)]
public partial class UnitDataAsset : AssetBase {
  public Quantum.UnitData Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new Quantum.UnitData();
    }
    base.Reset();
  }
}

public static partial class UnitDataAssetExts {
  public static UnitDataAsset GetUnityAsset(this UnitData data) {
    return data == null ? null : UnityDB.FindAsset<UnitDataAsset>(data);
  }
}