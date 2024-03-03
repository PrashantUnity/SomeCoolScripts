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

## String Interpolation inside Razer
```razor
<MudPaper Class="@($"mud-theme-{point.NodeColor}")" Width="64px" Height="64px" />
```
## Mud Blazor Upload Image And Preview
```razor
@using System.IO
<MudFileUpload T="IBrowserFile" FilesChanged="UploadFiles">
    <ButtonTemplate>
        <MudFab HtmlTag="label"
                Color="Color.Secondary"
                Icon="@Icons.Material.Filled.Image"
                Label="Load picture"
                for="@context.Id" />
    </ButtonTemplate>
</MudFileUpload>

@if(ImageUri!="")
{
    <MudImage Src="@ImageUri" Width="400" Height="400" /> 
}

@code {

    string ImageUri="";
    private async Task UploadFiles(IBrowserFile file)
    { 
        var image = await file.RequestImageFileAsync("image/png", 600, 600);

        using Stream imageStream = image.OpenReadStream(1024 * 1024 * 10);
        
        using MemoryStream ms = new();
        //copy imageStream to Memory stream
        await imageStream.CopyToAsync(ms);

        //convert stream to base64
        ImageUri = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
        StateHasChanged();
        //TODO upload the files to the server
    }
}

```

## Painc Version Of Jaavascript
```
@inject IJSRuntime JSRuntime;

<MudButton OnClick="Panic">Panic</MudButton>
<MudButton OnClick="Cool">Cool Version</MudButton>

@code {
    string url ="www.google.com";
    async Task Panic()
    {
        await JSRuntime.InvokeAsync<object>("open", url, "_blank");
    }
    async Task Cool()
    {
        await JSRuntime.InvokeVoidAsync("open", url, "_blank");
    }
}
```
