```
ToDo
- settings
- testengine
```
```
0.1     init
0.2     shell
0.3     meny
0.4     example page
0.5     file structure
0.6     debug
0.61    debug workflow
```
```
core
- input
  alla inputmessages plus ShellMessage for att prata mellan moduler
- debug
  simpel hemmagjord debugmanager
- state
  all outputstate plus ShellState for dela data mellan moduler
- init
  basic inits, kommer senare hämta data via Cmd
- *folder*
  coredata for moduler, eventuellt flytta till separata projects senare

main
- *update*
  routeing/updates for varje page
- *view*
  view for varje page
- shell
  wrapper som hanterar all kommunikation mellan program -> moduler
- program
  entrypoint, skapar window, hanterar avalonia
```
