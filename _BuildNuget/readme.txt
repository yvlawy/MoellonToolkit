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

Don't forget to set new version to the three libraries!
Build them.

Create a target folder:
Built\MoellonToolkit.x.x.x.x\

Copy the model folders.

Copy the 3 dll libraries:
	(from the folder: Dev\DevApp\bin\debug)
	MoellonToolkit.CommonDlgs.Defs.dll
	MoellonToolkit.CommonDlgs.Impl.dll
	MoellonToolkit.MVVMBase.dll

	into the path:
		Model\lib\net40\


Update the nuget text file descriptor:
	MoellonToolkit.nuspec

Generate the package:
(inside Visual Studio, in the nuget console)
	todo: copy lib!! (and more)
	>cd _BuildNuget\Built\MoellonToolkit.x.x.x.x
	>nuget pack

the result:
	inside: Built\MoellonToolkit.x.x.x.x\
		MoellonToolkit.x.X.x.x.nupkg

then, publish the generated file on the nuget website.
Sign in, 
Upload the packet:
	Built\MoellonToolkit.x.x.x.x\MoellonToolkit.x.X.x.x.nupkg
