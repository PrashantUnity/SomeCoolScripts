Steps to make Blazor Webassembly hoist on git hub


Step 1 : Create Public Repository.
step 2 : Go to github setting => Action => General => workflow section
			select these two
				Read and write permissions
				Allow GitHub Actions to create and approve pull requests

Step 2 : Create github action using asp.net core sdk from market place
	
```
	
name: DeployBlazorWebAssembly
on:
  push:
    branches: [ "master" ] 

jobs:
  build:

    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 6.0.x
        
    - name: Publish .NET Core Project
      run: dotnet publish BlazorGitHubPagesDemo.csproj -c Release -o release --nologo
    
 
    - name: Change base-tag in index.html from / to BlazorGitHubPagesDemo
      run: sed -i 's/<base href="\/" \/>/<base href="\/BlazorGitHubPagesDemo\/" \/>/g' release/wwwroot/index.html
    
 
    - name: copy index.html to 404.html
      run: cp release/wwwroot/index.html release/wwwroot/404.html

    # (Allow files and folders starting with an underscore)
    - name: Add .nojekyll file
      run: touch release/wwwroot/.nojekyll
      
    - name: Commit wwwroot to GitHub Pages
      uses: JamesIves/github-pages-deploy-action@3.7.1
      with:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        BRANCH: gh-pages
        FOLDER: release/wwwroot	
		
```
Step 3 :
	Go to github setting => Pages => Build and deployment section
		
			set Source to deploy from a branch
			in branch dropdown select gh-pages ( or whatever you have decided in Commit wwwroot to GitHub Pages section)
