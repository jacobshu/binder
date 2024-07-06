package main

import (
	"bufio"
	"fmt"
	"os"
	"slices"
	"strings"
)

type count struct {
	files []string
	count int
}

func main() {
	counts := make(map[string]count)
	files := os.Args[1:]
	if len(files) == 0 {
		countLines("stdin", os.Stdin, counts)
	} else {
		for _, arg := range files {
			f, err := os.Open(arg)
			if err != nil {
				fmt.Fprintf(os.Stderr, "dup2: %v\n", err)
				continue
			}
			countLines(arg, f, counts)
			f.Close()
		}
	}

	for line, countMap := range counts {
		if countMap.count > 1 {
			fmt.Printf("%d\t%s\t%s\n", countMap.count, line, strings.Join(countMap.files, ", "))
		}
	}
}

func countLines(src string, f *os.File, counts map[string]count) {
	input := bufio.NewScanner(f)
	for input.Scan() {
		text := input.Text()
		if entry, ok := counts[text]; ok {
			if !slices.Contains(entry.files, src) {
				entry.files = append(entry.files, src)
			}
			entry.count++
			counts[text] = entry
		} else {
      newEntry := count{
        files: []string{src},
        count: 1,
      }
      counts[text] = newEntry
    }
	}
}
