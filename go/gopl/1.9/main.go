package main

import (
	"fmt"
	"io"
	"net/http"
	"os"
	"strings"
)

func main() {
	for _, url := range os.Args[1:] {
		var sb strings.Builder
		if strings.HasPrefix(url, "http://") {
			sb.WriteString(url)
		} else {
			fmt.Fprintf(&sb, "https://%s", url)
		}

		resp, err := http.Get(sb.String())
		if err != nil {
			fmt.Fprintf(os.Stderr, "fetch: %v\n", err)
		}

		b, err := io.Copy(os.Stdout, resp.Body)
		if err != nil {
			fmt.Fprintf(os.Stderr, "copy: %v\n", err)
		}
		defer resp.Body.Close()

    fmt.Fprintf(os.Stdout, "%d written\nSTATUS: %s\n", b, resp.Status)
	}
}
