@echo off
echo ---------------------------------------------------------------------------------
echo variables name list/ You can name it as per your requirement
set ParentDir="PCF"
set AppName="AppName"
set nameSpaceName="SampleNameSpace"
set initName="SampleComponent"
set pubName="Samples"
set pubPrefix="samples"
set SolutionsPath="Solutions"
echo ----------------------------------------------- 
echo Project is in this Format
echo Parent Directory : %ParentDir%
echo Application Name : %AppName%
echo App Namespace    : %nameSpaceName%
echo Name             : %initName%
echo Publisher Name   : %pubName%
echo Publisher Prefix : %pubPrefix%
echo -----------------------------------------------
mkdir %ParentDir%
cd %ParentDir%
mkdir %AppName%
cd %AppName%
set buildPath=%cd%
echo ---------------------------------------------------------------------------------

call pac install latest 
echo ---------------------------------------------------------------------------------

echo "Project Creation Started"
call pac pcf init --namespace %nameSpaceName% --name %initName% --template field
 
echo ---------------------------------------------------------------------------------
echo "Running npm install Command" 
call npm install 
echo ---------------------------------------------------------------------------------

Echo "Running npm install react react-dom Command"
call npm install react react-dom  
 
echo ---------------------------------------------------------------------------------
Echo "Running "npm install @types/react --save-dev" Command"
call npm install @types/react --save-dev 
 
echo ---------------------------------------------------------------------------------
Echo "Opening Vs Code"
call code .
Echo "Running run refresh command"
call npm run refreshTypes
 
echo ---------------------------------------------------------------------------------
Echo "Running run build command"
call npm run build
start cmd.exe /k "npm run start"
echo ---------------------------------------------------------------------------------
mkdir %SolutionsPath%
cd %SolutionsPath%

Echo "Adding Solution File to solution  Folder"

call pac solution init --publisher-name %pubName%  --publisher-prefix %pubPrefix%

echo ---------------------------------------------------------------------------------
set path=%cd%
echo "Adding Solution reference to %path%"
 
echo ---------------------------------------------------------------------------------

::call pac solution add-reference --path %path% 
::call pac solution add-reference --path .. 
start cmd.exe /k "call pac solution add-reference --path %path%\.."
echo "add reference part done"
echo ---------------------------------------------------------------------------------
echo +++++++++++++++++++++++++++++++++++++
echo +++++++++++++++++++++++++++++++++++++
echo +++++++++++++++++++++++++++++++++++++
echo +++++++++++++++++++++++++++++++++++++
echo ++++"From Now on Bhagwan Bharose"++++
echo +++++++++++++++++++++++++++++++++++++
echo +++++++++++++++++++++++++++++++++++++
echo +++++++++++++++++++++++++++++++++++++
echo +++++++++++++++++++++++++++++++++++++
Echo "Project Building With MsBuild"  
::call msbuild /t:build /restore 
msbuild /t:build /restore 
 
echo ---------------------------------------------------------------------------------
Echo "Project Building With dotnet Build"
::call dotnet build  
dotnet build  
 
echo ---------------------------------------------------------------------------------
echo "Running msbuild command again For Release mode"
call msbuild/property:configuration=Release
echo ---------------------------------------------------------------------------------
echo Some of the Command may have not worked


echo ----------------------------------------------- 
echo Project is in this Format
echo Parent Directory : %ParentDir%
echo Application Name : %AppName%
echo App Namespace    : %nameSpaceName%
echo Name             : %initName%
echo Publisher Name   : %pubName%
echo Publisher Prefix : %pubPrefix%
echo ----------------------------------------------- 
echo try to execute it mannually in vs code 
echo cd %SolutionsPath%
echo pac solution add-reference --path ..
echo dotnet build
echo msbuild

pause
