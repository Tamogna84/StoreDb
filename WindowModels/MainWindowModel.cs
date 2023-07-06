using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Store_Db.Models;

namespace Store_Db.WindowModels;

public class MainWindowModel : BaseNotification
{
    #region Elements

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
        set
        {
            if (!SetField(ref _type, value)) return;

            Commands();
        }
    }

    private string? _trade_mark;
    public string? Trade_Mark
    {
        get => _trade_mark;
        set
        {
            if (!SetField(ref _trade_mark, value)) return;

            Commands();
        }
    }

    private string? _model;
    public string? Model
    {
        get => _model;
        set
        {
            if (!SetField(ref _model, value)) return;

            Commands();
        }
    }
    private int? _count;
    public int? Count
    {
        get => _count;
        set
        {
            if (!SetField(ref _count, value)) return;

            Commands();
        }
    }
    private int? _price;
    public int? Price
    {
        get => _price;
        set
        {
            if (!SetField(ref _price, value)) return;

            Commands();
        }
    }
    public void Commands()
    {
        AddItemCommand.RaiseCanExecuteChanged();
        ClearCommand.RaiseCanExecuteChanged();
        DeleteCommand.RaiseCanExecuteChanged();
        UpdateCommand.RaiseCanExecuteChanged();
    }
    #endregion

    #region List

    public ObservableCollection<Market> Markets { get; set; }

    private Market _selectedItem;
    public Market SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (!SetField(ref _selectedItem, value)) return;

            Id = _selectedItem.Id;
            Type = _selectedItem.Type;
            Trade_Mark = _selectedItem.Trade_Mark;
            Model = _selectedItem.Model;
            Count = _selectedItem.Count;
            Price = _selectedItem.Price;
        }
    }

    #endregion

    #region Commands

    public LambdaCommand AddItemCommand { get; set; }
    public LambdaCommand ClearCommand { get; set; }
    public LambdaCommand DeleteCommand { get; set; }
    public LambdaCommand UpdateCommand { get; set; }

    #endregion
    public void AllToZero()
    {
        Id = 0;
        Type = string.Empty;
        Trade_Mark = string.Empty;
        Model = string.Empty;
        Count = 0;
        Price = 0;
    }

    public MainWindowModel()
    {
        var db = new DataBaseContext();

        Markets = new ObservableCollection<Market>(db.Market_table);
        SelectedItem = new Market();
        
        AddItemCommand = new LambdaCommand(
            execute: _ =>
            {
                var item = new Market
                {
                    Type = Type,
                    Trade_Mark = Trade_Mark,
                    Model = Model,
                    Count = Count,
                    Price = Price
                };

                Markets.Add(item);
                db.Market_table.Add(item);
                db.SaveChanges();

                AllToZero();
            },
            canExecute: _ => !string.IsNullOrWhiteSpace(Type) &&
                             !string.IsNullOrWhiteSpace(Trade_Mark) &&
                             !string.IsNullOrWhiteSpace(Model)
        );

        ClearCommand = new LambdaCommand(
            execute: _ =>
            {
                AllToZero();
            },
           canExecute: _ => !string.IsNullOrWhiteSpace(Type) &&
                             !string.IsNullOrWhiteSpace(Trade_Mark) &&
                             !string.IsNullOrWhiteSpace(Model)
        );
        DeleteCommand = new LambdaCommand(
           execute: _ =>
           {
               if (MessageBox.Show("Вы хотите удалить товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question)
               == MessageBoxResult.Yes)
               {
                   var CurrentItem = SelectedItem;
                   Markets.Remove(CurrentItem);
                   db.Market_table.Remove(CurrentItem);
                   db.SaveChanges();


               }
           },
          canExecute: _ => !string.IsNullOrWhiteSpace(Type)
       );
        UpdateCommand = new LambdaCommand(
           execute: _ =>
           {
               var CurrentItem = SelectedItem;
               Markets.Remove(CurrentItem);
               db.Market_table.Remove(CurrentItem);
               
               //
               var item = new Market
               {
                   Id = CurrentItem.Id,
                   Type = Type,
                   Trade_Mark = Trade_Mark,
                   Model = Model,
                   Count = Count,
                   Price = Price
               };               
               Markets.Add(item);
               db.Market_table.Add(item);
               db.SaveChanges();
               AllToZero();
           },
          canExecute: _ => !string.IsNullOrWhiteSpace(Type)
       );
    }
}