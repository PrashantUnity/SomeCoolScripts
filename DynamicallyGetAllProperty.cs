using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

// live source code 
// https://paiza.io/projects/e/MRMde9fho_VfpRXhNX2nZQ?theme=twilight
public class Program
{
  public static void Main()
  {
      var temp = new Product()
      {
          Name = "Ek Number (No.1) - 1500 Bricks (1 Trailer)",
          Price = "₹14,700.00",
          OriginalPrice = "₹20,700.00",
          PercentDiscount = "30%",
          FrameSize = "10.25 x 5.25 x 3.25 inch",
          BricksSize = "10 x 5 x 3 inch (approx.)",
          Usage = "Wall/Floor/etc.",
          Description = "Ek Number (No.1) is the best quality of red brick commonly",
          Note = "For deliveries beyond 3 kms from our brick kiln, carriage charge ",
          Images = new List<string> {
              "https://picsum.photos/200",
              "https://picsum.photos/200",
              "https://picsum.photos/200",
              "https://picsum.photos/200"
          }
      };
      temp.Specification = new Specification()
      {
          BrickModel = "Ek Number (No.2)",
          CompressiveStrength = "105 kg/cm^2",
          Brand = "A.B Int Udyog",
          BricksShape = "Cuboid",
          MainIndegredents = "Clay",
          Color = "Deep Red",
          CoverageArea = "50 sq. inch",
          ProductType = "Red Brick",
          FireResistant = "Yes",
          HeatResistant = "Yes",
          WaterResistant = "Yes",
          WaterAbsorption = "12-15%",
          Thicknness = "3 inch",
          Density = "1920 kg/m^3",
          Weight = "4.7 kg",
          Waterproof = "No",
          Tolerance = "0.25 inch",
          DryingShrinkage = "5-8%",
          DeliveryTime = "2-4 Days",
          MinimumOrderQuantity = "1500 Bricks",
          PackagingDetails = "The order is delivered using a tractor trolley having 1500 bricks capacity."
      }
  }
  var ls = new List<(string,string)>();
  var propertyLs = new List<string>();
  foreach (var property in temp.GetType().GetProperties())
  {
       propertyLs.Add(property.GetValue(temp).ToString() );
      
  }
  Console.WriteLine();
  //Thread.Sleep(300); 
  var propertName = new List<string>();
  foreach (var property in typeof(Product).GetProperties())
  {
      propertName.Add(property.Name.ToString()); 
  }
  
  for(var i=0;i<propertName.Count;i++)
  {
      ls.Add((propertName[i],propertyLs[i]));
  }
  Console.WriteLine($"{"Properties",-20}{"Value",-15}");
  foreach(var i in ls)
  {
      Console.WriteLine($"{i.Item1,-20}{i.Item2,-15}"); 
  }
 /*
    //result
    
    Properties          Value          
    Name                Ek Number (No.1) - 1500 Bricks (1 Trailer)
    Price               ₹14,700.00     
    OriginalPrice       ₹20,700.00     
    PercentDiscount     30%            
    FrameSize           10.25 x 5.25 x 3.25 inch
    BricksSize          10 x 5 x 3 inch (approx.)
    Usage               Wall/Floor/etc.
    Description         Ek Number (No.1) is the best quality of red brick commonly
    Note                For deliveries beyond 3 kms from our brick kiln, carriage charge 
    Images              System.Collections.Generic.List`1[System.String]
    Specification       Submission#4+Specification
  */
}
// Define a property.
public class Product
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string OriginalPrice { get; set; }
    public string PercentDiscount { get; set; }
    public string FrameSize { get; set; }
    public string BricksSize { get; set; }
    public string Usage { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public List<string> Images { get; set; }
    public Specification Specification { get; set; }
}
public class Specification
{ 
    public string BrickModel { get; set; } 
    public string CompressiveStrength { get; set; } 
    public string Brand { get; set; }
    public string BricksShape { get; set; }
    public string MainIndegredents { get; set; }
    public string Color { get; set; }
    public string CoverageArea { get; set; }
    public string ProductType { get; set; }
    public string FireResistant { get; set; }
    public string HeatResistant { get; set; }
    public string WaterResistant { get; set; }
    public string WaterAbsorption { get; set; }
    public string Thicknness { get; set; }
    public string Density { get; set; }
    public string Weight { get; set; }
    public string Waterproof { get; set; }
    public string Tolerance { get; set; }
    public string DryingShrinkage { get; set; }
    public string DeliveryTime { get; set; }
    public string MinimumOrderQuantity { get; set; }
    public string PackagingDetails { get; set; }
}
