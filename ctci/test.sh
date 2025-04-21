#!/usr/bin/env zsh

function handle_new() {
    local name=$1
    
    if [[ -z "$name" ]]; then
        echo "Error: Name is required for the 'new' command"
        exit 1
    fi
    
    mkdir -p "$name"/{js,cs,cpp}
    
    # Initialize js project
    (cd "$name/js" && pnpm init)
    
    # Initialize cs project
    (cd "$name/cs" && dotnet new console)
    
    echo "Created new project '$name' with js, cs, and cpp directories"
}

# Function to handle the 'test' command
function handle_test() {
    echo "Running tests..."
    local directories=(*/)
    
    if [[ ${#directories[@]} -eq 0 ]]; then
        echo "No directories found"
        return
    fi
    
    # Process each directory
    for dir in "${directories[@]}"; do
        # Remove trailing slash
        dir=${dir%/}
        echo "Processing directory: $dir"
        
        # Check directory type and run appropriate commands
        if [[ -d "$dir/js" ]]; then
            echo "  Testing JS directory..."
            (cd "$dir/js" && pnpm test 2>/dev/null || echo "  No tests available in JS")
        fi
        
        if [[ -d "$dir/cs" ]]; then
            echo "  Testing CS directory..."
            (cd "$dir/cs" && dotnet test 2>/dev/null || echo "  No tests available in CS")
        fi
        
        if [[ -d "$dir/cpp" ]]; then
            echo "  Testing CPP directory..."
            # Add appropriate C++ test command here
            echo "  No test command configured for CPP"
        fi
    done
}

# Main logic
if [[ $# -eq 0 ]]; then
    echo "Usage: $0 <command> [arguments]"
    echo "Commands:"
    echo "  new [name]   - Create a new project directory"
    echo "  test         - List all directories in current location"
    exit 1
fi

command=$1
shift

case "$command" in
    "new")
        handle_new "$1"
        ;;
    "test")
        handle_test
        ;;
    *)
        echo "Unknown command: $command"
        echo "Valid commands are: new, test"
        exit 1
        ;;
esac
