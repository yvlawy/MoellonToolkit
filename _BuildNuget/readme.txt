MoellonToolkit nuget publication readme.txt:

================================================================================
Publications:
last is: 0.4.0.4, 18fev18.

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

-Don't forget to set new version to the three libraries!
   use 3 digits!
   
-Build them.
  

-Update the nuget text file descriptor:
(in Visual Studio)
_BuildNuget\Model\
	MoellonToolkit.nuspec

-Create a target folder:
Built\MoellonToolkit.x.x.x.x\

-Copy the model folders.

-Copy the MoellonToolkit.nuspec
  (from the model folder)
	into the path: on the root (into Built\MoellonToolkit.x.x.x.x\)
  
-Copy the 3 dll libraries:
	(from the folder: Dev\DevApp\bin\debug)
	MoellonToolkit.CommonDlgs.Defs.dll
	MoellonToolkit.CommonDlgs.Impl.dll
	MoellonToolkit.MVVMBase.dll

	into the path:
		Model\lib\net40\
		
-Check the version of these 3 libraries


-Generate the package:
(inside Visual Studio, in the package manager console)
	>cd _BuildNuget\Built
	> cd .\MoellonToolkit.x.x.x.x
	>nuget pack

the result:
	inside: Built\MoellonToolkit.x.x.x.x\
		MoellonToolkit.x.x.x.x.nupkg

then, publish the generated file on the nuget website.
Sign in, 
Upload the packet:
	Built\MoellonToolkit.x.x.x.x\MoellonToolkit.x.X.x.x.nupkg
