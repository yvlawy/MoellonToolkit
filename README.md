# What is MoellonToolkit?
![The MoellonToolkit framework](Docs/Logo/MoellonToolkit_logo128.jpg) MoellonToolkit is a small and basic framework designed for building WPF applications with the focus on the MVVM pattern.
The framework provides a generic back-end application controller with some convenient ready to use dialog boxes like: Error, Warning, Question, Information, InputText, Select Text,...
These functionalities will enable developers to build solutions very easily. 
The framework is developed in C# 4.0.  

# List of implemented dialog boxes

The classic error message box:

	commonDlg.ShowError("You're wrong.");

![The Error dialog box](Docs/Dialogs/dlgError.jpg)

The Information message box:

![The Information dialog box](Docs/Dialogs/dlgInformation.jpg)

	commonDlg.ShowInformation("Yes, you like dogs.");

The Warning message box:

![The Warning dialog box](Docs/Dialogs/dlgWarning.jpg)

	commonDlg.ShowWarning("Be carefull!");

The Question message box:

![The Question dialog box](Docs/Dialogs/dlgQuestion.jpg)

	commonDlg.ShowQuestion("Do you like dogs?");

The Extra Large Width Information message box:

![The Extra Large Width Information dialog box](Docs/Dialogs/dlgInformationWidthXL.jpg)

	string msg = "Yes, you like dogs, cats, birds, horses, worms, lions, ants, girafs, rabbits, ...";
	commonDlg.ShowInformation(WHSize.WXL, msg);

The Input text message box:

![The Input text dialog box](Docs/Dialogs/dlgInputText.jpg)

	string text;
    CommonDlgResult res = commonDlg.ShowDlgInputText("Input", "Give a name:", "name", out text);
    if (res != CommonDlgResult.Ok)
    {
		commonDlg.ShowWarning("The user cancel the operation!");
        return;
    }

The large width Input text message box:

![The large width Input text dialog box](Docs/Dialogs/dlgInputTextWidthLarge.jpg)

	string text;
    CommonDlgResult res = commonDlg.ShowDlgInputText(WHSize.WL, "Input", "Give a name:", "name", out text);
    if (res != CommonDlgResult.Ok)
	{ ... }


The combo choice dialog box:

![The combo choice dialog box](Docs/Dialogs/dlgComboChoice.jpg)

The list choice dialog box:

![The list choice dialog box](Docs/Dialogs/dlgListChoice.jpg)


The select file  dialog box:
(Use the Windows built-in dialog box.)

![The select file  dialog box](Docs/Dialogs/dlgSelectFile.jpg)

	res = commonDlg.ShowDlgSelectFile("C\\", "*.*", "All | *.*", out pathName, out fileName);
    if (res == CommonDlgResult.Ok)
	{ ... }

The save file  dialog box:

(Use the Windows built-in dialog box).

![The save file  dialog box](Docs/Dialogs/dlgSaveFile.jpg)

	res = commonDlg.ShowDlgSaveFile("C\\", "", "All | *.*", out pathName, out fileName);
    if (res == CommonDlgResult.Ok)
	{ ...}

The images of these dialog boxes are the folder: Docs\Dialogs.


	
# Quick Getting Started 
Create a WPF project. Create your back-end application controller class based on the MoellonToolkit base controller.
Create your Views and ViewModels, update the controller.

For more details, see the application sample named DevApp provided in the solution in the Dev folder.

## Use defined dialog boxes
You can just use the common dialog boxes provided by the framework, without the back-office application controller.

Use the ICommonDlg interface and the concrete implementation CommonDlg.

    ICommonDlg commonDlg = new CommonDlg();

Sample:	Display a dialog box asking the user to confirm the application exit, has 2 buttons; ok and cancel.

    if (commonDlg.ShowDlg(WHSize.WL_HL, "Confirmation", "Do you really want to exit the application?", CommonDlgIcon.Question, CommonDlgButtons.OkCancel) != CommonDlgResult.Ok)
		return false;

The first parameter set the Width and the Height size of the dialog box: 
WL is for: Width large, HL is for: Height Large.

# Others functionnalities
You can choose your text translation for titles, labels and buttons used in dialog boxes.
Today available languages are: gb, fr, es.
But you can set your own text for each text code: Ok, Cancel, Yes, No,...

Set the translation page for text:

    commonDlg.SetCurrentCultureInfo(CultureCode.en_GB);


# Package is available on Nuget
https://www.nuget.org/packages/MoellonToolkit
