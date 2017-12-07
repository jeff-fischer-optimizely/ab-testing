IF "%1"=="Debug" (set Configuration=Debug) ELSE (set Configuration=Release)

set PackagePath="..\artifacts\%Configuration%\KpiDeployment"
set ProjectPath="..\src\EPiServer.Marketing.KPI"
set Dependencies="EPiServer.CMS.Core"

IF exist "%PackagePath%" ( rd "%PackagePath%" /s /q )

md "%PackagePath%\lib"

xcopy "%ProjectPath%\bin\Debug\net461\EPiServer.Marketing.KPI.dll" "%PackagePath%\lib\"  /I /F /R /Y

xcopy "%ProjectPath%\Package.nuspec" "%PackagePath%\"  /I /F /R /Y

xcopy "%ProjectPath%\EPiServer.Marketing.KPI.xproj" "%PackagePath%\"  /I /F /R /Y

xcopy "..\src\Database"\KPI\*.sql "%PackagePath%\tools\epiupdates\sql"  /I /F /R

rem get versions

"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe" powershell -ExecutionPolicy ByPass -File "buildpackage.ps1" "%PackagePath%" "%ProjectPath%" "%Dependencies%"

xcopy "%PackagePath%"\*nupkg "..\artifacts"  /I /F /R /Y

rd "%PackagePath%" /s /q
