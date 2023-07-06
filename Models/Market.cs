using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Store_Db;
public class Market : BaseNotification
{
    private int _id;
    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    private string? _type;
    public string? Type
    {
        get => _type;
        set => SetField(ref _type, value);
    }

    private string? _trade_mark;
    public string? Trade_Mark
    {
        get => _trade_mark; 
        set => SetField(ref _trade_mark, value);
    }

    private string? _model;
    public string? Model
    {
        get => _model;
        set => SetField(ref _model, value);
    }
    private int? _count;
    public int? Count
    {
        get => _count;
        set => SetField(ref _count, value);
    }
    private int? _price;
    public int? Price
    {
        get => _price;
        set => SetField(ref _price, value);
    }
}