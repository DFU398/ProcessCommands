#!/bin/bash
set -xe

podman build --tag process-commands-build --file .github/actions/build/Dockerfile
podman run --volume .:/github/workspace:z --interactive --tty process-commands-build --target:Pack
