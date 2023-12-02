package main


func main() {
  // get the passed text from the command line
  var calibrationString string
  fmt.Scanln(&calibrationString)
  var first int
  var last int
  for i := 0; i < len(calibrationString); i++ {
    // if calibration string is a number set to first
    if calibrationString[i] >= '0' && calibrationString[i] <= '9' {   } 
}
  
