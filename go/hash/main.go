package main

import (
	"crypto/sha256"
	"flag"
	"fmt"
	"hash/fnv"
	"io/fs"
	"os"
	"path/filepath"
	"time"

	"github.com/cespare/xxhash"
)

var hashFlag string

func main() {
	flag.StringVar(&hashFlag, "a", "sha256", "choose the hashing algorithm, must be 'xxhash', 'fnv', or 'sha256'")
	flag.Parse()
	dirs := flag.Args()
	start := time.Now()

	hashChannel := make(chan []byte, 5)
	for _, dir := range dirs {
		fileInfo, err := os.Stat(dir)
		dirname := fileInfo.Name() + ".txt"
		fmt.Printf("dirname: %v\n", dirname)

		if err != nil {
			panic("invalid argument passed")
		}

		if fileInfo.IsDir() {
			f, err := os.OpenFile(dirname, os.O_CREATE|os.O_RDWR|os.O_APPEND, 0666)
			if err != nil {
				panic(err)
			}
			defer f.Close()

			filepath.Walk(dir, func(path string, info fs.FileInfo, err error) error {
				if err != nil {
					return err
				}

				if info.Name() == ".DS_Store" {
					return nil
				}
				if info.IsDir() {
					return nil
				}

				fmt.Printf("processing %v\n", path)
				go hash(path, hashChannel, hashFlag)
				
        for msg := range hashChannel {
					fmt.Printf("writing to file\n")
					f.Write(msg)
				}

				return nil
			})

			// go write(f, hashChannel)

		} else {
			panic("argument is not directory")
		}
	}

	end := time.Now()
	fmt.Printf("total elapsed time: %v\n", end.Sub(start))
}

func write(f *os.File, ch <-chan []byte) {
	for msg := range ch {
		fmt.Printf("writing to file\n")
		f.Write(msg)
	}
}

func hash(file string, ch chan<- []byte, hashType string) {
	hash := fmt.Sprint(time.Now().Unix()) + "\t" + file + "\n"
	fmt.Printf("%v hash of %v :: %v\n", hashType, file, hash)
	ch <- []byte(hash)

	// switch hashType {
	// case "xxhash":
	// 	useXxhash(file, ch)
	// case "fnv":
	// 	useFNV(file, ch)
	// case "sha":
	// 	useSHA256(file, ch)
	// }

}

func useXxhash(file string, ch chan<- []byte) {
	h := xxhash.New()
	ch <- h.Sum(nil)
}

func useFNV(file string, ch chan<- []byte) {
	h := fnv.New64()
	ch <- h.Sum(nil)
}

func useSHA256(file string, ch chan<- []byte) {
	h := sha256.New()
	ch <- h.Sum(nil)
}
