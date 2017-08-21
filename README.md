# What is MoellonToolkit?
![The MoellonToolkit framework](Docs/Logo/MoellonToolkit_logo128.jpg) MoellonToolkit is a small and basic framework designed for building WPF applications with the focus on the MVVM pattern.
The framework provides a generic back-end application controller with some convenient ready to use dialog boxes like: Error, Warning, Question, Information, InputText, Select Text,...
These functionalities will enable developers to build solutions very easily. 
The framework is developed in C# 4.0.  

# List of implemented dialog boxes
See the images of dialog boxes in the folder: Docs\Dialogs.

The classic error message:
![The Error dialog box](Docs/Dialogs/dlgError.jpg)

The Information message:
![The Information dialog box](Docs/Dialogs/dlgInformation.jpg)

The Warning message:
![The Warning dialog box](Docs/Dialogs/dlgWarning.jpg)

![The Question dialog box](Docs/Dialogs/dlgQuestion.jpg)

![The Extra Large Width Information dialog box](Docs/Dialogs/dlgInformationWidthXL.jpg)

![The Input text dialog box](Docs/Dialogs/dlgInputText.jpg)

![The large width Input text dialog box](Docs/Dialogs/dlgInputTextWidthLarge.jpg)

![The combo choice dialog box](Docs/Dialogs/dlgComboChoice.jpg)

![The list choice dialog box](Docs/Dialogs/dlgListChoice.jpg)

![The select file  dialog box](Docs/Dialogs/dlgSelectFile.jpg)

Use the Windows built-in dialog box.

![The save file  dialog box](Docs/Dialogs/dlgSaveFile.jpg)

Use the Windows built-in dialog box.

# Others functionnalities
You can choose your text translation for titles, labels and buttons used in dialog boxes.
Today available languages are: gb, fr, es.
But you can set your own text for each text code: Ok, Cancel, Yes, No,...

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

# Package is available on Nuget
https://www.nuget.org/packages/MoellonToolkit
