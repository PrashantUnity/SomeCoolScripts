@echo off
mkdir PCF
cd PCF
mkdir MyApp
cd MyApp

call pac install latest

echo "Project Creation Started"
call pac pcf init --namespace SampleNameSpace --name SampleComponent --template field

echo "Running npm install Command" 
call npm install 

Echo "Running npm install react react-dom Command"
call npm install react react-dom  

Echo "Running "npm install @types/react --save-dev" Command"
call npm install @types/react --save-dev 

Echo "Opening Vs Code"
call code .

mkdir solution
cd solution

Echo "Adding Solution File to solution  Folder"
set pubName="PubName"
set pubPrefix="pubpre"
call pac solution init --publisher-name %pubName%  --publisher-prefix %pubPrefix%

set path=%cd%
echo "Adding Solution reference to %path%"
echo " from Now on Bhagwan Bharose" 
call pac solution add-reference --path %path% 

cd..
Echo "Project Building With MsBuild"
call msbuild  

Echo "Project Building With dotnet Build"
call dotnet build  

echo "Batch Script Finished"
pause 
