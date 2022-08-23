# What it is?

It it a simple text calculator using [reverse polish notation](https://en.wikipedia.org/wiki/Reverse_Polish_notation). It pretty extensible so you can add new functions as much as you want.

So. How is works? Imagine a situation when you write a string like "3+4". As for you it has meaning: it's an expression; and you can solve it: the answer is 7. But how can you tell a computer this? Yes, you can write this as code and print the output, but what if we wanna use other number or write a more complicated expressions like "sqrt(9) + 21*7 - (3+4)/2". So, I repeat. How can you tell a computer what is it? and what should it do with this text? I believe that there're many answers on this question. But one of them is [reverse polish notation](https://en.wikipedia.org/wiki/Reverse_Polish_notation).

Simply, it bases on "converting" this text to something what computer can understand. So the text "3+4" converts to [3, 4, "+"]. And the computer goes through this array from left to right trying to find an operator. When it found one it looking for what to do. For "+" it's "sum two items before". And then the array from [3, 4, "+"] comes to [7]. And 'cause array is now has one item, he prints the answer - "7". In reality it's more complicated 'cause we have different priority of some operators (like "+" and "*"), we have brackets, we have functions and other. But basic principles are the same.

# Background

This project is rebuild of my old code which was written in Pascal as I was in a school (maybe it was 2015).
The idea of this came to me when I was like "wow, how this thing (an app) understand my writings (equations) and can solve them and show to me step-by-step solution". I was impressed, so I decided to create something like that.
After year I found that project and decided to recreate it with my current skills in C# (it was August 2021).

Here's one more interesting fact: after school I wanted to apply to somewhere where I can learn computer science, but 'cause of my parents I decided to go to become a doctor (huge step). And as expected, nothing good happen so after 1.5 year of education I left it (it was 22th February 2022. It's quite symbolic 'cause I from Russian and there're some bad things happen at the 24th February) and got issues (well, I guess, we all have some of them). So now I finally "heal" myself so I can say that was enlightening and I really want to learn computer science and write a code.
