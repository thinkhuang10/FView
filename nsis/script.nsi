!define PRODUCT_NAME "FView"
!define PRODUCT_VERSION "1.0.0.1"
!define PRODUCT_PUBLISHER "FLIGHT"
!define PRODUCT_COMPANY_NAME "大连弗莱德电气技术有限公司"
!define PRODUCT_WEB_SITE "https://www.dl-flight.com/"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT_NAME}"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"

!define InstallFilesDir "..\src\bin"

;--------------------------------
;Include
;--------------------------------
!include "WinVer.nsh"
!include "x64.nsh"
!include "MUI2.nsh"

;--------------------------------
;General
;--------------------------------
BrandingText "${PRODUCT_COMPANY_NAME}"

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "FView_${PRODUCT_VERSION}.exe"

InstallDir "$PROGRAMFILES\${PRODUCT_NAME}"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""

RequestExecutionLevel admin

ShowInstDetails show
ShowUninstDetails show

;--------------------------------
; MUI Settings
;--------------------------------
;!define MUI_ICON "#[NSISDIR]\orange-install.ico"
;!define MUI_UNICON "#[NSISDIR]\orange-uninstall-self.ico"
!define MUI_HEADERIMAGE
;!define MUI_HEADERIMAGE_BITMAP "logo.png"

!define MUI_LANGDLL_REGISTRY_ROOT "HKLM"
!define MUI_LANGDLL_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_LANGDLL_REGISTRY_VALUENAME "NSIS:Language"

!define MUI_LICENSEPAGE_CHECKBOX
!define MUI_ABORTWARNING

;--------------------------------
;Pages
;--------------------------------
!insertmacro MUI_PAGE_WELCOME
;!insertmacro MUI_PAGE_LICENSE "#[LICENSE]"
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------
; Language files
;--------------------------------
!insertmacro MUI_LANGUAGE "SimpChinese"

;--------------------------------
;Installer Sections
;--------------------------------

Section /o ".NET Framework 4.8" DotnetFramework
  !define DOTNETFX4_8_INSTALLER_NAME "ndp48-x86-x64-allos-enu.exe"
  
  File /oname=$TEMP\${DOTNETFX4_8_INSTALLER_NAME} "${DOTNETFX4_8_INSTALLER_NAME}"
  
  DetailPrint "Starting Microsoft .NET Framework v4.8 Setup..."
  ExecWait '"$TEMP\${DOTNETFX4_8_INSTALLER_NAME}" /norestart /ChainingPackage FullX64BootStrapper'
  Delete "$TEMP\${DOTNETFX4_8_INSTALLER_NAME}"
  DetailPrint "Microsoft .NET Framework v4.8 is installed."
 
SectionEnd

Section "FTView服务端" FTViewServer
  SetOutPath "$INSTDIR\Config"
  File /r "${InstallFilesDir}\Config\*"
  
  SetOutPath "$INSTDIR\UserControl"
  File /r "${InstallFilesDir}\UserControl\*"
  
  SetOutPath "$INSTDIR\zh-CHS"
  File /r "${InstallFilesDir}\zh-CHS\*"
  
  SetOutPath "$INSTDIR\zh-CN"
  File /r "${InstallFilesDir}\zh-CN\*"

  SetOutPath "$INSTDIR"
  File /r "${InstallFilesDir}\DHMI.exe"
  
  CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
  CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\DHMI.lnk" "$INSTDIR\DHMI.exe"
  CreateShortCut "$DESKTOP\DHMI.lnk" "$INSTDIR\DHMI.exe"
  
  File /r "${InstallFilesDir}\ActiveXFinder.dll"
  File /r "${InstallFilesDir}\Beng_1.dll"
  File /r "${InstallFilesDir}\Button_1.dll"
  File /r "${InstallFilesDir}\CommonSnappableTypes.dll"
  File /r "${InstallFilesDir}\DataGrid.dll"
  File /r "${InstallFilesDir}\DCCE_Button.dll"
  File /r "${InstallFilesDir}\DevExpress.Data.Desktop.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.Data.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.Drawing.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.Office.v23.2.Core.dll"
  File /r "${InstallFilesDir}\DevExpress.Pdf.v23.2.Core.dll"
  File /r "${InstallFilesDir}\DevExpress.Pdf.v23.2.Drawing.dll"
  File /r "${InstallFilesDir}\DevExpress.Printing.v23.2.Core.dll"
  File /r "${InstallFilesDir}\DevExpress.RichEdit.v23.2.Core.dll"
  File /r "${InstallFilesDir}\DevExpress.RichEdit.v23.2.Export.dll"
  File /r "${InstallFilesDir}\DevExpress.Sparkline.v23.2.Core.dll"
  File /r "${InstallFilesDir}\DevExpress.Utils.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.XtraBars.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.XtraEditors.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.XtraGrid.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.XtraLayout.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.XtraNavBar.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.XtraPrinting.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.XtraTreeList.v23.2.dll"
  File /r "${InstallFilesDir}\DevExpress.XtraVerticalGrid.v23.2.dll"
  File /r "${InstallFilesDir}\DHMI.exe.config"
  File /r "${InstallFilesDir}\DJ_1.dll"
  File /r "${InstallFilesDir}\DockConfig.xml"
  File /r "${InstallFilesDir}\DynamicAlarm.dll"
  File /r "${InstallFilesDir}\FM_1.dll"
  File /r "${InstallFilesDir}\Guan.dll"
  File /r "${InstallFilesDir}\HMICompile.dll"
  File /r "${InstallFilesDir}\ICSharpCode.TextEditor.dll"
  File /r "${InstallFilesDir}\Interop.ACTIVEXFINDERLib.dll"
  File /r "${InstallFilesDir}\Interop.MSScriptControl.dll"
  File /r "${InstallFilesDir}\KG_1.dll"
  File /r "${InstallFilesDir}\Light_1.dll"
  File /r "${InstallFilesDir}\Microsoft.Bcl.AsyncInterfaces.dll"
  File /r "${InstallFilesDir}\MySql.Data.dll"
  File /r "${InstallFilesDir}\Pipe.dll"
  File /r "${InstallFilesDir}\SetForms2.dll"
  File /r "${InstallFilesDir}\SetsForms.dll"
  File /r "${InstallFilesDir}\ShapeRuntime.dll"
  File /r "${InstallFilesDir}\System.Buffers.dll"
  File /r "${InstallFilesDir}\System.Memory.dll"
  File /r "${InstallFilesDir}\System.Numerics.Vectors.dll"
  File /r "${InstallFilesDir}\System.Runtime.CompilerServices.Unsafe.dll"
  File /r "${InstallFilesDir}\System.Text.Encodings.Web.dll"
  File /r "${InstallFilesDir}\Newtonsoft.Json.dll"
  File /r "${InstallFilesDir}\System.Threading.Tasks.Extensions.dll"
  File /r "${InstallFilesDir}\System.ValueTuple.dll"
  File /r "${InstallFilesDir}\Timer_1.dll"
  File /r "${InstallFilesDir}\YiBiao.dll"
  File /r "${InstallFilesDir}\YouBiao.dll"
  File /r "${InstallFilesDir}\LogHelper.dll"
  File /r "${InstallFilesDir}\Serilog.dll"
  File /r "${InstallFilesDir}\Serilog.Sinks.File.dll"
  File /r "${InstallFilesDir}\System.Diagnostics.DiagnosticSource.dll"
  File /r "${InstallFilesDir}\OxyPlot.dll"
  File /r "${InstallFilesDir}\OxyPlot.WindowsForms.dll"
  File /r "${InstallFilesDir}\XYControl.dll"
  
SectionEnd

Section "FTView客户端" FTViewClient
  SetOutPath "$INSTDIR\HMIRun"
  File "${InstallFilesDir}\HMIRUN\DHMIRUN.exe"
  
  CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
  CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\DHMIRUN.lnk" "$INSTDIR\DHMIRUN.exe"
  
  File /r "${InstallFilesDir}\HMIRUN\CommonSnappableTypes.dll"
  File /r "${InstallFilesDir}\HMIRUN\DHMIRUN.exe.config"
  File /r "${InstallFilesDir}\HMIRUN\HMIWeb.dll"
  File /r "${InstallFilesDir}\HMIRUN\Interop.MSScriptControl.dll"
  File /r "${InstallFilesDir}\HMIRUN\MySql.Data.dll"
  File /r "${InstallFilesDir}\HMIRUN\ShapeRuntime.dll"
  File /r "${InstallFilesDir}\LogHelper.dll"
  File /r "${InstallFilesDir}\Serilog.dll"
  File /r "${InstallFilesDir}\Serilog.Sinks.File.dll"
  File /r "${InstallFilesDir}\System.Diagnostics.DiagnosticSource.dll"
  File /r "${InstallFilesDir}\System.Buffers.dll"
  File /r "${InstallFilesDir}\System.Memory.dll"
  File /r "${InstallFilesDir}\System.Numerics.Vectors.dll"
  File /r "${InstallFilesDir}\System.Runtime.CompilerServices.Unsafe.dll"

SectionEnd

Section -AdditionalIcons
  SetOutPath "$INSTDIR"
  WriteINIStr "$INSTDIR\${PRODUCT_PUBLISHER}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"

  CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
  CreateShortCut  "$SMPROGRAMS\${PRODUCT_NAME}\${PRODUCT_COMPANY_NAME}.lnk" "$INSTDIR\${PRODUCT_PUBLISHER}.url"
  CreateShortCut  "$SMPROGRAMS\${PRODUCT_NAME}\卸载 ${PRODUCT_NAME}.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR" ;
  WriteRegStr HKLM "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr HKLM "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr HKLM "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr HKLM "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

;--------------------------------
;Uninstaller Section
;--------------------------------
Section "Uninstall"
#[UNREGDLL]

  RMDir /r "$INSTDIR"
  RMDir /r "$SMPROGRAMS\${PRODUCT_NAME}"

#[DELSTARTUP]

  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"

  DeleteRegKey HKLM "${PRODUCT_UNINST_KEY}"

  SetAutoClose true
SectionEnd


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Function GetNetFrameworkVersion
  ReadRegDWORD $R0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" "Release"
FunctionEnd

Function CheckPreinstalled

	Call GetNetFrameworkVersion
	${If} $R0 >= 528040 ;判断是否高于.NET Framework 4.8, 默认版本号528040
	    SectionSetFlags ${DotnetFramework} ${SF_RO}	
		SectionSetText ${DotnetFramework} ""
		DetailPrint "Microsoft .NET Framework is alread installed."	
	${Else}
		IntOp $0 ${SF_SELECTED} | ${SF_RO}
		SectionSetFlags ${DotnetFramework} $0
		DetailPrint "Microsoft .NET Framework has not installed."
	${EndIf}

FunctionEnd
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

;--------------------------------
;Install Callbacks
;--------------------------------
Function .onInit
  ReadRegStr $0 HKLM "${PRODUCT_UNINST_KEY}" "UninstallString"
  ${If} $0 != ""
    MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "系统需移除之前版本 ${PRODUCT_NAME}，确定移除之前版本？" IDNO +4
    System::Call 'kernel32::GetModuleFileNameA(i 0, t .R0, i 1024) i r1'
    WriteRegStr HKLM "${PRODUCT_UNINST_KEY}" "InstallString" "$R0"
    Exec "$0"
    Abort
  ${EndIf}

  Call CheckPreinstalled

  ;!insertmacro MUI_LANGDLL_DISPLAY
FunctionEnd

Function .onInstSuccess
FunctionEnd

;--------------------------------
;Uninstall Callbacks
;--------------------------------
Var InstallString
Function un.onInit
  !insertmacro MUI_UNGETLANGUAGE

  ReadRegStr $InstallString HKLM "${PRODUCT_UNINST_KEY}" "InstallString"
  ${If} $InstallString == ""
    MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "你确实要完全移除 ${PRODUCT_NAME} ，其及所有的组件？" IDYES +2
    Abort
  ${EndIf}
FunctionEnd

Function un.onUninstSuccess
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  DeleteRegKey HKLM "${PRODUCT_UNINST_KEY}"
  
  ;删除桌面快捷方式
  Delete "$DESKTOP\DHMI.lnk"

  ${If} $InstallString == ""
    MessageBox MB_ICONINFORMATION|MB_OK "${PRODUCT_NAME} 已成功地从你的计算机移除。"
  ${Else}
    Exec "$InstallString"
  ${EndIf}
FunctionEnd
