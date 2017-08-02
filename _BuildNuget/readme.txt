================================================================================
Folders and files descriptions:

BuildNuget\
To build the nuget package of the application MoellonToolkit.

Model\
The model of files and folders.

Built\
temporary content. 
where are built the current package.


================================================================================
Process:

Create a target folder:
Built\MoellonToolkit.x.x.x.x\


Copy the 3 dll libraries:
	MoellonToolkit.CommonDlgs.Defs.dll
	MoellonToolkit.CommonDlgs.Impl.dll
	MoellonToolkit.MVVMBase.dll

	into the path:
		(from the folder: Dev\DevApp\bin\debug)
		Model\lib\net40\


Update the nuget text file descriptor:
	MoellonToolkit.nuspec

Generate the package:
(inside Visual Studio, in the nuget console)
	todo: copy lib!! (and more)
	>cd _BuildNuget\Built\MoellonToolkit.0.3.0
	>nuget pack

the result:
	inside: Built\MoellonToolkit.x.x.x.x\
		MoellonToolkit.x.X.x.x.nupkg

then, publish the generated file on the nuget website.
