#!/bin/bash
rm -rf ./dist
dotnet publish ./qr-code-creator.csproj -o ./dist/win-x64 --sc -r win-x64 -c Release -p:PublishSingleFile=true