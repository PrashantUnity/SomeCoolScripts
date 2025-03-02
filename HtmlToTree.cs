using System;
using System.Collections.Generic;

public class HtmlTreeNode
{
    public string TagName { get; set; }
    public List<string> Classes { get; } = new List<string>();
    public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
    public List<HtmlTreeNode> Children { get; } = new List<HtmlTreeNode>();
    public string InnerText { get; set; } = string.Empty;
}

public static class HtmlParser
{
    private static HashSet<string> voidTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        // HTML5 void elements
        "area", "base", "br", "col", "embed", "hr", "img", "input", 
        "link", "meta", "param", "source", "track", "wbr",
        
        // Legacy/obsolete void elements
        "basefont", "bgsound", "frame", "isindex", "keygen", 
        "menuitem", "spacer", "colgroup", "command"
    };


    public static HtmlTreeNode Parse(string html)
    {
        HtmlTreeNode root = new HtmlTreeNode();
        Stack<HtmlTreeNode> stack = new Stack<HtmlTreeNode>();
        HtmlTreeNode currentParent = root;
        int index = 0;

        while (index < html.Length)
        {
            if (html[index] == '<')
            {
                int endIndex = html.IndexOf('>', index);
                if (endIndex == -1) break;

                string tagContent = html.Substring(index + 1, endIndex - index - 1).Trim();
                index = endIndex + 1;

                bool isClosingTag = tagContent.StartsWith("/");
                bool isSelfClosing = false;

                if (isClosingTag)
                {
                    tagContent = tagContent.Substring(1).Trim();
                }
                else if (tagContent.EndsWith("/"))
                {
                    isSelfClosing = true;
                    tagContent = tagContent.Substring(0, tagContent.Length - 1).Trim();
                }

                if (isClosingTag)
                {
                    if (stack.Count > 0)
                        currentParent = stack.Pop();

                    // Check if we've finished processing first root element
                    if (currentParent == root && root.Children.Count > 0)
                    {
                        break;
                    }
                }
                else
                {
                    int firstSpace = tagContent.IndexOf(' ');
                    string tagName = firstSpace == -1 ? tagContent : tagContent.Substring(0, firstSpace).Trim();
                    string attributesString = firstSpace == -1 ? string.Empty : tagContent.Substring(firstSpace).Trim();

                    HtmlTreeNode node = new HtmlTreeNode { TagName = tagName };

                    if (voidTags.Contains(tagName.ToLowerInvariant()))
                        isSelfClosing = true;

                    var attributes = ParseAttributes(attributesString);
                    foreach (var attr in attributes)
                    {
                        node.Attributes[attr.Key] = attr.Value;
                        if (attr.Key.Equals("class", StringComparison.OrdinalIgnoreCase))
                            node.Classes.AddRange(attr.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                    }

                    currentParent.Children.Add(node);

                    // Check if we're adding first root element
                    if (currentParent == root && root.Children.Count == 1)
                    {
                        if (isSelfClosing || voidTags.Contains(tagName.ToLowerInvariant()))
                        {
                            break;
                        }
                    }

                    if (!isSelfClosing)
                    {
                        stack.Push(currentParent);
                        currentParent = node;
                    }
                }

                // Final check after tag processing
                if (currentParent == root && root.Children.Count > 0)
                {
                    break;
                }
            }
            else
            {
                int nextTagIndex = html.IndexOf('<', index);
                if (nextTagIndex == -1) nextTagIndex = html.Length;

                string text = html.Substring(index, nextTagIndex - index);
                if (!string.IsNullOrEmpty(text))
                    currentParent.InnerText += text;

                index = nextTagIndex;
            }
        }

        return root.Children.Count > 0 ? root.Children[0] : null;
    }

    private static Dictionary<string, string> ParseAttributes(string attributesString)
    {
        Dictionary<string, string> attributes = new Dictionary<string, string>();
        int index = 0;
        while (index < attributesString.Length)
        {
            while (index < attributesString.Length && char.IsWhiteSpace(attributesString[index]))
                index++;
            if (index >= attributesString.Length)
                break;

            int equalsIndex = attributesString.IndexOf('=', index);
            if (equalsIndex == -1)
            {
                string attrNames = attributesString.Substring(index).Trim();
                if (!string.IsNullOrEmpty(attrNames))
                    attributes[attrNames] = "true";
                break;
            }

            string attrName = attributesString.Substring(index, equalsIndex - index).Trim();
            index = equalsIndex + 1;

            while (index < attributesString.Length && char.IsWhiteSpace(attributesString[index]))
                index++;
            if (index >= attributesString.Length)
            {
                attributes[attrName] = "true";
                break;
            }

            char quote = attributesString[index];
            string attrValue;
            if (quote == '"' || quote == '\'')
            {
                index++;
                int endQuoteIndex = attributesString.IndexOf(quote, index);
                if (endQuoteIndex == -1)
                {
                    attrValue = attributesString.Substring(index);
                    index = attributesString.Length;
                }
                else
                {
                    attrValue = attributesString.Substring(index, endQuoteIndex - index);
                    index = endQuoteIndex + 1;
                }
            }
            else
            {
                int nextSpace = attributesString.IndexOf(' ', index);
                if (nextSpace == -1)
                {
                    attrValue = attributesString.Substring(index);
                    index = attributesString.Length;
                }
                else
                {
                    attrValue = attributesString.Substring(index, nextSpace - index);
                    index = nextSpace;
                }
            }

            attributes[attrName] = attrValue;
        }

        return attributes;
    }
    public static string Serialize(HtmlTreeNode node)
    {
        if (node == null) return string.Empty;
        StringBuilder sb = new StringBuilder();
        SerializeNode(node, sb);
        return sb.ToString();
    }

    private static void SerializeNode(HtmlTreeNode node, StringBuilder sb)
    {
        if (string.IsNullOrEmpty(node.TagName))
        {
            // Handle text nodes
            sb.Append(node.InnerText);
            return;
        }

        // Build opening tag
        sb.Append('<').Append(node.TagName.ToLowerInvariant());

        // Add attributes
        var attributes = new Dictionary<string, string>();
        
        // Merge class attributes
        if (node.Classes.Count > 0)
        {
            attributes["class"] = string.Join(" ", node.Classes);
        }
        
        foreach (var attr in node.Attributes)
        {
            if (attr.Key.Equals("class", StringComparison.OrdinalIgnoreCase)) continue;
            attributes[attr.Key] = attr.Value;
        }

        foreach (var attr in attributes)
        {
            sb.Append(' ').Append(attr.Key.ToLowerInvariant());
            if (attr.Value != "true")
            {
                sb.Append("=\"")
                  .Append(attr.Value.Replace("\"", "&quot;"))
                  .Append('"');
            }
        }

        bool isVoid = voidTags.Contains(node.TagName);
        bool hasChildren = node.Children.Count > 0;
        bool hasContent = !string.IsNullOrWhiteSpace(node.InnerText);

        // Handle void elements
        if (isVoid)
        {
            sb.Append(" />");
            return;
        }

        // Close opening tag
        sb.Append('>');

        // Add inner text and children
        if (hasContent) sb.Append(node.InnerText);
        foreach (var child in node.Children)
        {
            SerializeNode(child, sb);
        }

        // Add closing tag
        sb.Append("</").Append(node.TagName.ToLowerInvariant()).Append('>');
    }
}
public class Program
{
  public static void Main()
  {
      string html = File.ReadAllText("index.html"); 

      var htmlTree = HtmlParser.Parse(html);
      
      var HtmlParser.Serialize(htmlTree);
    }
}
