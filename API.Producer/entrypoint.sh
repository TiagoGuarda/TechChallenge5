#!/bin/bash

set -e

run_cmd="dotnet API.Producer.dll"

sleep 5

exec $run_cmd