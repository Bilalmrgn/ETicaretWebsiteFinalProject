using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class BasketItem { public string Id {get;set;} }
public class Basket { public List<BasketItem> basketItems {get;set;} = new List<BasketItem>(); }

class Program {
    static void Main() {
        var json = "{\"basketItems\":[{\"id\":\"123\"}]}";
        var b = JsonConvert.DeserializeObject<Basket>(json);
        Console.WriteLine(b.basketItems.Count);
    }
}
