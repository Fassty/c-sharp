# Fixed Point Math Library for MFF .NET Course
## Project summary
>The primary goal was to create a fast implementation of **32 bit fixed-point** arithmetics.
>The project contains a library for basic arithmetic operations over the 3 implemented types:```Q24_8 , Q16_16 , Q8_24```

>Next step was to write unit tests with wide code coverage and add implicit conversion between ```int``` and ```Fixed<T>```
and overload the basic arithmetic operators ```+-*/``` for easier usage (_this conversion is implicit for simpler and faster usage of basic operations_)

>The last part of the project was to handle conversions between ```Fixed<T>```s of different QTypes and to implement BenchmarkDotNet 
library and run the benchmarks over RREF matrix reduction algorithm using the ```Fixed<T>``` types (_the conversion here is explicit
to prevent unwanted assignment between different types, this conversion also causes loss of information_)

## Goals for the project
- [x] Create a generic struct ```Fixed<T>``` and arithmetics for the ```Q24_8 , Q16_16 , Q8_24``` types
- [x] Add unit tests with wide code coverage
- [x] Add implicit conversion from int to ```Fixed<T>``` and overload the basic arithmetic operators
- [x] Add explicit conversion between QTypes while preserving highest possible precision
- [x] Implement the BenchmarkDotNet library and run benchmarks over RREF matrix reduction algorithm for ```Fixed<T>``` matricies
- [x] Switch to NUnit and extend tests
- [ ] Extend FixedPointAPI for the new conversion and operator functionality 

## Usage
The basic usage is shown in the API example which is a part of the project

You may also use the functionality of operators and implicit conversions e.g.:

```c#
Fixed<Q24_8> fixed = 5; // Creates a new Fixed<Q24_8> object with the value of 5
var sum = fixed + 10; // Creates a new Fixed<Q24_8> with the value of 15 being a sum of the value of fixed and 10
var product = fixed * fixed * 10; // Creates a new Fixed<Q24_8> with the value of 250
var quotient = fixed / 2; // Creates a new Fixed<Q24_8> with the value of 2.5

sum += 5; // Creates a new Fixed<Q24_8> with the value of 20 and assigns it back to the sum object
```

And also explicit conversions...
```c#
Fixed<Q16_16> q16 = 1024;
Fixed<Q24_8> q24 = (Fixed<Q24_8>)q16; // The value will remain 1024
Fixed<Q8_24> q8 = (Fixed<Q8_24>)q16; // The value will get rounded to 0 because of 8bit int precision of Q8_24
```
