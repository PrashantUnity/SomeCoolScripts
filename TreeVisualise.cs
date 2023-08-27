public class TreeVisualise
{
  public static void Main()
  {
    var root = new BinaryTree();
    for (var i = 1; i < 15; i++)
        root.Insert(random.Next(1, 1000000));
    var str = root.InOrder(root.root,0); 
    
    var outputls = str.Split('\n');
    var finalOutPut = new List<string>();
    int c = 0;
    foreach (var item in outputls)
    {
        if(item.EndsWith("leaf"))
        {
            finalOutPut.Add(item.Replace("leaf","leaf" + c++));
        }
    }
    foreach (var item in finalOutPut)
        Console.WriteLine(item);
  }
}
public class BinaryTree
{
    public Node root;
    public void Insert(int data)
    {
        var node = new Node(data);
        if (root == null)
        {
            root = node;
            return;
        }

        var current = root;

        while (true)
        {
            if (current.data > data)
            {
                if (current.leftChild == null)
                {
                    current.leftChild = node;
                    break;
                }
                current = current.leftChild;
            }
            else if (current.data < data)
            {
                if (current.rightChild == null)
                {
                    current.rightChild = node;
                    break;
                }
                current = current.rightChild;
            }
        }
    }
    public string InOrder(Node node,int i)
    {
        if (node == null)
            return "leaf"+ "\n";
        return node.data + "-->" + InOrder(node.leftChild, ++i) + node.data + "-->" + InOrder(node.rightChild, ++i);
    }
}
/*
this can be visualise in .Net Interactive tools using mermaid
can be viewed here https://github.com/PrashantUnity/SomeCoolScripts/blob/main/Screenshot%202023-08-27%20193737.png
flowchart TD
    431048-->286678-->241295-->220590-->172319-->leaf0
    172319-->leaf1
    220590-->leaf2
    241295-->276637-->leaf3
    276637-->leaf4
    286678-->350238-->leaf5
    350238-->leaf6
    431048-->914767-->600663-->500141-->485086-->leaf7
    485086-->leaf8
    500141-->leaf9
    600663-->leaf10
    914767-->972670-->964477-->917496-->leaf11
    917496-->leaf12
    964477-->leaf13
    972670-->leaf14
*/
