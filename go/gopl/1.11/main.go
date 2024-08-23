package main

import (
	"bufio"
	"fmt"
	"io"
	"net/http"
	"os"
	"time"
)

func main() {
	start := time.Now()
	ch := make(chan string)

	file, err := os.Open("cht.txt")
	if err != nil {
		fmt.Fprintf(os.Stderr, "open file: %v\n", err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
  urls := []string{}
	for scanner.Scan() {
    urls = append(urls, scanner.Text())
	}
  for _, url := range urls {
    go fetch(url, ch)
  }

	for range urls {
		fmt.Println(<-ch)
	}

	fmt.Printf("%.2fs elapsed\n", time.Since(start).Seconds())
}

func fetch(url string, ch chan<- string) {
	start := time.Now()
	client := &http.Client{}
	req, err := http.NewRequest(http.MethodGet, url, nil)
	if err != nil {
		ch <- fmt.Sprint(err)
	}

	req.Header.Set("User-Agent", "curl")
	resp, err := client.Do(req)
	if err != nil {
		ch <- fmt.Sprintf("while reading %s: %v", url, err)
	}
 
  // ch <- resp.Status
	// out, err := os.Create("cht.txt")
	// if err != nil {
	// 	fmt.Fprintf(os.Stderr, "create file: %v\n", err)
	// }
	// defer out.Close()

	nbytes, err := io.Copy(io.Discard, resp.Body)
	if err != nil {
		fmt.Fprintf(os.Stderr, "create request: %v\n", err)
	}
	defer resp.Body.Close()

	secs := time.Since(start).Seconds()
	ch <- fmt.Sprintf("%.2fs %7d %s", secs, nbytes, url)
}
