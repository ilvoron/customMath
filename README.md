<div align="center">
  
  <a href="" title="OS">![OS](https://img.shields.io/badge/os-Windows-blue?cacheSeconds=3600)</a>
  <a href="" title=".NET Core 3.1">![OS](https://img.shields.io/badge/.NET_Core-3.1-blue?cacheSeconds=3600)</a>
  <a href="" title="GitHub License">![GitHub License](https://img.shields.io/github/license/ilvoron/SnakeGameC?cacheSeconds=3600)</a>
  
</div>

# Reverse Polish Notation (RPN) Text Calculator

## Overview

This project is a simple yet extensible text-based calculator using **Reverse Polish Notation (RPN)**. It allows you to easily add new functions and operators as needed, making it a powerful tool for solving various mathematical expressions.

## How It Works

Let’s say you write an expression like `3 + 4`. For you, this has meaning—it's an arithmetic expression, and you know that the result is `7`. But how can you tell a computer to understand and solve this? Writing the expression directly into code is one approach, but what if you want to work with different numbers or more complex expressions like:

```
sqrt(9) + 21 * 7 - (3 + 4) / 2
```

One solution is **Reverse Polish Notation**.

### What is Reverse Polish Notation (RPN)?

RPN is a way to represent mathematical expressions without the need for parentheses, based on how computers process operations. Instead of writing expressions in the standard form (`3 + 4`), RPN uses a stack-based approach. The expression `3 + 4` would be transformed into `3 4 +`. 

Here’s how the process works:

1. Convert the expression into RPN: `3 + 4` becomes `[3, 4, "+"]`.
2. The computer scans the array from left to right. When it encounters an operator like `+`, it applies the operation to the previous two items (`3` and `4`).
3. The array then reduces to `[7]`.
4. Once there's only one item left, the result is printed, which in this case is `7`.

The RPN approach becomes more useful when handling expressions with different operator priorities, brackets, and functions.

### Example

For a more complex expression like `sqrt(9) + 21 * 7 - (3 + 4) / 2`, RPN converts it into a format that’s easy for the computer to understand and solve.

## Background

This project is a rewrite of code I originally wrote in Pascal back in 2015, while I was still in school. The inspiration came from my curiosity about how software can interpret and solve equations step by step. I was fascinated by this and decided to create something similar. In August 2021, I revisited the project and rewrote it in C# using my current skills.
