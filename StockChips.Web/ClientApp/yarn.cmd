@echo off
setlocal

set "YARN_NODE=%NODE_EXE%"
if not defined YARN_NODE set "YARN_NODE=node"

"%YARN_NODE%" "%~dp0.yarn\releases\yarn-1.22.22.js" %*

endlocal
