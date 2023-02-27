@echo off
::inside () fill all the app name you want to terminate
::tasklist 
FOR %%G IN (
	chrome.exe,
	firefox.exe,
	mspaint.exe
	
	) DO (
 taskkill /IM %%G /F ) 
::TASKKILL /?
pause
