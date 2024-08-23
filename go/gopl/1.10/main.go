package main

import (
	"fmt"
	"io"
	"net/http"
	"os"
	"time"
)

func main() {
	start := time.Now()
	ch := make(chan string)
	for _, url := range os.Args[1:] {
		go fetch(url, ch)
	}

	for range os.Args[1:] {
		fmt.Println(<-ch)
	}

	fmt.Printf("%.2fs elapsed\n", time.Since(start).Seconds())
}

func fetch(url string, ch chan<- string) {
	start := time.Now()
	client := http.Client{}
	req, err := http.NewRequest(http.MethodGet, url, nil)
	if err != nil {
		ch <- fmt.Sprint(err)
	}

	req.Header.Set("User-Agent", "curl")
	resp, err := client.Do(req)
	if err != nil {
		ch <- fmt.Sprintf("while reading %s: %v", url, err)
	}

	out, err := os.Create("cht.txt")
	if err != nil {
		fmt.Fprintf(os.Stderr, "create file: %v\n", err)
	}
	defer out.Close()

	nbytes, err := io.Copy(out, resp.Body)
	if err != nil {
		fmt.Fprintf(os.Stderr, "create request: %v\n", err)
	}
	defer resp.Body.Close()

	secs := time.Since(start).Seconds()
	ch <- fmt.Sprintf("%.2fs %7d %s", secs, nbytes, url)
}
