# AI Prompt

So yeah, I used Claude Code (Sonnet 4.5) for this challenge
Here's how it went down

## What I Actually Did
started by reading through the PDF to understand the problem
basically need to group consecutive same digits and map them to letters based on how many times they repeat
wrote the core decoder logic myself 
tricky part was handling the transitions 
when do you convert a group vs keep accumulating? 
figured out I needed to check if the next digit is different or if there's a space/backspace/send character

once I had something working, wanted to make sure it was actually correct and production-ready, 
so I brought in Claude Code to help me with Test Cases

## Prompts Used
**Initial testing:**
```
"can you test if it work perfectly?"
```

```
"cleans up the code and everything in this folder and just read this instruction...
production-ready code, clear structure, stability, professional standards"
```

```
"the markdown write too much look like ai and not good yet"
"doesn't look good yet okay start with Read me first i think make a better organized one"
```

## How We Split The Work

**Me:**
- Wrote the initial decoder logic
- Figured out the algorithm (grouping digits, modulo for cycling)
- Tested with HELLO and TURING examples
- Made sure the logic handled edge cases

**Claude Code:**
- Helped verify my algorithm was correct
- Reorganized my code into proper project structure
- Generated comprehensive test cases (38 tests covering stuff I didn't think of)
- Added error handling and input validation
- Fixed some bugs in my test data
- Set up the solution file and project configs
- Wrote documentation
