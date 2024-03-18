#!/bin/bash

set -e

run_cmd="dotnet Worker.Consumer.dll"

sleep 5

exec $run_cmd