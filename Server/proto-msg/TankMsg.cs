//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: TankMsg.proto
namespace TankMsg
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TipsError")]
  public partial class TipsError : global::ProtoBuf.IExtensible
  {
    public TipsError() {}
    
    private int _code;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"code", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int code
    {
      get { return _code; }
      set { _code = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Vector_3")]
  public partial class Vector_3 : global::ProtoBuf.IExtensible
  {
    public Vector_3() {}
    
    private float _x;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float x
    {
      get { return _x; }
      set { _x = value; }
    }
    private float _y;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float y
    {
      get { return _y; }
      set { _y = value; }
    }
    private float _z;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float z
    {
      get { return _z; }
      set { _z = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Color")]
  public partial class Color : global::ProtoBuf.IExtensible
  {
    public Color() {}
    
    private float _r;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"r", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float r
    {
      get { return _r; }
      set { _r = value; }
    }
    private float _g;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"g", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float g
    {
      get { return _g; }
      set { _g = value; }
    }
    private float _b;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"b", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float b
    {
      get { return _b; }
      set { _b = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TankDTO")]
  public partial class TankDTO : global::ProtoBuf.IExtensible
  {
    public TankDTO() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _hp;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"hp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int hp
    {
      get { return _hp; }
      set { _hp = value; }
    }
    private Vector_3 _pos;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"pos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 pos
    {
      get { return _pos; }
      set { _pos = value; }
    }
    private Color _color;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"color", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Color color
    {
      get { return _color; }
      set { _color = value; }
    }
    private int _team;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"team", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int team
    {
      get { return _team; }
      set { _team = value; }
    }
    private string _nickName;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"nickName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string nickName
    {
      get { return _nickName; }
      set { _nickName = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqRegister")]
  public partial class ReqRegister : global::ProtoBuf.IExtensible
  {
    public ReqRegister() {}
    
    private string _account;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private string _pwd;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pwd", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string pwd
    {
      get { return _pwd; }
      set { _pwd = value; }
    }
    private string _nickname;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"nickname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string nickname
    {
      get { return _nickname; }
      set { _nickname = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RspRegister")]
  public partial class RspRegister : global::ProtoBuf.IExtensible
  {
    public RspRegister() {}
    
    private string _account;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private string _pwd;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pwd", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string pwd
    {
      get { return _pwd; }
      set { _pwd = value; }
    }
    private string _nickname;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"nickname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string nickname
    {
      get { return _nickname; }
      set { _nickname = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqLogin")]
  public partial class ReqLogin : global::ProtoBuf.IExtensible
  {
    public ReqLogin() {}
    
    private string _account;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private string _pwd;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pwd", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string pwd
    {
      get { return _pwd; }
      set { _pwd = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ItemDTO")]
  public partial class ItemDTO : global::ProtoBuf.IExtensible
  {
    public ItemDTO() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private string _account;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private int _itemid;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"itemid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemid
    {
      get { return _itemid; }
      set { _itemid = value; }
    }
    private int _count;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int count
    {
      get { return _count; }
      set { _count = value; }
    }
    private int _slot;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"slot", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int slot
    {
      get { return _slot; }
      set { _slot = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EquipDTO")]
  public partial class EquipDTO : global::ProtoBuf.IExtensible
  {
    public EquipDTO() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private string _account;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private int _tank;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"tank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int tank
    {
      get { return _tank; }
      set { _tank = value; }
    }
    private int _bullet;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"bullet", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int bullet
    {
      get { return _bullet; }
      set { _bullet = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RspLogin")]
  public partial class RspLogin : global::ProtoBuf.IExtensible
  {
    public RspLogin() {}
    
    private string _account;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private string _pwd;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pwd", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string pwd
    {
      get { return _pwd; }
      set { _pwd = value; }
    }
    private string _nickname;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"nickname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string nickname
    {
      get { return _nickname; }
      set { _nickname = value; }
    }
    private int _diamond;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"diamond", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int diamond
    {
      get { return _diamond; }
      set { _diamond = value; }
    }
    private readonly global::System.Collections.Generic.List<ItemDTO> _items = new global::System.Collections.Generic.List<ItemDTO>();
    [global::ProtoBuf.ProtoMember(5, Name=@"items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ItemDTO> items
    {
      get { return _items; }
    }
  
    private EquipDTO _equip;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"equip", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public EquipDTO equip
    {
      get { return _equip; }
      set { _equip = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqBuy")]
  public partial class ReqBuy : global::ProtoBuf.IExtensible
  {
    public ReqBuy() {}
    
    private int _itemid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemid
    {
      get { return _itemid; }
      set { _itemid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RspBuy")]
  public partial class RspBuy : global::ProtoBuf.IExtensible
  {
    public RspBuy() {}
    
    private ItemDTO _item;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"item", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ItemDTO item
    {
      get { return _item; }
      set { _item = value; }
    }
    private int _diamond;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"diamond", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int diamond
    {
      get { return _diamond; }
      set { _diamond = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqUseTank")]
  public partial class ReqUseTank : global::ProtoBuf.IExtensible
  {
    public ReqUseTank() {}
    
    private int _itemid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemid
    {
      get { return _itemid; }
      set { _itemid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RspUseTank")]
  public partial class RspUseTank : global::ProtoBuf.IExtensible
  {
    public RspUseTank() {}
    
    private int _itemid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemid
    {
      get { return _itemid; }
      set { _itemid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqUseBullet")]
  public partial class ReqUseBullet : global::ProtoBuf.IExtensible
  {
    public ReqUseBullet() {}
    
    private int _itemid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemid
    {
      get { return _itemid; }
      set { _itemid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RspUseBullet")]
  public partial class RspUseBullet : global::ProtoBuf.IExtensible
  {
    public RspUseBullet() {}
    
    private int _itemid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemid
    {
      get { return _itemid; }
      set { _itemid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqEnterRoom")]
  public partial class ReqEnterRoom : global::ProtoBuf.IExtensible
  {
    public ReqEnterRoom() {}
    
    private int _battleType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"battleType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int battleType
    {
      get { return _battleType; }
      set { _battleType = value; }
    }
    private int _limitNumber;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"limitNumber", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int limitNumber
    {
      get { return _limitNumber; }
      set { _limitNumber = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RspEnterRoom")]
  public partial class RspEnterRoom : global::ProtoBuf.IExtensible
  {
    public RspEnterRoom() {}
    
    private int _roomid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roomid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roomid
    {
      get { return _roomid; }
      set { _roomid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyBattleStart")]
  public partial class NotifyBattleStart : global::ProtoBuf.IExtensible
  {
    public NotifyBattleStart() {}
    
    private int _battleid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"battleid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int battleid
    {
      get { return _battleid; }
      set { _battleid = value; }
    }
    private int _battleType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"battleType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int battleType
    {
      get { return _battleType; }
      set { _battleType = value; }
    }
    private int _numberLimit;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"numberLimit", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int numberLimit
    {
      get { return _numberLimit; }
      set { _numberLimit = value; }
    }
    private readonly global::System.Collections.Generic.List<TankDTO> _tanks = new global::System.Collections.Generic.List<TankDTO>();
    [global::ProtoBuf.ProtoMember(4, Name=@"tanks", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<TankDTO> tanks
    {
      get { return _tanks; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TankKillData")]
  public partial class TankKillData : global::ProtoBuf.IExtensible
  {
    public TankKillData() {}
    
    private string _account;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private int _kill;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"kill", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int kill
    {
      get { return _kill; }
      set { _kill = value; }
    }
    private int _hurt;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"hurt", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int hurt
    {
      get { return _hurt; }
      set { _hurt = value; }
    }
    private int _deathCount;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"deathCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int deathCount
    {
      get { return _deathCount; }
      set { _deathCount = value; }
    }
    private int _order;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"order", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int order
    {
      get { return _order; }
      set { _order = value; }
    }
    private int _team;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"team", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int team
    {
      get { return _team; }
      set { _team = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyBattleEnd")]
  public partial class NotifyBattleEnd : global::ProtoBuf.IExtensible
  {
    public NotifyBattleEnd() {}
    
    private TankKillData _data;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"data", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public TankKillData data
    {
      get { return _data; }
      set { _data = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyArenaEnd")]
  public partial class NotifyArenaEnd : global::ProtoBuf.IExtensible
  {
    public NotifyArenaEnd() {}
    
    private readonly global::System.Collections.Generic.List<TankKillData> _datas = new global::System.Collections.Generic.List<TankKillData>();
    [global::ProtoBuf.ProtoMember(1, Name=@"datas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<TankKillData> datas
    {
      get { return _datas; }
    }
  
    private int _redKillCount;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"redKillCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int redKillCount
    {
      get { return _redKillCount; }
      set { _redKillCount = value; }
    }
    private int _blueKillCount;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"blueKillCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int blueKillCount
    {
      get { return _blueKillCount; }
      set { _blueKillCount = value; }
    }
    private int _winteam;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"winteam", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int winteam
    {
      get { return _winteam; }
      set { _winteam = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyReborn")]
  public partial class NotifyReborn : global::ProtoBuf.IExtensible
  {
    public NotifyReborn() {}
    
    private TankDTO _tank;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"tank", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public TankDTO tank
    {
      get { return _tank; }
      set { _tank = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyOffline")]
  public partial class NotifyOffline : global::ProtoBuf.IExtensible
  {
    public NotifyOffline() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqMove")]
  public partial class ReqMove : global::ProtoBuf.IExtensible
  {
    public ReqMove() {}
    
    private Vector_3 _pos;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"pos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 pos
    {
      get { return _pos; }
      set { _pos = value; }
    }
    private Vector_3 _rot;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"rot", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 rot
    {
      get { return _rot; }
      set { _rot = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyMove")]
  public partial class NotifyMove : global::ProtoBuf.IExtensible
  {
    public NotifyMove() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private Vector_3 _pos;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 pos
    {
      get { return _pos; }
      set { _pos = value; }
    }
    private Vector_3 _rot;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"rot", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 rot
    {
      get { return _rot; }
      set { _rot = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqFire")]
  public partial class ReqFire : global::ProtoBuf.IExtensible
  {
    public ReqFire() {}
    
    private Vector_3 _pos;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"pos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 pos
    {
      get { return _pos; }
      set { _pos = value; }
    }
    private Vector_3 _rot;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"rot", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 rot
    {
      get { return _rot; }
      set { _rot = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyFire")]
  public partial class NotifyFire : global::ProtoBuf.IExtensible
  {
    public NotifyFire() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private Vector_3 _pos;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 pos
    {
      get { return _pos; }
      set { _pos = value; }
    }
    private Vector_3 _rot;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"rot", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 rot
    {
      get { return _rot; }
      set { _rot = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqHit")]
  public partial class ReqHit : global::ProtoBuf.IExtensible
  {
    public ReqHit() {}
    
    private string _enemy;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"enemy", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string enemy
    {
      get { return _enemy; }
      set { _enemy = value; }
    }
    private Vector_3 _bulletPos;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"bulletPos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector_3 bulletPos
    {
      get { return _bulletPos; }
      set { _bulletPos = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyHit")]
  public partial class NotifyHit : global::ProtoBuf.IExtensible
  {
    public NotifyHit() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _damage;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"damage", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int damage
    {
      get { return _damage; }
      set { _damage = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotifyDeath")]
  public partial class NotifyDeath : global::ProtoBuf.IExtensible
  {
    public NotifyDeath() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private TankKillData _data;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"data", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public TankKillData data
    {
      get { return _data; }
      set { _data = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}