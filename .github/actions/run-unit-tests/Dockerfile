FROM alpine:3.24

RUN apk add --no-interactive dotnet8-sdk

# set workdir to where github actions mounts the repo root
WORKDIR /github/workspace

CMD ["dotnet", "test"]