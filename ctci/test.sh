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
    ls -d */ 2>/dev/null || echo "No directories found"
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
