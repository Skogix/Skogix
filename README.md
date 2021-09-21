```
ToDo
- läs
  - https://docs.avaloniaui.net/docs/controls
  - https://docs.avaloniaui.net/docs/layout
- kolla upp om det är värt att använda xaml for layout
  - https://docs.avaloniaui.net/docs/getting-started/ide-support
  - verkar vara värt for att få ha lite designer view osv
- settings
- testengine
- ui-haxx
  - mindre egen tutorial/guide/lista over ui-components i avalonia
  - submenyer
  - keyboard binds
  
- testprojekt
  - chess
  - pathfinding
  - parser
```
```
0.1     init
0.2     shell
0.3     meny
0.4     example page
0.5     file structure
0.6     debug
0.7     tic-tac-toe
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
