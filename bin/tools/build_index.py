#!/usr/bin/env python3
"""
Minimal build_index.py for wk-memory-index.ps1.
Returns space-separated long integers (line offsets) or empty string for no index.
"""
import sys

if __name__ == "__main__":
    # Return empty string (no offsets) - wk-memory-index.ps1 will split by space
    # and RemoveEmptyEntries will result in empty array
    print("")
