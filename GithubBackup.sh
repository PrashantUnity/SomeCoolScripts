#!/bin/zsh

# ==========================================
# Configuration Variables
# ==========================================
GITHUB_ACCOUNT="codefrydev"
TARGET_DIR="$HOME/GitHub-Clones"
REPO_LIMIT=1000
VISIBILITY="public,private"

# ==========================================
# Pre-flight Checks
# ==========================================
# Ensure the GitHub CLI is installed and authenticated
if ! command -v gh &> /dev/null; then
    echo "Error: GitHub CLI (gh) is not installed."
    exit 1
fi

# Create target directory if it doesn't exist
mkdir -p "$TARGET_DIR"
cd "$TARGET_DIR" || { echo "Error: Cannot access $TARGET_DIR"; exit 1; }

echo "=== Starting GitHub Sync for @${GITHUB_ACCOUNT}: $(date) ==="

# ==========================================
# 1. Update Existing Repositories
# ==========================================
echo -e "\n[1/2] Updating existing local repositories..."

# Enable null_glob so the loop doesn't fail if the directory is completely empty
setopt NULL_GLOB
for repo_dir in */; do
    if [ -d "${repo_dir}.git" ]; then
        echo "  -> Pulling latest for ${repo_dir%/}"
        # Use a subshell ( ) so we don't have to manually 'cd ..' back to TARGET_DIR
        (cd "$repo_dir" && git pull -q)
    fi
done
unsetopt NULL_GLOB

# ==========================================
# 2. Clone New Repositories
# ==========================================
echo -e "\n[2/2] Checking GitHub for new repositories..."

gh repo list "$GITHUB_ACCOUNT" \
    --limit "$REPO_LIMIT" \
    --visibility "$VISIBILITY" \
    --json name \
    --jq '.[].name' | while read -r repo_name; do
    
    if [ ! -d "$repo_name" ]; then
        echo "  -> Found new repository! Cloning $repo_name..."
        gh repo clone "$GITHUB_ACCOUNT/$repo_name" "$repo_name"
    fi
done

echo -e "\n=== Sync Complete: $(date) ==="
