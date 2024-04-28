# Xmal File
```xaml
<Window x:Class="FindDifference.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid >
        <Grid.ColumnDefinitions>
            <ColumnDefinition></ColumnDefinition>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>
        <Button Grid.Row="0" Grid.Column="0" Height="50" VerticalAlignment="Top" Click="Button_Click" Content="Calculate ? Find the Difference" FontSize="22" FontWeight="Bold" Background="#FF8BD99B" />
        <Grid  Grid.Column="0" Margin="0,55,0,0" Grid.RowSpan="2">
            <Grid.ColumnDefinitions>
                <ColumnDefinition></ColumnDefinition>
                <ColumnDefinition></ColumnDefinition>
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition></RowDefinition>
                <RowDefinition></RowDefinition>
            </Grid.RowDefinitions>

            <Grid  Grid.Row="0" Grid.Column="0" >
                <Grid.ColumnDefinitions>
                    <ColumnDefinition></ColumnDefinition>
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height="38" ></RowDefinition>
                    <RowDefinition></RowDefinition>
                </Grid.RowDefinitions>
                <Label Content="Data One" Height="38" Grid.Row="0" Grid.Column="0" Background="#FFFAE4E4" FontWeight="Bold" FontSize="22" />
                <RichTextBox  Grid.Row="1" Grid.Column="0" Background="{x:Null}" BorderThickness="10,10,10,10" AutomationProperties.HelpText="Paste Data Here for Column One"  x:Name="OneText" LostFocus="OneText_LostFocus"> 
                </RichTextBox>
            </Grid>
            <Grid  Grid.Row="1" Grid.Column="0">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition></ColumnDefinition>
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height="38" ></RowDefinition>
                    <RowDefinition></RowDefinition>
                </Grid.RowDefinitions>
                <Label Content="Data Missing From One" Height="38" Grid.Row="0" Grid.Column="0" Background="#FFFAE4E4" FontWeight="Bold" FontSize="22" />
                <RichTextBox  Grid.Row="1" Grid.Column="0" Background="{x:Null}" BorderThickness="10,10,10,10" x:Name="OneAnswer"> 
                </RichTextBox>
            </Grid>
            <Grid   Grid.Row="0" Grid.Column="1">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition></ColumnDefinition>
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height="38" ></RowDefinition>
                    <RowDefinition></RowDefinition>
                </Grid.RowDefinitions>
                <Label Content="Data Two" Height="38" Grid.Row="0" Grid.Column="0" Background="#FFC5CBC2" FontWeight="Bold" FontSize="22" />
                <RichTextBox  Grid.Row="1" Grid.Column="0" Background="{x:Null}" BorderThickness="10,10,10,10" AutomationProperties.HelpText="Paste Data Here for Column Two" x:Name="TwoText" LostFocus="TwoText_LostFocus" >
                    
                </RichTextBox>
            </Grid>
            <Grid  Grid.Row="1" Grid.Column="1" >
                <Grid.ColumnDefinitions>
                    <ColumnDefinition></ColumnDefinition>
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height="38" ></RowDefinition>
                    <RowDefinition></RowDefinition>
                </Grid.RowDefinitions>
                <Label Content="Data Missing From Two" Height="38" Grid.Row="0" Grid.Column="0" Background="#FFC5CBC2" FontWeight="Bold" FontSize="22" />
                <RichTextBox  Grid.Row="1" Grid.Column="0" Background="{x:Null}" BorderThickness="10,10,10,10" x:Name="TwoAnswer">
                     
                </RichTextBox>
            </Grid>
        </Grid>
    </Grid>

</Window>
```
# Backend Code
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FindDifference
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string oneText = string.Empty;
        string twoText = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            oneText = ReadText(OneText);
            twoText = ReadText(TwoText);
            Console.WriteLine(oneText);
            Console.WriteLine(twoText);
            var (One, Two) = CalculatingDifference(oneText, twoText);
            AddText(OneAnswer, One);
            AddText(TwoAnswer, Two);
        }


        private void TwoText_LostFocus(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("Two Focused Changed");
        }

        private void OneText_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Two Focused Changed");
        }
        string ReadText(RichTextBox richTextBox)
        {
            TextRange textRange = new TextRange(
                richTextBox.Document.ContentStart,
                richTextBox.Document.ContentEnd
            );
            return textRange.Text;
        }
        (HashSet<string> One, HashSet<string> Two) CalculatingDifference(string strOne, string strTwo)
        {

            var hashOneSet = new HashSet<string>(strOne.Split('\n').Where(x => x.Length > 0).Select(x => x.Trim().ToString()));
            var hashTwoSet = new HashSet<string>(strTwo.Split('\n').Where(x => x.Length > 0).Select(x => x.Trim().ToString()));

            var hashOne = new HashSet<string>();
            var hashTwo = new HashSet<string>();
            foreach (var i in hashTwoSet)
            {
                if (!hashOneSet.Contains(i))
                {
                    hashOne.Add(i);
                }
            }
            foreach (var i in hashOneSet)
            {
                if (!hashTwoSet.Contains(i))
                {
                    hashTwo.Add(i);
                }
            }
            return (hashOne, hashTwo);
        }


        void AddText(RichTextBox answerRichTextBox, HashSet<string> textList)
        {
            answerRichTextBox.Document.Blocks.Clear();
            foreach (var item in textList)
            {
                answerRichTextBox.Document.Blocks.Add(new Paragraph(new Run(item)));
            }
        }

    }
}

```
