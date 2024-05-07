# Blazor

## html
```razor
@if(ls is not null)
{
  <MudContainer Class="center-container">
      <MudPaper>
        @foreach(var item in ls)
        {
            <MudPaper Class="d-flex flex-row flex-grow-1 gap-1 pa-1" Elevation="0">
                @foreach(var point in item)
                {
                    <MudPaper Class="mud-theme-success" Width="64px" Height="64px"/> 
                } 
            </MudPaper>
        } 
      </MudPaper>
  </MudContainer>
}
 
<style>
   .center-container {
       display: flex;
       justify-content: center;
       align-items: center;
       
   }  
</style>
```
## Csharp
```cs
  [Inject] public IJSRuntime JSRuntime { get; set; }
  private int gridSize = 64;
  private WindowSize windowSize;
  List<List<string>> ls = new();
  int? column =0;
  int? row =0;
  
  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
        await GetInnerDimensions();
    }
  }
  
  private async Task GetInnerDimensions()
  {
    windowSize = await JSRuntime.InvokeAsync<WindowSize>("getInnerDimensions");
    column = windowSize?.Width/gridSize;
    row=windowSize?.Height/gridSize;
  
    for(var i=0; i<row-1;i++)
    {
        var temp = new List<string>();
        for(var j=0; j<column-2;j++)
        {
            temp.Add($"{i} : {j}");
        }
        ls.Add(temp);
    }
    StateHasChanged();
  }
  public class WindowSize
  {
    public int Width { get; set; }
    public int Height { get; set; }
  }
```
## javascript
```js
window.getInnerDimensions = () => {
    return {
        Width: window.innerWidth,
        Height: window.innerHeight
    };
};
```
