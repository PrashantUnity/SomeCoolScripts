# Code Snippet for Mud Blazor


##  Simple implementation of Pagination 

```razor
@if (prime.Count > 0)
{
    <MudPaper Class="d-flex flex-wrap justify-center gap-4" Elevation="0">
        @foreach (var item in prime)
        {
            <div class=" pa-1" style="background-color:transparent;">
                <MudCard>
                    <MudCardContent>
                        <div class="d-flex justify-center flex-grow-1 gap-4 ma-0">
                            <MudText Typo="Typo.h1" Style="font-weight:bolder">@item</MudText>
                            </div>
                        </MudCardContent>
                    </MudCard>
                </div>
        }
    </MudPaper>
}


<PagerContent>
    <MudPagination SelectedChanged="PageChanged" 
    Count="@((ls.Count) / 10)" Class="pa-4"/>
</PagerContent>

@code {
    List<int> prime = new List<int>();
    List<int> ls = Enumerable.Range(1,10000).ToList();

    private void PageChanged(int i)
    {
        prime = ls.Skip((i-1)*10).Take(10).ToList();
    }
}
```

## Matrix Code Snippet
```razor
<MudButton OnClick="ButtonOnClick">Generate</MudButton> 

<MudPaper Class="d-flex flex-column flex-grow-1 gap-1" Elevation="0">
    @foreach(var item in ls)
    {
        <MudPaper Class="d-flex flex-row flex-grow-1 gap-1" Elevation="0">
            @foreach(var point in item)
            {
                <MudPaper Class="mud-theme-success" Width="64px" Height="64px"/> 
            }
            
        </MudPaper>
    } 
</MudPaper>

@code {
    
    int n = 8;
    List<List<string>> ls = new();
    void ButtonOnClick()
    {
        ls = new();

        for(var i=0;i<n;i++)
        {
            var temp = new List<string>();
            for(var j=0; j<n; j++)
            {
                temp.Add($"{i}:{j}");
            }
            ls.Add(temp);
        }
    }
}
```
