# Instruction to Setup Of My Posh for windows

### Install OhMyPosh
    winget install JanDeDobbeleer.OhMyPosh -s winget

### Get All themes to your local Machine
    Get-PoshThemes

### Default Themes assuming that you have VS Code installed 
    code $PROFILE

### Now Paste the below snippet inside .ps1 File which opened from above 
    oh-my-posh.exe init pwsh| Invoke-Expression
---
## For Specific Themes You need to replace "pathoftheme" with absolute theme path
    oh-my-posh.exe init pwsh --config "pathoftheme" | Invoke-Expression
### Generally themes path  is located inside
    User => AppData => Local\Programs\oh-my-posh\themes\atomic.omp.json"
Copy Full path replace "pathoftheme" with your selected Choice of themes

### Your teminal might be appering in blocky this is because Nerd Font is not installed in pc
### go to [Nerd Font Website](https://www.nerdfonts.com/font-downloads) and download any font of you like
---

## For VS Code You Might Have To Setup Font Manually 
    open Setting of Vs Code 
    search Teminal 
    locate 
        Teminal > Integrated : Font Family
    Click On 
        Editor : Font Family
    And append "Font Name" Which you intalled on your pc
    in my case this is what look likes after adding
    Consolas, 'Courier New', monospace,'UbuntuMono Nerd Font'
## for Terminal/Powershell
    Got to setting 
    Default 
    Font Face 
    Select Nerd Front From Drop Down

