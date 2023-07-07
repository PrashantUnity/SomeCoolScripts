# Basics
|Name   |  Description |
|---:|:---|
| Cmdlet  | Commands built into shell written in .NET  |
|  Functions | Commands written in PowerShell language  |
| Parameter  |  Argument to a Cmdlet/Function/Script |
|Alias |Shortcut for a Cmdlet or Function|
|Scripts |Text files with .ps1 extension
|Applications |Existing windows programs
|Pipelines | Pass objects Get-process word \| Stop-Process
|Ctrl+c| Interrupt current command
|Left/right | Navigate editing cursor
|Ctrl+left/right | Navigate a word at a time
|Home / End | Move to start / end of line |
|Up/down | Move up and down through history|
|Insert |Toggles between insert/overwrite mode|
|F7 |Command history in a window|
|Tab / Shift-Tab | Command line completion|

# Help
|Name   |  Description |
|---:|:---|
|Get-Command | Get all commands
|Get-Command -Module RGHS | Get all commands in RGHS module
|Get-Command Get-p* | Get all commands starting with get-p
|Get-help get-process | Get help for command
|Get-Process|  Get-Member Get members of the object
|Get-Process| format-list -properties * Get-Process as list with all properties


# Writing output and reading input
|Name   |  Description |
|---:|:---|
|"This displays a string"| String is written directly to output
|Write-Host "color" -ForegroundColor Red -NoNewLine |String with colors, no new line at end
|$age = Read-host "Please enter your age" |Set $age variable to input from user
|$pwd = Read-host "Please enter your password" -asSecureString |Read in $pwd as secure string
|Clear-Host |Clear console

# Variables
|Name   |  Description |
|---:|:---|
|$var = "string" |Assign variable
|$a,$b = 0 or $a,$b = 'a','b' |Assign multiple variables
|$a,$b = $b,$a | Flip variables
|$var=[int]5 |Strongly typed variable

# Assignment, Logical, Comparison Operators
|Name   |  Description |
|---:|:---|
|=,+=,-=,++,-- |Assign values to variable 
|-and,-or,-not,! |Connect expressions / statements 
|-eq, -ne | Equal, not equal
| -gt, -ge | Greater than, greater than or equal 
|-lt, -le | Less than, less than or equal
|-replace |“Hi” -replace “H”, “P” 
|-match,-notmatch |Regular expression match 
| -like,-notlike | Wildcard matching 
|-contains,-notcontains |Check if value in array 
|-in, -notin |Reverse of contains,notcontains

# Parameters
|Name   |  Description |
|---:|:---|
|-Confirm |Prompt whether to take action
| -WhatIf |Displays what command would do

# Cmdlets
|Name   |  Description |
|---:|:---|
|Get-EventLog| Get-WinEvent Get-Date
|Start-Sleep| Compare-Object
|Start-Job| Get-Credential
|Test-Connection| New-PSSession
|Test-Path| Split-Path
|Get-ADUser| Get-ADComputer
|Get-History| New-ISESnippet
|Get-WMIObject| Get-CimInstance


# Arrays, Objects
|Name   |  Description |
|---:|:---|
|$arr = "a", "b" |Array of strings
|$arr = @()  |Empty array
|$arr[5]  |Sixth array element
|$arr[-3..-1]  |Last three array elements
|$arr[1,4+6..9]  |Elements at index 1,4, 6-9
|$arr[1] += 200  |Add to array item value
|$z = $arA + $arB  |Two arrays into single array
|[pscustomobject]@{x=1;z=2}  | Create custom object
(Get-Date).Date | Date property of object


# Importing, Exporting, Converting
|Name   |  Description |
|---:|:---|
|Export-CliXML | Import-CliXML
|ConvertTo-XML | ConvertTo-HTML
|Export-CSV | Import-CSV
|ConvertTo-CSV | ConvertFrom-CSV


# Flow Control
|Name   |
|---|
|If(){} Elseif(){ } Else{ }
|while(){}
|For($i=0; $i -lt 10; $i++){}
|Foreach($file in dir C:\){$file.name}
|1..10 \| foreach{$_}


# Comments, Escape Characters
|Name   |  Description |
|---:|:---|
|#Comment |Comment
|<#comment#> |Multiline Comment
|"`"test`"" | Escape char `
|`t |Tab
|`n |New line
|` Line |continue

# Aliases for common commands
|Name   |  Description |
|---:|:---|
|Gcm | Get-Command
|Foreach,% | Foreach-Object
|Sort | Sort-Object
|Where,? | Where-Object
|Diff,compare | Compare-Object
|Dir, ls, gci | Get-ChildItem
|Gi |Get-Item
|Copy,cp,cpi | Copy-Item
|Move,mv,mi | Move-Item
|Del,rm | Remove-Item
|Rni,ren | Rename-Item
|Ft | Format-Table
|Fl |Format-List
|Gcim | Get-CimInstance
|Cat,gc,type | Get-Content
|Sc |Set-Content
|h,history,ghy | Get-History
|Ihy,r | Invoke-History
|Gp | Get-ItemProperty
|Sp | Set-ItemProperty
|Pwd,gl | Get-Location
|Gm |Get-Member
|Sls | Select-String
|Cd,chdir,sl | Set-Location
|Cls,clear | Clear-Host

# Cmdlets
|Name   |
|---|
|Set-Location
|Get-Content
|Add-Content
|Set-Content
|Out-File
|Out-String
|Copy-Item
|Remove-Item
|Move-Item
|Set-Item
|New-Item
