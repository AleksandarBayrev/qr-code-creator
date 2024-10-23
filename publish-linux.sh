#!/bin/bash
rm -rf ./dist
dotnet publish ./qr-code-creator.csproj -o ./dist/linux-x64 --sc -r linux-x64 -c Release -p:PublishSingleFile=true