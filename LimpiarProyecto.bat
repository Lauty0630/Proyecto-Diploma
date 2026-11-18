@echo off
rem ==================================================================================
rem  LimpiarProyecto.bat - deja la carpeta del proyecto FLY SAFE solo con lo que
rem  necesita el sistema para compilar y correr.
rem
rem  - Mueve a la carpeta "_para_borrar" lo que no se usa: scripts JS de Enterprise
rem    Architect, scripts SQL viejos de la raiz (copias desactualizadas), el proyecto
rem    MSI viejo (PROYECTO_ING_DE_SOFTWARE_Setup, que empaquetaba una version vieja
rem    del sistema y de la base), "Claude outputs", UpgradeLog.htm y 3 archivos .cs
rem    que no forman parte de ningun proyecto.
rem  - Borra las carpetas bin y obj (son resultados de compilacion: Visual Studio
rem    las vuelve a crear al compilar, asi no queda nada viejo).
rem
rem  Cerra Visual Studio antes de ejecutarlo.
rem  Revisa "_para_borrar" y, si esta todo bien, borrala.
rem ==================================================================================
cd /d "%~dp0"
set "DEST=_para_borrar"
if not exist "%DEST%" mkdir "%DEST%"

echo Moviendo archivos que no usa el sistema a %DEST% ...
for %%F in ("EsquemaCompleto.sql" "FLYSAFE_Diagrama_Conceptual_RFN1.js" "FLYSAFE_Diagrama_Procesos_RFN1.js" "FLYSAFE_Diagramas_CUN01-CUN05.js" "FLYSAFE_Diagramas_CUN01-CUN06.js" "Script_Rol_Gerente_Reporte.sql" "Script_Serializacion_XML.sql" "Script_Vuelos_y_Adicionales.sql" "UpgradeLog.htm") do (
    if exist %%F move /y %%F "%DEST%\" >nul
)
for %%D in ("Claude outputs" "PROYECTO_ING_DE_SOFTWARE_Setup") do (
    if exist %%D move /y %%D "%DEST%\" >nul
)
if exist "BE\Comunes\Class1.cs" move /y "BE\Comunes\Class1.cs" "%DEST%\BE_Class1.cs" >nul
if exist "Servicios\Serializacion\UsuarioXml_GV42.cs" move /y "Servicios\Serializacion\UsuarioXml_GV42.cs" "%DEST%\" >nul
if exist "Servicios\Serializacion\EstiloXmlUsuarios_GV42.cs" move /y "Servicios\Serializacion\EstiloXmlUsuarios_GV42.cs" "%DEST%\" >nul

echo Borrando resultados de compilacion (bin y obj) ...
for %%P in ("BE" "BLL" "DAL" "Servicios" "PROYECTO ING DE SOFTWARE" "Instalador" "InstaladorUnico") do (
    if exist "%%~P\bin" rmdir /s /q "%%~P\bin"
    if exist "%%~P\obj" rmdir /s /q "%%~P\obj"
)

echo.
echo Listo. Revisa la carpeta %DEST% y borrala si esta todo bien.
echo Despues abri la solucion y compila en Release (Compilar ^> Recompilar solucion).
pause
