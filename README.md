# Proyecto-Tiwkay
PASOS PARA USAR GIT
/// para remover un repositorio local
rm rf .git*
/////////////////////////////////Inicio del juego
Crear repositorio: gitignore and README
Git init
Git checkout -b main
Git remote add origin https:….

////////////Git remote –v     ////Para ver cómo está establecido el origin
Git fetch       ///Traer los archivos en el repositorio a mi repositorio local incluidas las branches
Git pull origin main    //Trae solo cambios en los archivos, incluye los readme y gitingnore.
Git status 
//Agregar gitatributtes a repositorio local
Git lfs install
Git add .
Git commit -m “Initial Commit” //Plantilla siempre al inicio
Git push origin main

/////////////////////////////////

Nuevo Branch
Git checkout -b h/john-readme
Git Branch       //para ver cuantos branches hay en el repositorio local
// Cambios en readme
Git log //Veces que una persona a hecho push y cuando lo hizo
Git add .
Git commit -m “Acutualizar el archivo REadme”
Git push origin h/john-readme

//////////////////////////////////////////////////Revisar la Branch en github
