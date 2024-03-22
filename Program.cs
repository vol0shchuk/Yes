using System;
using System.Threading;
public class CoffeeMachineEventArgs : EventArgs
{
    public string Message { get; private set; }

    public CoffeeMachineEventArgs(string message)
    {
        Message = message;
    }
}

public class CoffeeMachine
{
    public event EventHandler<CoffeeMachineEventArgs> NotificationEvent;

    private int waterLevel; 
    private int coffeeBeans; 
    private decimal money;

    public CoffeeMachine()
    {
        waterLevel = 100;
        coffeeBeans = 100;
        money = 0;
    }

    public void MakeCoffee()
    {
        if (waterLevel >= 20 && coffeeBeans >= 10)
        {
            waterLevel -= 20;
            coffeeBeans -= 10;
            money += 1.5m;
            OnNotificationEvent("Coffee is ready!");
        }
        else if (waterLevel < 20)
        {
            OnNotificationEvent("Not enough water!");
        }
        else if (coffeeBeans < 10)
        {
            OnNotificationEvent("Not enough coffee beans!");
        }
    }


    public void AddWater(int amount)
    {
        waterLevel += amount;
        OnNotificationEvent($"Water added: {amount} ml.");
    }

    public void AddCoffee(int amount)
    {
        coffeeBeans += amount;
        OnNotificationEvent($"Coffee added: {amount} g.");
    }

    public void InsertMoney(decimal amount)
    {
        money += amount;
        OnNotificationEvent($"Money added: {amount} USD.");
    }

    protected virtual void OnNotificationEvent(string message)
    {
        NotificationEvent?.Invoke(this, new CoffeeMachineEventArgs(message));
    }
}

class Program
{
    static void Main(string[] args)
    {
        CoffeeMachine coffeeMachine = new CoffeeMachine();

        coffeeMachine.NotificationEvent += HandleNotification;

        coffeeMachine.InsertMoney(1.0m);
        coffeeMachine.MakeCoffee();
        coffeeMachine.AddWater(50);
        coffeeMachine.MakeCoffee();
        coffeeMachine.AddCoffee(30);
        coffeeMachine.MakeCoffee();

        Console.ReadLine();
    }

    static void HandleNotification(object sender, CoffeeMachineEventArgs e)
    {
        Console.WriteLine($"Notification: {e.Message}");
    }
}
