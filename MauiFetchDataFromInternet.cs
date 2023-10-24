private async void OnCounterClicked(object sender, EventArgs e)
{ 
    CounterBtn.Text = $"Clicked {++count} times"; 
    var response = await FromJsonAsyncEta();
    MonkeyImage.Source = response[0].Images; 
}
public async Task<List<Monkey>> FromJsonAsync()
{
    List<Monkey> monkeyList = new List<Monkey>();
    var response = await client.GetAsync("https://www.montemagno.com/monkeys.json");
    if (response.IsSuccessStatusCode)
    {
        var temp = await response.Content.ReadAsStringAsync();
        monkeyList = JsonConvert.DeserializeObject<List<Monkey>>(temp);
    }
    return monkeyList;
}

public class Monkey
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Details { get; set; }
    public string Image { get; set; }
    public int Population { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}
