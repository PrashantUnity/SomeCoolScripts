using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

// live source code 
// https://paiza.io/projects/3evx-Z0Fg0jXd_V_rqzEnw
public class Program
{
  public static void Main()
  {
      var p = new Myproperty()
      {
          Propert1="Get",
          Propert2="Value",
          Propert3="Dynamically",
          Propert4="Of",
          Propert5="Class", 
      };
      var ls = new List<Myproperty>()
      {
          p,p,p,p
      };
      foreach(var i in ls)
      {
          foreach (var property in i.GetType().GetProperties())
          {
              var valueOfProperty = property.GetValue(i);
              Console.Write(valueOfProperty + " ");
          }
          Console.WriteLine();
          //Thread.Sleep(300);
      }
  }

}
// Define a property.
public class Myproperty
{ 
    public string Propert1{get;set;}
    public string Propert2{get;set;}
    public string Propert3{get;set;}
    public string Propert4{get;set;}
    public string Propert5{get;set;} 

}
