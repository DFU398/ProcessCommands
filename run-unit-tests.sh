#!/bin/bash
set -xe

podman build --tag unittests --file .github/actions/run-unit-tests/Dockerfile
podman run --volume .:/github/workspace:z --interactive --tty unittests
