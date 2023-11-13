#!/bin/bash

IMAGE_NAME="mes-poc-dotnet-app"
CURRENT_DIR="$(cd "$(dirname "$0")" && pwd)"

# Build the image
docker build -t "$IMAGE_NAME" $CURRENT_DIR