
## This is how you create Custom Element using RenderFragment Similar gor creation of google ads scripts

### original Script generated using GISCUS

> Be sure To Change Parameter value Based on you Generation from here https://giscus.app/
```javascript
<script src="https://giscus.app/client.js"
        data-repo="PrashantUnity/GettingStarted"
        data-repo-id="R_kgDOKzC5Hw"
        data-category="General"
        data-category-id="DIC_kwDOKzC5H84CdPDh"
        data-mapping="specific"
        data-term=" GettingStarted Discussions"
        data-strict="0"
        data-reactions-enabled="1"
        data-emit-metadata="0"
        data-input-position="top"
        data-theme="light"
        data-lang="en"
        data-loading="lazy"
        crossorigin="anonymous"
        async>
</script>
```
```razor
@using Microsoft.AspNetCore.Components

@if (Script != null)
{
    <div>
        @Script
    </div>
}

@code {

    #region Parameter

    [Parameter]
    public string InputPosition { get; set; } = "top";
    [Parameter]
    public string Term { get; set; } = "GettingStarted Discussions";
    [Parameter]
    public string Repo { get; set; } = "PrashantUnity/GettingStarted";
    [Parameter]
    public string RepoId { get; set; } = "R_kgDOKzC5Hw"; 
    #endregion

    
    public string Category { get; set; } = "General"; 
    public string CategoryId { get; set; } = "DIC_kwDOKzC5H84CdPDh"; 
    public string Mapping { get; set; } = "specific"; 
    public bool ReactionsEnabled { get; set; } = true; 
    public string Theme { get; set; } = "light"; 
    public string Language { get; set; } = "en"; 
    public string Loading { get; set; } = "lazy"; 
    public string EmitMetadata { get; set; } = "0"; 
    public string Strict { get; set; } = "0";

    private RenderFragment Script { get; set; }

    protected override void OnParametersSet()
    {
        Script = new RenderFragment(b =>
        {
            b.OpenElement(0, "script");
            b.AddMultipleAttributes(1, new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("src", "https://giscus.app/client.js"),
                    new KeyValuePair<string, object>("data-repo", Repo),
                    new KeyValuePair<string, object>("data-repo-id", RepoId),
                    new KeyValuePair<string, object>("data-category", Category),
                    new KeyValuePair<string, object>("data-category-id", CategoryId),
                    new KeyValuePair<string, object>("data-mapping", Mapping),
                    new KeyValuePair<string, object>("data-term", Term),
                    new KeyValuePair<string, object>("data-strict", Strict),
                    new KeyValuePair<string, object>("data-reactions-enabled", ReactionsEnabled ? "1" : "0"),
                    new KeyValuePair<string, object>("data-emit-metadata", EmitMetadata),
                    new KeyValuePair<string, object>("data-input-position", InputPosition),
                    new KeyValuePair<string, object>("data-theme", Theme),
                    new KeyValuePair<string, object>("data-lang", Language),
                    new KeyValuePair<string, object>("data-loading", Loading),
                    new KeyValuePair<string, object>("crossorigin", "anonymous"),
                    new KeyValuePair<string, object>("async", true),
                });
            b.CloseElement();
        });
    }
}
```

## Uses
```razor
<GiscusIntegration />
```
