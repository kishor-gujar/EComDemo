namespace EComDemo.Models;

public class Order
{
    public int Id { get; set; }
    public string FistName { get; set; }
    public string Lastname { get; set; }
    public string Address { get; set; }

    public virtual List<OrderProduct> Products { get; set; }
}